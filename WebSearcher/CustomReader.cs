using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebSearcher
{
    public class CustomReader
    {
        private HttpClient _client { get; set; }

        List<string> liste { get; set; } = new List<string>();
        List<SearchItem> result { get; set; } = new List<SearchItem>();
        public CustomReader()
        {

            _client = new HttpClient();
        }





        public async Task<List<string>> GetConnectionJsonAsync(string url)
        {
            string json = "";

            try
            {

                var checkingResponse = await _client.GetAsync(url);

                if (checkingResponse.IsSuccessStatusCode)
                {
                    liste.Add(checkingResponse.StatusCode.ToString());
                }
                else
                {
                    liste.Add(checkingResponse.StatusCode.ToString());
                }
                var stream = _client.GetStreamAsync(url).Result;
                var reader = new StreamReader(stream);
                json = reader.ReadToEnd();
                reader.Close();

            }
            catch (Exception e)
            {
                liste.Add(e.Message);
            }
            liste.Add(json);

            return liste;
        }


        public List<SearchItem> ReadJson(string inputString)
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
            return result;
        }
    }
}
