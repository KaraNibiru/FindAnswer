// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hi, send me your massive before. Use dots or comma between numbers:");
string line = Console.ReadLine();

IEnumerable<int> mass = line.Replace(".", ",").Replace(" ", "").Split(",").Select(o => Convert.ToInt32(o));

Dictionary<string,int> summary = GetSummary(mass);

Console.Write("Now send me number which you need check for summary: ");
int number = 0;

while (number > -1)
{
    number = Convert.ToInt32(Console.ReadLine());

    if (summary.ContainsValue(number))
    {
        Console.WriteLine("Yes, we found out how you can get this:");
        foreach (var result in summary.Where(x => x.Value == number))
            Console.WriteLine(result.Key + " = " + number);

    }
    else
    {
        Console.WriteLine("No, we cant do that :(");
    }
    Console.WriteLine("We can try it again, just send me new number or type -1 for exit");
}

Dictionary<string, int> GetSummary(IEnumerable<int> items)
{
    IEnumerable<KeyValuePair<string, int>> mass =
        items.Select(x => KeyValuePair.Create(x.ToString(), x));
    IEnumerable<KeyValuePair<string, int>> summary = GetSummaryCount(mass, 1);

    for (int i = 2; i <= items.Count(); i++)
        summary = summary.Concat(GetSummaryCount(mass, i));

    Dictionary<string, int> result = new Dictionary<string, int>();
    foreach (var sum in summary)
        if (!result.ContainsKey(sum.Key))
            result.Add(sum.Key, sum.Value);

    return result;
}

IEnumerable<KeyValuePair<string, int>> GetSummaryCount(IEnumerable<KeyValuePair<string, int>> items, int count)
{
    int i = 0;
    foreach (var item in items)
    {
        if (count == 1)
            yield return item;
        else
        {
            foreach (var result in GetSummaryCount(items.Skip(i + 1), count - 1))
                yield return new KeyValuePair<string, int>
                    (item.Key + "+" + result.Key,
                     item.Value + result.Value);
        }
        i++;
    }
}
