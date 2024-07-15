namespace App.Main;

public class CliChoose
{
    private int cursorY = 0;

    public IEnumerable<UserOption<TValue>>? ChooseMany<TValue>(string title, IEnumerable<UserOption<TValue>> optionsParam)
    {
        cursorY = 0;
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

                    options[cursorY].IsSelected = !options[cursorY].IsSelected;

                    break;

                case ConsoleKey.Enter:
                    Console.Clear();
                    return options.Where(op=> op.IsSelected).Select(option => new UserOption<TValue>(option.Label, option.Value));
            }
        }
    }

    private void IncreaseCursor<TValue>(List<CliOption<TValue>> options)
    {
        if (cursorY == options.Count - 1) return;

        cursorY++;
    }

    private void DecreaseCursor()
    {

        if (cursorY == 0) return;

        cursorY--;
    }

    private void HoverActualCursor<TValue>(List<CliOption<TValue>> options)
    {
        for (var index = 0; index < options.Count; index++)
        {
            options[index].IsHovered = false;
        }


        options[cursorY].IsHovered = true;
    }
    
    private void Render<TValue>(List<CliOption<TValue>> options)
    {
        for (int i = 0; i < options.Count; i++)
        {
            if (options[i].IsHovered)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (options[i].IsSelected)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("[✔] ");
            }
            else
            {
                Console.Write("[ ] ");
            }

            Console.WriteLine(options[i].Label);
        }
    }

}
