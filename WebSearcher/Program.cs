using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using WebSearcher;

var url = "https://teknobird.com/kaliteli-corek-otu-yagi/";
var client = new WebClient();
var stream = client.OpenRead(url);
var reader = new StreamReader(stream);
string json = reader.ReadToEnd();
reader.Close();




List<SearchItem> result = new List<SearchItem>();

 static void DumpHRefs(string inputString, List<SearchItem> result)
{
    string hrefPattern = "<a\\s*(.*)\\>s*(.*)</a>";

    try
    {
        Match regexMatch = Regex.Match(inputString, hrefPattern,
                                       RegexOptions.None | RegexOptions.None,
                                       TimeSpan.FromSeconds(1));
        while (regexMatch.Success)
        {
            if (regexMatch.Value.Contains("arifoglu.com"))
            {
                var list = regexMatch.Groups[1].Value.Split("//www.arifoglu.com/");
                var list2 = regexMatch.Groups[1].Value.Split($"href=\"");
                SearchItem search = new SearchItem();
                search.Text = regexMatch.Groups[2].Value;
                search.urlSection = list[1].Trim('"');
                search.url = list2[1].Trim('"');

                result.Add(search);
            }
            
            regexMatch = regexMatch.NextMatch();
        }
    }
    catch (RegexMatchTimeoutException)
    {
        Console.WriteLine("The matching operation timed out.");
    }
}


DumpHRefs(json, result);


foreach(var item in result)
{
        Console.WriteLine(nameof(SearchItem.url) + " : " + item.url + "\n");
        Console.WriteLine(nameof(SearchItem.urlSection) + " : " + item.urlSection + "\n");
        Console.WriteLine(nameof(SearchItem.Text) + " : " + item.Text + "\n");

    Console.WriteLine("---------\n");
    }

