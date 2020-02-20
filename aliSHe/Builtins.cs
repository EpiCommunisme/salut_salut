using System;
using System.IO;
using System.Collections.Generic;

namespace AliSHe
{
        public class Builtins
        {
                public static void Main(string[] argv)
                {
                        List<string> lst = new List<string>();
                        foreach (string s in argv)
                                lst.Add(s);
                        Echo(lst);
                }
                public static void Pwd(List<string> args)
                {
                        if (args.Count == 0)
                        {
                                Console.WriteLine(Directory.GetCurrentDirectory());
                        }
                        else
                        {
                                throw new ArgumentException("pwd: too many arguments");
                        }
                }
                public static void Cd (List<string> args)
                {
                        string dirAct = "";
                        if (args.Count == 0)
                        {
                               dirAct = "/home" + Environment.UserName;
                        }
                        else
                        {
                                if (args.Count == 1)
                                {
                                        if(Directory.Exists(args[0]) || File.Exists(args[0]))
                                        {
                                                dirAct += args [0];
                                        }
                                        else
                                        {
                                                if (Directory.Exists(args[0]))
                                                {
                                                        throw new ArgumentException ("cd: " + args[0] + ": Not a directory");
                                                }
                                                 throw new ArgumentException ("cd: " + args [0] + ": No such file or directory");
                                        }
                                }
                                else
                                {
                                         throw new ArgumentException ("cd: too many arguments");
                                }

                         }

                }


                public static void Echo(List<string> args)
                {
                        if (args.Count == 0)
                        {
                                Console.WriteLine();
                        }
                        else
                        {
                                String str = args[0];
                                int i = 1;
                                while (i < args.Count)
                                {
                                        str += " " + args[i];
                                        i++;
                                }
                                Console.WriteLine(str);
                        }
                }
               

        }
}
