using CliHelperDotnet.Core.Models;

namespace CliHelperDotnet.Core;

public enum ChooseMode
{
    One,
    Many
}

public class CliChoose
{
    private int _cursorY = 0;
    private ChooseMode _mode;

    public IEnumerable<UserOption<TValue>>? ChooseMany<TValue>(string title, IEnumerable<UserOption<TValue>> optionsParam)
    {
        _cursorY = 0;
        _mode = ChooseMode.Many;
        var options = optionsParam.Select(option => new CliOption<TValue>(option.Label, option.Value, false, false)).ToList();

        if (options.Count == 0)
        {
            Console.WriteLine("A lista não pode ser vazia");
            return default;
        }


        options[0].IsHovered = true;

        while (true)
        {
            Console.Clear();
            Console.ResetColor();
            Console.WriteLine($"{title}\n");
            Render(options);

            var key = Console.ReadKey().Key;


            switch (key)
            {
                case ConsoleKey.UpArrow:
                    
                    DecreaseCursor();
                    HoverActualCursor(options);

                    break;
                case ConsoleKey.DownArrow:

                    IncreaseCursor(options);
                    HoverActualCursor(options);

                    break;

                case ConsoleKey.Spacebar:

                    options[_cursorY].IsSelected = !options[_cursorY].IsSelected;

                    break;

                case ConsoleKey.Enter:
                    Console.Clear();
                    Console.ResetColor();
                    return options.Where(op=> op.IsSelected).Select(option => new UserOption<TValue>(option.Label, option.Value));
            }
        }
    }

    public UserOption<TValue> ChooseOne<TValue>(string title, IEnumerable<UserOption<TValue>> optionsParam)
    {
        _cursorY = 0;
        _mode = ChooseMode.One;
        var options = optionsParam.Select(option => new CliOption<TValue>(option.Label, option.Value, false, false)).ToList();

        if (options.Count == 0)
        {
            Console.WriteLine("A lista não pode ser vazia");
            return default;
        }

        options[0].IsSelected = true;

        while (true)
        {
            Console.Clear();
            Console.ResetColor();
            Console.WriteLine($"{title}\n");
            Render(options);

            var key = Console.ReadKey().Key;


            switch (key)
            {
                case ConsoleKey.UpArrow:
                    
                    DecreaseCursor();
                    SelectActualCursor(options);

                    break;
                case ConsoleKey.DownArrow:

                    IncreaseCursor(options);
                    SelectActualCursor(options);

                    break;

                case ConsoleKey.Enter:
                    Console.Clear();
                    Console.ResetColor();
                    return new UserOption<TValue>(options[_cursorY].Label, options[_cursorY].Value);
            }
        }


    }
    
    public IEnumerable<UserOption<TValue>>? SearchAndChooseMany<TValue>(string title, IEnumerable<UserOption<TValue>> optionsParam)
    {
        var search = string.Empty;
        _mode = ChooseMode.Many;
       _cursorY = 0; 

        var options = optionsParam.Select(option => new CliOption<TValue>(option.Label, option.Value, false, false)).ToList();
        var refOptions = options;

        if (options.Count == 0)
        {
            Console.WriteLine("A lista não pode ser vazia");
            return default;
        }

        options[0].IsHovered = true;

        while (true)
        {
            Console.Clear();
            Console.ResetColor();
            // CliConsole.WriteLine($"{title}", CliConsoleTextStyle.Bold, CliConsoleTextStyle.Italic);
            CliConsole.WriteTitle(title);
            Render(options);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"\nDigite para pesquisar na lista: {search}");

            var keyInfo = Console.ReadKey(true);
            var key = keyInfo.Key;
            var keyChar = keyInfo.KeyChar;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    
                    DecreaseCursor();
                    HoverActualCursor(options);

                    break;
                case ConsoleKey.DownArrow:
                    IncreaseCursor(options);
                    HoverActualCursor(options);

                    break;
                case ConsoleKey.Spacebar:

                    options[_cursorY].IsSelected = !options[_cursorY].IsSelected;

                    break;
                case ConsoleKey.Enter:
                    Console.Clear();
                    Console.ResetColor();
                    return options.Where(op=> op.IsSelected).Select(option => new UserOption<TValue>(option.Label, option.Value));
                case ConsoleKey.Backspace:
                    if(string.IsNullOrEmpty(search)) break;
                    
                    search = search.Remove(search.Length - 1);
                    var filteredOptionsBp = refOptions.Select((op)=> {op.IsHovered = false; return op;}).Where(op => op.Label.Contains(search)).ToList();
                    _cursorY = 0;
                    options = filteredOptionsBp;
                    break;

                default:

                    search += keyChar;
                    var filteredOptions = refOptions.Select((op)=>{op.IsHovered = false; return op;}).Where(op => op.Label.Contains(search)).ToList();

                    _cursorY = 0; 
                    options = filteredOptions;
                    break;
            }
        }
        
        
    }

    private string NormalizeString(ConsoleKey key, ConsoleKeyInfo keyInfo)
    {

        string[] digitKeys = ["D1", "D2", "D3","D4","D5","D6","D7","D8","D9","D0"];
        string tabKey = "Tab";

        var stringKey = key.ToString();
        
        if(digitKeys.Contains(stringKey))
        {
            return stringKey.Replace("D","");
        }

        if(stringKey == tabKey)
        {
            return "    ";
        }

        // Detect if is lowercase or uppercase
        if(keyInfo.Modifiers == ConsoleModifiers.Shift)
        {
            return stringKey.ToUpper();
        }

        return stringKey;
    }
    
    private void IncreaseCursor<TValue>(List<CliOption<TValue>> options)
    {
        if (_cursorY == options.Count - 1) return;

        _cursorY++;
    }

    private void DecreaseCursor()
    {

        if (_cursorY == 0) return;

        _cursorY--;
    }

    private void HoverActualCursor<TValue>(List<CliOption<TValue>> options)
    {
        for (var index = 0; index < options.Count; index++)
        {
            options[index].IsHovered = false;
        }


        options[_cursorY].IsHovered = true;
    }

    private void SelectActualCursor<TValue>(List<CliOption<TValue>> options)
    {
        for (var index = 0; index < options.Count; index++)
        {
            options[index].IsSelected = false;
        }

        options[_cursorY].IsSelected = true;
    }
    
    private void Render<TValue>(List<CliOption<TValue>> options)
    {
        
        var checkIconByMode = _mode == ChooseMode.Many ? "☑ " : "➤ ";
        var unCheckIconByMode = _mode == ChooseMode.Many ? "☐ " : "  ";

        for (int i = 0; i < options.Count; i++)
        {
            Console.ResetColor();
            
            if (options[i].IsSelected)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                
                Console.Write(checkIconByMode);
            }
            else
            {
                Console.Write(unCheckIconByMode);
            }

            if (options[i].IsHovered)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            Console.WriteLine(options[i].Label);
        }
    }

}
