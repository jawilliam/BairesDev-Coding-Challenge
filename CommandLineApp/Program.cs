using CommandDotNet;
using System;

namespace CommandLineApp
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                return new AppRunner<PeopleCommands>().Run(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return -1;
        }
    }
}
