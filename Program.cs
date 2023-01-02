// See https://aka.ms/new-console-template for more information
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;

Console.WriteLine("Markov chain name generator");

string[] names;
List<string> beginnings = new() ;
var order = 3;
var size = 15;
var cycle = 1;
Dictionary<string, List<string>> ngrams = new () ;


names = File.ReadLines("names.txt").ToArray();

if (args.Length == 3)
{
    Int32.TryParse(args[0], out order);
    Int32.TryParse(args[1], out size);
    Int32.TryParse(args[2], out cycle);

}
  string generateName()
{
    Random rnd = new Random();
    var currentGram = beginnings[rnd.Next(beginnings.Count)];

    var result = currentGram;

    for (int i = 0; i < size; i++)
    {



        if (!ngrams.ContainsKey(currentGram)) { break; }

        var possibilities = ngrams[currentGram];
        var next = possibilities[rnd.Next(possibilities.Count)];
        result += next;
        var len = result.Length;
        currentGram = result.Substring(len - order);
    }
    return result;
}
for (int j = 0; j < names.Count(); j++)
{
    var txt = names[j];
    for (var i = 0; i < txt.Length - order  ; i++)
    {
        var gram = txt.Substring(i, order);
        if (i == 0) beginnings.Add(gram);
        // ngrams.push(gram);
        if (!ngrams.ContainsKey(gram))
        {
            List<string> markov = new();
            markov.Add(txt.Substring(i + order, 1));

            ngrams.Add(gram, markov); 
            

        }
        else
        {

            ngrams[gram].Add(txt.Substring(i + order,1));
        }
    }

    
}






//Console.WriteLine(JsonSerializer.Serialize(beginnings));
//Console.WriteLine();
//Console.WriteLine(JsonSerializer.Serialize(ngrams));

for (int i = 0; i < cycle; i++) Console.WriteLine(generateName());