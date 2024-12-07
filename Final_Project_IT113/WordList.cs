using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Final_Project_IT113
{
    internal class WordList
    {
        public string BaseString { get; set; }
        public List<string> Words { get; set; }
        public Dictionary<string, int> FreqDist { get; set; }
        //my stop word list is very short in comparison to class, but this was all that I felt that I needed for my default url in program
        public static List<string> StopWords = new List<string>(["", "  ", " ", "and", "the", "a"]);
        public WordList(string baseString)
        {
            BaseString = baseString;
            Words = new List<string>();
            FreqDist = new Dictionary<string, int>();

            //I made minor adjustments to when removestopwords is called because it seemed to make a difference for when removing empty space from my dictionary keys
            foreach(string token in WordList.RemoveStopWords(RemoveSpecialCharacters(BaseString)).ToLower().Split(" "))
            {
                if (FreqDist.ContainsKey(token))
                {
                    FreqDist[token]++;
                }
                else
                {
                    FreqDist.Add(token, 1);
                }
            }
            //I feel like there is an easier way of doing this, but making a list of keys, and a list of values was the only way I could figure out how to make a list 
            //of strings where each string is the combination of value followed by key
            List<int> WordCount = FreqDist.Values.ToList();
            List<string> Word = FreqDist.Keys.ToList();

            foreach (int count in WordCount)
            {
                Words.Add(count.ToString() + " " + Word.ToString());

            }
        }
        public Dictionary<string, int> OrderDescending()
        {
            return FreqDist.OrderByDescending(x => x.Value).ToDictionary();
        }
        public string RemoveSpecialCharacters(string str)
        {
            //I made all special characters that are removed be replaced with " " instead of "" to prevent words from being mixed with html elements in my dictionary keys
            return Regex.Replace(str, "[^a-zA-Z0-9\\s]+", " ", RegexOptions.Compiled);
        }
        public static string RemoveStopWords(string str)
        {
            List<string> result = new List<string>();

            result.AddRange(str.Split(" "));

            result = result.Where(w => !WordList.StopWords.Contains(w)).ToList();

            return String.Join(" ", result);
        }
    }
}
