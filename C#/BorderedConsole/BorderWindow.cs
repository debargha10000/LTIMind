using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BorderedConsole
{

    public sealed class BorderWindow : IAsyncDisposable
    {
        private readonly List<string> _outputLines = new();
        private string _currentInput = "";
        private readonly Thread _paint;
        private readonly CancellationTokenSource _cts = new();

        private readonly TextWriter _origOut = Console.Out;
        private readonly TextWriter _origErr = Console.Error;
        private readonly TextReader _origIn = Console.In;
        private string? _submittedInput = null;

        internal void AddInputChar(char c)
        {
            _currentInput += c;
        }

        internal void BackspaceInput()
        {
            if (_currentInput.Length > 0)
                _currentInput = _currentInput[..^1];
        }

        internal void SubmitInput()
        {
            _submittedInput = _currentInput;
            AppendLine("> " + _currentInput);
            _currentInput = "";
        }

        internal string? ConsumeInput()
        {
            var temp = _submittedInput;
            _submittedInput = null;
            return temp;
        }

        public static Task<BorderWindow> RunAsync(
            string? title = null,
            char h = '-', char v = '|', char tl = '+', char tr = '+',
            char bl = '+', char br = '+')
        {
            var window = new BorderWindow(h, v, tl, tr, bl, br);
            return Task.FromResult(window);
        }

        private BorderWindow(char h, char v, char tl, char tr, char bl, char br)
        {
            Console.SetOut(new BorderWriter(this, _origOut));
            Console.SetError(new BorderWriter(this, _origErr));
            Console.SetIn(new BorderReader(this, _origIn));

            _paint = new Thread(() =>
            {
                while (!_cts.IsCancellationRequested)
                {
                    DrawFrame(); // You’ll fill this in
                    Thread.Sleep(50);
                }
            })
            { IsBackground = true };

            _paint.Start();
        }

        public async ValueTask DisposeAsync()
        {
            _cts.Cancel();
            _paint.Join();

            Console.SetOut(_origOut);
            Console.SetError(_origErr);
            Console.SetIn(_origIn);

            _cts.Dispose();
            await Task.CompletedTask;
        }
        internal void AppendLine(string line)
        {
            lock (_outputLines)
            {
                _outputLines.Add(line);
                if (_outputLines.Count > Console.WindowHeight - 5)
                    _outputLines.RemoveAt(0);
            }
        }

        private void DrawFrame()
        {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            Console.SetBufferSize(Math.Max(width, Console.BufferWidth), Math.Max(height, Console.BufferHeight));
            Console.Clear();

            // Top border
            Console.SetCursorPosition(0, 0);
            Console.Write("+" + new string('-', width - 2) + "+");

            // Side borders
            for (int y = 1; y < height - 1; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write("|");
                Console.SetCursorPosition(width - 1, y);
                Console.Write("|");
            }

            // Bottom border
            Console.SetCursorPosition(0, height - 1);
            Console.Write("+" + new string('-', width - 2) + "+");

            // Draw output content
            lock (_outputLines)
            {
                int startY = 2;
                foreach (var line in _outputLines)
                {
                    if (startY >= height - 3) break;
                    Console.SetCursorPosition(2, startY++);
                    Console.Write(line.PadRight(width - 4));
                }
            }

            // Draw input prompt
            Console.SetCursorPosition(2, height - 2);
            Console.Write("Input: " + _currentInput.PadRight(width - 10));
            Console.SetCursorPosition(9 + _currentInput.Length, height - 2);
        }
    }
}

// You will
