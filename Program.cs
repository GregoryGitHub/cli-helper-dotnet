using CliHelperDotnet.Core;
using CliHelperDotnet.Core.Models;

namespace CliHelperDotnet;

public class Program
{
    public static void Main()
    {
        var options = new List<UserOption<int>>
        {
            new UserOption<int>("NAF Meios Pagamento 1", 1),
            new UserOption<int>("Hub Antifraude 2", 2),
            new UserOption<int>("Reservas 3", 3),
            new UserOption<int>("Núcleo Clientes 4", 4)
        };

        var lista2 = new List<UserOption<string>>
        {
            new UserOption<string>("Opção única 1", "1"),
            new UserOption<string>("Opção única 2", "2"),
            new UserOption<string>("Opção única 3", "3"),
            new UserOption<string>("Opção única 4", "4"),
        };

        var listaTabela = new List<Dictionary<string, string>>
        {
            new Dictionary<string, string>
            {
                {"Nome", "João"},
                {"Idade", "20"},
                {"Cidade", "São Paulo"}
            },
            new Dictionary<string, string>
            {
                {"Nome", "Maria"},
                {"Idade", "30"},
                {"Cidade", "Rio de Janeiro"}
            },
            new Dictionary<string, string>
            {
                {"Nome", "José"},
                {"Idade", "40"},
                {"Cidade", "Curitiba"}
            },
            new Dictionary<string, string>
            {
                {"Nome", "Isbela Cabral"},
                {"Idade", "32"},
                {"Cidade", "São Lourenço da Mata PE"}
            }
        };

        // var cliChoose = new CliChoose();

        // var selecionados = cliChoose.ChooseMany("Escolha suas opções:", options);

        // Console.WriteLine("Você escolheu as seguintes opções:");

        // foreach (var selecionado in selecionados)
        // {
        //     Console.WriteLine(selecionado.Label);
        // }

        // var opcaoEscolhida = cliChoose.ChooseOne("Escolha uma opção:", lista2);

        // Console.WriteLine("Você escolheu a seguinte opção:");
        // Console.WriteLine(opcaoEscolhida.Label);

        // var opcoesEscolhidas = cliChoose.SearchAndChooseMany("Escolha um sistema para iniciar:", options);

        // if (opcoesEscolhidas == null)
        // {
        //     Console.WriteLine("Nenhuma opção selecionada");
        //     return;
        // }

        // Console.WriteLine("Você escolheu as seguintes opções:");

        // foreach (var opcaoEscolhida in opcoesEscolhidas)
        // {
        //     Console.WriteLine(opcaoEscolhida.Label);
        // }

        var cliTable = new CliTable();

        cliTable.Render(listaTabela);
    }


}
