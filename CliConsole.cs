using System;

namespace App.Main;

public static class CliConsole
{
    private const string TEXT_BOLD = "\x1b[1m";
    private const string TEXT_ITALIC = "\x1b[3m";
    private const string RESET = "\x1b[0m";

    public static void Write(string text, params CliConsoleTextStyle[]? parameters)
    {
        var format = GetStyles(parameters);

        Console.Write($"{format}{text}{RESET}");
    }

    public static void WriteLine(string text, params CliConsoleTextStyle[]? parameters)
    {
        var format  = GetStyles(parameters);
        Console.WriteLine($"{format}{text}{RESET}");
    }

    public static void WriteTitle(string title)
    {
        string border = new string('*', title.Length + 4);
        Console.WriteLine(border);
        WriteLine($"* {title} *", CliConsoleTextStyle.Bold);
        Console.WriteLine(border);
    }

    private static string GetStyles(CliConsoleTextStyle[]? parameters)
    {
        if(parameters is null) return "";

        return parameters.Aggregate(string.Empty, (acc, item) =>
        {

            acc += item switch
            {
                CliConsoleTextStyle.Bold => TEXT_BOLD,
                CliConsoleTextStyle.Italic => TEXT_ITALIC,
                _ => ""
            };

            return acc;
        });

    }

    private static void PrintBoxedText(string text, params CliConsoleTextStyle[]? parameters )
    {
        
    }
}
