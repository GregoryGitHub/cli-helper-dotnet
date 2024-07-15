namespace App.Main;

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
