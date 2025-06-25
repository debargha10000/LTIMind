using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

class ConsoleWrapper
{
    private static int lastWidth = -1;
    private static int lastHeight = -1;
    private static string content = "";
    private static string userInput = "";

    public static void DrawWrapper()
    {
        int width = Console.WindowWidth;
        int height = Console.WindowHeight;

        Console.SetBufferSize(Math.Max(width, Console.BufferWidth), Math.Max(height, Console.BufferHeight));
        Console.Clear();

        // Draw top border
        Console.SetCursorPosition(0, 0);
        Console.Write("+" + new string('-', Math.Max(0, width - 2)) + "+");

        // Draw side borders
        for (int y = 1; y < height - 1; y++)
        {
            Console.SetCursorPosition(0, y);
            Console.Write("|");
            Console.SetCursorPosition(width - 1, y);
            Console.Write("|");
        }

        // Draw bottom border
        if (height > 1)
        {
            Console.SetCursorPosition(0, height - 1);
            Console.Write("+" + new string('-', Math.Max(0, width - 2)) + "+");
        }

        // Draw content
        DrawCenteredWrappedContent(content, width, height);

        // Draw user input field just above bottom
        DrawInputField();
    }

    public static void DrawCenteredWrappedContent(string text, int width, int height)
    {
        int usableWidth = width - 4;
        int usableHeight = height - 5; // Leave space for top/bottom + input

        var lines = WrapText(text, usableWidth);

        int startY = (height / 2) - (lines.Count / 2);

        for (int i = 0; i < lines.Count && i < usableHeight; i++)
        {
            int y = startY + i;
            if (y > 0 && y < height - 2)
            {
                string line = lines[i];
                int x = (width - line.Length) / 2;
                Console.SetCursorPosition(x, y);
                Console.Write(line);
            }
        }
    }

    public static void DrawInputField()
    {
        int y = Console.WindowHeight - 2;
        Console.SetCursorPosition(2, y);
        Console.Write("Input: " + userInput.PadRight(Console.WindowWidth - 10));
        Console.SetCursorPosition(9 + userInput.Length, y); // place cursor after input
    }

    public static List<string> WrapText(string text, int maxWidth)
    {
        List<string> lines = new();
        string[] words = text.Split(' ');

        StringBuilder line = new();

        foreach (var word in words)
        {
            if (line.Length + word.Length + 1 > maxWidth)
            {
                lines.Add(line.ToString());
                line.Clear();
            }

            if (line.Length > 0)
                line.Append(" ");

            line.Append(word);
        }

        if (line.Length > 0)
            lines.Add(line.ToString());

        return lines;
    }

    public static void Start(string initialContent)
    {
        content = initialContent;
        Console.CursorVisible = true;

        Thread inputThread = new Thread(CaptureInput);
        inputThread.Start();

        while (true)
        {
            if (Console.WindowWidth != lastWidth || Console.WindowHeight != lastHeight)
            {
                lastWidth = Console.WindowWidth;
                lastHeight = Console.WindowHeight;
                DrawWrapper();
            }

            Thread.Sleep(100);
        }
    }

    public static void CaptureInput()
    {
        while (true)
        {
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                content += "\n> " + userInput; // Append input to content
                userInput = "";
                DrawWrapper(); // Refresh screen
            }
            else if (key.Key == ConsoleKey.Backspace && userInput.Length > 0)
            {
                userInput = userInput.Substring(0, userInput.Length - 1);
                DrawWrapper();
            }
            else if (!char.IsControl(key.KeyChar))
            {
                userInput += key.KeyChar;
                DrawWrapper();
            }
        }
    }

    static void Main(string[] args)
    {
        Start("Welcome to the dynamic console wrapper! Type below.");
    }
}
