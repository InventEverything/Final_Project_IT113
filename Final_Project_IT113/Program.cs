using System.Net;

namespace Final_Project_IT113
{
    internal class Program
    {

        //Which structure/algorithm/pattern you have chosen.
            //I have decided to go with frequqncy distribution.
        //What your project aims to demonstrate or solve.
            //My hope was to be able to put in any web site address and receive a frequency distribution of the words used on the page. 
            //This is partly helpful for me personaly to see a summary of which html elements are used most frequently (div, span, li, etc)
        //In what situations would your design fail to meet expectations.
            //I would need to do a lot of adjustments to make the information more useful. Right now my default address results as desired, but more complicated web sites 
            //would require a lot more filtering. I also noticed that on line 54 when waiting for readkey it seems that any key works except for the "esc" key, esc seems to break
            //my infiniute loop.
            //I also noticed that console.clear() does not seem to clear the whole console if there is to many lines printed to the console. I am not sure how to fully clear the console.
            //Some sites seem to return 403 forbidden error.
        //In what situations would your design exceed expectations.
            //I am happy to have figured out (with the help of stackoverflow.com) how to use the url parts of my code

        static void Main(string[] args)
        {
            do
            {
                string url = "http://dog-api.kinduff.com/api/facts";
                Console.WriteLine("Please enter a web address or press enter to use the default value: "+url);
                do
                {
                    string userInput = Console.ReadLine();
                    //if a url is added by the user
                    if (userInput != "")
                    {
                        url = userInput;
                    }
                    //if the url is invalid
                    if(!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid entry\nPlease enter a valid web address, for example: http://dog-api.kinduff.com/api/facts");
                    }
                    else
                    {
                        Console.Clear();
                        break;
                    }
                } while (true);

                WebClient SiteSearch = new WebClient();
                SiteSearch.BaseAddress = url;
                string returnJson = SiteSearch.DownloadString(url);

                WordList wordList = new WordList(returnJson);
                Dictionary<string, int> OrderedList = wordList.OrderDescending();
                Console.WriteLine("\n"+url+"\nFrequency : Word");
                foreach (KeyValuePair<string, int> word in OrderedList)
                {
                    Console.WriteLine(word.Value.ToString() + " : " + word.Key.ToString());
                }
                Console.WriteLine("\nPress any key to start over");
                Console.ReadKey();
                Console.Clear();
            } while (true);
        }
    }
}
