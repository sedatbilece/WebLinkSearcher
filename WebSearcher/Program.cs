using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using WebSearcher;
using System.Reflection;

var url = "https://www.bandirma.com.tr/kadin-esofman-takimi/";
string domain = "damlabutik.com.tr";
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
    Console.WriteLine(nameof(SearchItem.Url) + " : " + item.Url + "\n");
    Console.WriteLine(nameof(SearchItem.LandingPage) + " : " + item.LandingPage + "\n");
    Console.WriteLine(nameof(SearchItem.Anchor) + " : " + item.Anchor + "\n");

    Console.WriteLine("---------\n");
}