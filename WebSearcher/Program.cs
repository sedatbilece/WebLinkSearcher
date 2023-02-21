using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using WebSearcher;
using System.Reflection;

var url = "https://teknobird.com/kaliteli-corek-otu-yagi/";
string json = "";
string IsSuccessStatusCode = "";

try
{
    var client = new HttpClient();

    var checkingResponse = await client.GetAsync(url);
    if (checkingResponse.IsSuccessStatusCode)
    {
        Console.WriteLine("Bağlantı Başarılı ");
        IsSuccessStatusCode= checkingResponse.IsSuccessStatusCode.ToString();
        Console.WriteLine(IsSuccessStatusCode);

    } else {
        Console.WriteLine("Bağlantıda hata var");
    }
    var stream = client.GetStreamAsync(url).Result;
    var reader = new StreamReader(stream);
    json = reader.ReadToEnd();
    reader.Close();

}
catch(Exception e)
{
    Console.WriteLine($"Error : {e.Message}");
}



List<SearchItem> result = new List<SearchItem>();

 static void ReadJson(string inputString, List<SearchItem> result)
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


ReadJson(json, result);


foreach(var item in result)
{
        Console.WriteLine(nameof(SearchItem.url) + " : " + item.url + "\n");
        Console.WriteLine(nameof(SearchItem.urlSection) + " : " + item.urlSection + "\n");
        Console.WriteLine(nameof(SearchItem.Text) + " : " + item.Text + "\n");

    Console.WriteLine("---------\n");
    }

