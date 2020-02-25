Builtins.cs 
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace AliSHe
{
	  public class Builtins
	  {
		public static string dir_path = Directory.GetCurrentDirectory();
		
		public static void Pwd(List<string> args)
		{
			if (args.Count == 0)
			{
				Console.WriteLine(dir_path);
			}
			else
			{
				throw new ArgumentException("pwd: too many arguments");
			}
		}
		
		public static void Cd(List<string> args)
		{
			string arg;
			if (args.Count == 0)
			{
				dir_path = "/home/"+Environment.UserName;
			}
			else if (args.Count == 1)
			{
				if (args[0] == "-I" 11 args[0] == "~")
				{
					arg = "/home/"+Environment.UserName;
				}
				else
				{
					arg = args[0];
				}
				if (Directory.Exists(Path.GetFullPath(args[0])))
				{
					dir_path = Path.GetFullPath(args[0]);
				}
				else if (File.Exists(Path.GetFullPath(args[0])))
				{
					throw new ArgumentException("cd: {0}: Not a directory",args[0]);
				}
				else
				{
					throw new ArgumentException("cd: {0}: No such file or directory",args[0]);
				}
			}
			else if (args.Count > 1)
			{
				throw new ArgumentException("cd: too many arguments");
			}
		}
		
		public static void Echo(List<string> args)
		{
			if (args.Count != 0)
			{
				Console.Write(args[0]);
				for (int i = 1; i < args.Count; i++)
				{
					Console.Write("{0}",args[i]);
				}
			}
			Console.WriteLine();
		}
		
		public static string JustTheName(string path)
		{
			int i = path.Length - 1;
			string res = "";
			while ( i >= 0 && Char.ToString(path[i]) != "/" && Char.ToString(path[i]) != "\\")
			{
				res = path[i] + res; i--;
			}
			return res;
		}
		
		public static void Ls(List<string> args)
		{
			string[] directories;
			string[] files;
			string dir;
			if (args.Count == 0)
			{
				directories = Directory.GetDirectories(dir_path);
				if (directories.GetUpperBound(0) >= 0)
				{
					for (int i = 0; i < directories.GetUpperBound(0); i++)
					{
						Console.Write("{0} ", JustTheName(directories[i]));
					}
				Console.WriteLine(JustTheName(directories[dire ctories.GetUpperBound(0)]));
				}
				files = Directory.GetFiles(dir_path);
				if (files.GetUpperBound(0) >= 0)
				{
					for (int i = 0; i < files.GetUpperBound(0); i++)
					{
						Console.Write"{0} ", JustTheName(files[i]);
					}
					Console.WriteLine(JustTheName(files[files.GetUpperBound(0)]));
				}
			}
			else
			{
				for (int i = 0; i < args.Count; i++)
				{
					if (Directory.Exists(Path.GetFullPath(args[i])))
					{
						dir = Path.GetFullPath(args[i]);
						Console.WriteLine("{0} :", JustTheName(dir));
						directories = Directory.GetDirectories(Path.GetFullPath(args [i]));
						if (directories.GetUpperBound(0) >= 0)
						{
							for (int j = 0; j < directories.GetUpperBound(0); j++)
							{
								Console.Write(" {0} ", JustTheName(directories[j]));
							}
						Console.WriteLine(JustTheName(directories[dire ctories.GetUpperBound(0)]));
						}
						files = Directory.GetFiles(Path.GetFullPath(args[i]));
						if (files.GetUpperBound(0) >= 0)
						{
							for (int j = 0; j < files.GetUpperBound(0); j++)
							{
								Console.Write("{0} ", JustTheName(files[j]));
							}
						Console.WriteLine(JustTheName(files[files.GetU pperBound(0)]));
						}
					}
					else if (File.Exists(Path.GetFullPath(args[i])))
					{
						Console.WriteLine(JustTheName(args[i]));
					}
					else
					{
						throw new ArgumentException("ls: cannot '{0}': No such file or directory", args[i]);
					}
				}
			}
		}
			
		public static void Cat(List<string> args)
		{
			if (args.Count == 0)
			{
				throw new ArgumentException("cat : no arguments given");
			}
			else
			{
				for (int i = 0; i < args.Count; i++)
				{
					if (Directory.Exists(args[i]))
					{
						throw new ArgumentException("cat : {0}: Is a directory", JustTheName(args[i]));
					}
					else if (File. Exists(args[i]))
					{
						foreach (string line in File.ReadAllLines(args[i]))
						{
							Console.WriteLine(line);
						}
					}
					else
					{
						throw new ArgumentException ("cat: {0}: No such file or directory", args[i])
					}
				}
			}
		}
	}
}