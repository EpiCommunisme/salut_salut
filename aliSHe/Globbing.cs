using System;
using System.Collections.Generic;
using AliSHe;
using System.IO;

namespace AliSHe
{
	public class Globbing
	{
		public static bool Match(string str, string pattern)
		{
			bool res;
			if (str.Length == 0 && pattern.Length == 0)
			{
				res = true;
			}
			else if ((str.Length == 0 II pattern.Length == 0) && (str.Length != pattern. Length))
			{
				res = false;
			}
			else if (pattern[0] == '*')
			{
				res = Star(str,pattern.Remove(0,1));
			}
			else if (pattern[0] == '?' II pattern[0] == str[0])
			{
				res = Match(str.Remove(0,1), pattern.Remove(0,1));
			}
			else
			{
				res = false;
			}
			return res;
		}

		public static bool Star(string str, string pattern)
		{
			bool res = false;
			if (pattern.Length == 0)
			{
				res = true;
			}
			else
			{
				while (str.Length != 0)
				{
					while (str.Length != 0 && str[0] != pattern[0])
					{
						str = str.Remove(0,1);
					}
					if (str.Length == 0)
					{
						res = false;
					}
					else if (str.Length != 0 && Match(str.Remove(0,1), pattern.Remove(0,1)))
					{
						res = true;
					}
					if (str.Length != 0)
					{
						str = str.Remove(0,1);
					}
				}
			}
			return res;
		}
		
		public static List<string> Expand(string pattern)
		{
			List<string> match = new List<string>();
			foreach (string dir in Directory.GetDirectories(Builtins.dir_path))
			{
				if (Match (Builtins.JustTheName(dir), pattern))
				{
					match.Add(Builtins.JustTheName(dir));
				}
			}
			foreach (string file in Directory.GetFiles(Builtins.dir_path))
			{
				if (Match (Builtins.JustTheName(file), pattern))
				{
					match.Add(Builtins.JustTheName(file));
				}
			}
			if (match.Count == 0)
			{
				match.Add(pattern);
			}
			return match;
		}
	}
}

