

namespace App.Main;

public class Program
{
    public static void Main()
    {
        var options = new List<Option>
        {
            new Option("Opção 1", false, false),
            new Option("Opção 2", false, false),
            new Option("Opção 3", false, false),
            new Option("Opção 4", false, false),
            new Option("Opção 5", false, false),
            new Option("Opção 6", false, false),
        };

        var cli = new Cli();

        cli.Choose("Escolha suas opções:", options);
    }


}
