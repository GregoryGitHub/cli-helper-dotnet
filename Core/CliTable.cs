namespace CliHelperDotnet.Core;

public class CliTable
{
    public void Render(IEnumerable<Dictionary<string, string>> list)
    {
        var columns = list.First().Keys.ToList();
        var columnWidths = new int[columns.Count];

        for (var i = 0; i < columns.Count; i++)
        {
            columnWidths[i] = columns[i].Length;
        }

        foreach (var row in list)
        {
            for (var i = 0; i < columns.Count; i++)
            {
                if (row[columns[i]].Length > columnWidths[i])
                {
                    columnWidths[i] = row[columns[i]].Length;
                }
            }
        }

        var totalWidth = columnWidths.Sum() + columnWidths.Length * 3 + 1;

        Console.WriteLine(new string('-', totalWidth));

        for (var i = 0; i < columns.Count; i++)
        {
            Console.Write($"| {columns[i].PadRight(columnWidths[i])} ");
        }

        Console.WriteLine("|");

        Console.WriteLine(new string('-', totalWidth));

        foreach (var row in list)
        {
            for (var i = 0; i < columns.Count; i++)
            {
                Console.Write($"| {row[columns[i]].PadRight(columnWidths[i])} ");
            }

            Console.WriteLine("|");
        }

        Console.WriteLine(new string('-', totalWidth));
        
    }

}