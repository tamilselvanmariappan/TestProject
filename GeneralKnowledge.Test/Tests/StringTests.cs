using System;
using System.Linq;

namespace GeneralKnowledge.Test.App.Tests
{
    /// <summary>
    /// Basic string manipulation exercises
    /// </summary>
    public class StringTests : ITest
    {
        public void Run()
        {
            // TODO
            // Complete the methods below

            AnagramTest();
            GetUniqueCharsAndCount();
        }

        private void AnagramTest()
        {
            var word = "stop";
            var possibleAnagrams = new string[] { "test", "tops", "spin", "post", "mist", "step" };

            Console.WriteLine("Anagram Test");

            foreach (var possibleAnagram in possibleAnagrams)
            {
                Console.WriteLine(string.Format("{0} > {1}: {2}", word, possibleAnagram, possibleAnagram.IsAnagram(word)));
            }
        }

        private void GetUniqueCharsAndCount()
        {
            var word = "xxzwxzyzzyxwxzyxyzyxzyxzyzyxzzz";

            // TODO
            // Write an algorithm that gets the unique characters of the word below 
            // and counts the number of occurrences for each character found
            Console.WriteLine("\n\nGet Unique Chars And Count");
            Console.WriteLine(word);
            var obj = word.GroupBy(x => x).Select(x => new
            {
                Key = x.Key.ToString(),
                Value = x.Count()
            }).ToDictionary(t => t.Key, t => t.Value);
            foreach (var t in obj)
            {
                Console.WriteLine($"{ t.Key} has occurred { t.Value} times ");
            }
        }
    }

    public static class StringExtensions
    {
        public static bool IsAnagram(this string a, string b)
        {
            return a.ToCharArray().OrderBy(c => c).SequenceEqual(b.OrderBy(c => c));
        }
    }
}
