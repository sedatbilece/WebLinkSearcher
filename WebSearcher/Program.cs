using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using WebSearcher;
using System.Reflection;

var url = "https://teknobird.com/kaliteli-corek-otu-yagi/";
string domain = "arifoglu.com";
string json = "";

var customReader = new CustomReader();

var liste = await customReader.GetConnectionJsonAsync(url);

var message = liste[0];
Console.WriteLine("Status : " + message + "\n---------\n");

json = liste[1];

List<SearchItem> result = new List<SearchItem>();


result = customReader.ReadJson(json,domain);


foreach (var item in result)
{
    Console.WriteLine(nameof(SearchItem.url) + " : " + item.url + "\n");
    Console.WriteLine(nameof(SearchItem.urlSection) + " : " + item.urlSection + "\n");
    Console.WriteLine(nameof(SearchItem.Text) + " : " + item.Text + "\n");

    Console.WriteLine("---------\n");
}