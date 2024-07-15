namespace App.Main;

public class Cli
{
    private int cursorY = 0;
    private bool isSelecting = true;

    public IEnumerable<Option> Choose(string title, IEnumerable<Option> optionsParam)
    {
        cursorY = 0;
        var options = optionsParam.ToList();

        if (options.Count == 0)
        {
            Console.WriteLine("A lista não pode ser vazia");
        }

        options[0].IsHovered = true;

        while (isSelecting)
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
                    Console.WriteLine("Você Apertou o Entrer");

                    return options;
            }
        }

        return options;
    }

    private void IncreaseCursor(List<Option> options)
    {
        if (cursorY == options.Count - 1) return;

        cursorY++;
    }

    private void DecreaseCursor()
    {

        if (cursorY == 0) return;

        cursorY--;
    }

    private void HoverActualCursor(List<Option> options)
    {
        for (var index = 0; index < options.Count; index++)
        {
            options[index].IsHovered = false;
        }


        options[cursorY].IsHovered = true;
    }
    private void Render(List<Option> options)
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

            Console.WriteLine(options[i].Name);
        }
    }

}
