using System;
using System.Collections.Generic;
using System.Diagnostics;
using AliSHe;

namespace AliSHe
{ 
  public class AliSHe
  {
	public static string input;
	public static List<string> GetCommand()
	{
		int i = 0;
		string arg;
		List<string> commands = new List<string>();
		while (i < input.Length)
		{
			arg= "";
			while (i < input.Length && input[i] != ' ')
			{
				arg += input[i];
				i++;
			}
			commands.Add(arg);
			i++;
		}
		return commands;
	}
	public static List<string> GetArgs(List<string> command)
	{
		List<string> res = command;
		res.RemoveAt(0);
		return res;
	}
	public static void Exec(string program, List<string> arguments)
	{
		Process process = new Process();
		process.StartInfo.FileName = "/bin/bash";
		string processarguments = "-c \"" + program +" ";
		foreach (string pattern in arguments)
		{
			processarguments += pattern + " ";
		}
		processarguments += "\"";
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.RedirectStandardOutput = true, 
		process.StartInfo.WorkingDirectory = Builtins.dir_path;
		process.StartInfo.Arguments = processarguments;
		process.Start();
		process.WaitForExit();
	}
	public static void Run()
	{
		string command = "";
		do
		{
			string current_dir = Builtins.dir_path;
			Console.Write("[aliSHe - {0} $] ", Builtins.JustTheName(current_dir));
			input = Console.ReadLine();
			command = "";
			List<string> inputs = GetCommand();
			List<string> arguments = new List<string>();
			if (inputs.Count != 0)
			{
				command = inputs[0];
				arguments = GetArgs(inputs);
			}
			List<string> args = new List<string>();
			foreach (string pattern in arguments)
			{
				foreach(string matched in Globbing.Expand(pattern))
				{
					args.Add(matched);
				}
			}
			try
			{
				if (command == "ls")
				{
					Builtins.Ls(args);
				}
				else if (command == "cd")
				{
				Builtins.Cd(args);
				}
				else if (command == "echo")
				{
					Builtins.Echo(args);
				}
				else if (command == "pwd")
				{
					Builtins.Pwd(args);
				}
				else if (command == "cat")
				{
					Builtins.Cat(args);
				}
				else if (command != "exit")
				{
					Exec(command, args);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}while (command != "exit");
		}
	}
}