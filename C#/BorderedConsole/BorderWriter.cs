using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BorderedConsole
{
    internal class BorderWriter : TextWriter
    {
        private readonly BorderWindow _window;
        private readonly TextWriter _original;
        private readonly StringBuilder _buffer = new();

        public BorderWriter(BorderWindow window, TextWriter original)
        {
            _window = window;
            _original = original;
        }

        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(char value)
        {
            if (value == '\n')
            {
                FlushLine();
            }
            else if (value != '\r') // ignore carriage returns
            {
                _buffer.Append(value);
            }
        }

        public override void Write(string? value)
        {
            if (value == null) return;
            foreach (char c in value)
                Write(c);
        }

        public override void WriteLine(string? value)
        {
            Write(value);
            FlushLine();
        }

        private void FlushLine()
        {
            lock (_window)
            {
                _window.AppendLine(_buffer.ToString());
                _buffer.Clear();
            }
        }

    }

}
