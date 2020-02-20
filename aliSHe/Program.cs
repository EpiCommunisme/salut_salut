using System;
using System.Collections.Generic;

namespace AliSHe
{
    public class Program
    {
        public static void Main(string[] argv)
        {
            AliSHe.Run();
        }

        private static void CheckFunctions()
        {
            // You do not need to call this function, you just need to make
            // sure that it compiles.
            try
            {
                List<string> arr = new List<string>();

                //Builtins
                Builtins.Pwd(arr);
                Builtins.Cd(arr);
                Builtins.Ls(arr);
                Builtins.Echo(arr);
                Builtins.Cat(arr);

                //Globbing
                Globbing.Match("", "");
                Globbing.Star("", "");
                Globbing.Expand("");

                //AliSHe
                AliSHe.GetCommand();
                AliSHe.GetArgs(arr);
                AliSHe.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
