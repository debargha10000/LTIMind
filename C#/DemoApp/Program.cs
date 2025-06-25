using BorderedConsole;

await using var border = await BorderWindow.RunAsync();

Console.WriteLine("Hello from inside the frame.");
Console.Write("What’s your name? ");
var name = Console.ReadLine();
Console.WriteLine($"Welcome, {name}!");
