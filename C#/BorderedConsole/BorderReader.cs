using System;
using System.IO;

namespace BorderedConsole
{
    internal class BorderReader : TextReader
    {
        private readonly BorderWindow _window;
        private readonly TextReader _original;

        public BorderReader(BorderWindow window, TextReader original)
        {
            _window = window;
            _original = original;

            new Thread(CaptureInput) { IsBackground = true }.Start();
        }

        private void CaptureInput()
        {
            while (true)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    _window.SubmitInput();
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    _window.BackspaceInput();
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    _window.AddInputChar(key.KeyChar);
                }
            }
        }

        public override string? ReadLine()
        {
            while (true)
            {
                var line = _window.ConsumeInput();
                if (line != null)
                    return line;
                Thread.Sleep(50);
            }
        }
    }
}
