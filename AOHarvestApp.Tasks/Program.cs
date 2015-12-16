using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOHarvestApp.Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null)
            {
                Console.WriteLine("args is null"); // Check for null array
            }
            else
            {
                Console.Write("args length is ");
                Console.WriteLine(args.Length); // Write array length
                for (int i = 0; i < args.Length; i++) // Loop through array
                {
                    string argument = args[i];
                    Console.Write("args index ");
                    Console.Write(i); // Write index
                    Console.Write(" is [");
                    Console.Write(argument); // Write string
                    Console.WriteLine("]");
                }
            }
            Console.ReadLine();

            //if (HarvestManager.GetDayEntries().Select(i => i.Hours).Sum() < 12)
            //{
            //    EmailManager.SendIncompleteDailyEntriesEmail("test@test.com");
            //}
        }
    }
}
