using System;
using System.Diagnostics;

namespace YouAndCthulhu
{
    public class LexerParser
    {
        public enum CommandToken
        // Tokens used in parsing a function's command line.
              {
                  TOKEN_INCREMENT, //i
                  TOKEN_DECREMENT, //d
                  TOKEN_CALL,      //[
                  TOKEN_CALL_ACC,  //]
                  TOKEN_OUTPUT,    //o
                  TOKEN_INPUT,     //*
                  TOKEN_MOVE_OUT,  //E
                  TOKEN_MOVE_IN,    //e
                  TOKEN_DEFAULT    //error
              }

        public static Function LineToFunction(string s, FunctionTable ftable)
        {
            char[] delimiter = {' '};
            string[] parts = s.Trim().Split(delimiter);
            if (parts.Length != 2)
                throw new Exception("Line does not respect syntax");

            int index = 0;
            ulong idnum = GetIdNatural(parts[0], ref index);
            char idchar = GetIdLetter(parts[0], ref index);
            if (index != parts[0].Length)
                throw new Exception("Wrong line format !");
            Function f = new Function(idnum, idchar, parts[1], ftable);

            return f;
        }

        public static char GetIdLetter(string s, ref int index)
        {
            int l = s.Length;
            for (int i = index; i < l; i++)
            {
                if (Char.IsDigit(s[i]))
                {
                    index += 1;
                    return s[i];
                }
            }
            throw new NotImplementedException("Aucune lettre trouve.. zut!");
        }

        public static UInt64 GetIdNatural(string s, ref int index)
        {
            string res = "";
            int l = s.Length;
            for (int i = index; i < l; i++)
            {
                if (Char.IsDigit(s[i]))
                {
                    index += 1;
                    res += s[i];
                }
                else
                {
                    return Convert.ToUInt64(res);
                }
            }
            throw new NotImplementedException("Aucun nombre trouve.. zut!");
        }

        public static CommandToken CommandLexer(string s, int index)
        // returns CommandToken of the given index.
        {
            if (s[index] == 'i')
            {
                return CommandToken.TOKEN_INCREMENT;
            }

            if (s[index] == 'd')
            {
                return CommandToken.TOKEN_DECREMENT;
            }
            
            if (s[index] == '*')
            {
                return CommandToken.TOKEN_INPUT;
            }
            
            if (s[index] == 'o')
            {
                return CommandToken.TOKEN_OUTPUT;
            }
            
            if (s[index] == '[')
            {
                return CommandToken.TOKEN_CALL;
            }
            
            if (s[index] == ']')
            {
                return CommandToken.TOKEN_CALL_ACC;
            }
            
            if (s[index] == 'e')
            {
                return CommandToken.TOKEN_MOVE_IN;
            }

            if (s[index] == 'E')
            {
                return CommandToken.TOKEN_MOVE_OUT;
            }

            return CommandToken.TOKEN_DEFAULT;
        }
    }
}