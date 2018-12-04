using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadedMergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            //list of lists so we can easily sort multiple lengths in a loop
            List<List<int>> randomNumberLists = new List<List<int>> { };
            //various lengths of the lists to sort
            int[] lengths = { 8, 64, 256, 1024 };
            int i = 0;
            foreach (var length in lengths)
            {
                randomNumberLists.Add(new List<int> { });
                foreach (var num in Enumerable.Range(0, length))
                {
                    var rand = new Random();
                    randomNumberLists[i].Add(rand.Next(length + 100));
                }
                i++;
            }

            foreach (var list in randomNumberLists)
            {
                Console.Write("===========Length: " + list.Count + "=======================\n");
                Console.Write("///Unthreaded\\\\\n");
                long start = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                Console.Write("     Start: " + start + "\n");
                Sorter.MergeSort(list);
                long end = DateTimeOffset.Now.ToUnixTimeMilliseconds();                Console.Write("     End: " + end + "\n");
                Console.Write("           Time in seconds: " + (end - start) / 1000 + "\n\n");

                Console.Write("///Threaded\\\\\n");
                start = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                Console.Write("     Start: " + start + "\n");
                Sorter.ThreadedMergeSort(list);
                end = DateTimeOffset.Now.ToUnixTimeMilliseconds();                Console.Write("     End: " + end + "\n");
                Console.Write("           Time in seconds: " + (end - start) / 1000 + "\n\n");
            }
            Console.Read();
        }
    }
}
