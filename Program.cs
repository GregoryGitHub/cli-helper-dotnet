namespace App.Main;

public class Program
{
    public static void Main()
    {
        var options = new List<UserOption<int>>
        {
            new UserOption<int>("Opção 1", 1),
            new UserOption<int>("Opção 2", 2),
            new UserOption<int>("Opção 3", 3),
            new UserOption<int>("Opção 4", 4),
            new UserOption<int>("Opção 5", 5),
            new UserOption<int>("Opção 6", 6),
        };

        var lista2 = new List<UserOption<string>>
        {
            new UserOption<string>("Opção única 1", "1"),
            new UserOption<string>("Opção única 2", "2"),
            new UserOption<string>("Opção única 3", "3"),
            new UserOption<string>("Opção única 4", "4"),
        };

        var cliChoose = new CliChoose();

        var selecionados = cliChoose.ChooseMany("Escolha suas opções:", options);

        Console.WriteLine("Você escolheu as seguintes opções:");

        foreach (var selecionado in selecionados)
        {
            Console.WriteLine(selecionado.Label);
        }

        var opcaoEscolhida = cliChoose.ChooseOne("Escolha uma opção:", lista2);

        Console.WriteLine("Você escolheu a seguinte opção:");
        Console.WriteLine(opcaoEscolhida.Label);
    }


}
