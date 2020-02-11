using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Threading;

namespace YouAndCthulhu
{
    public class Function
    {
        // A Function contains an id, composed of a natural and a character,
        // a list of Actions, which are kinds of lambda expressions, and a
        // register.
        private UInt64 idnum;
        private char idchar;
        private List<Action> execution;
        private int accumulator;

        public Function(UInt64 idnum, char idchar, string commands, 
                        FunctionTable ftable)
        {
            this.idnum = idnum;
            this.idchar = idchar;
            accumulator = 0;
        }

        // Getters
        public ulong Idnum => idnum;
        public char Idchar => idchar;
        public int Accumulator => accumulator;

        // Methods implements Function Commands as seen on Esolang.
        public void Increment()
        {
            accumulator++;
        }

        public void Decrement()
        {
            accumulator--;
        }

        public void MoveOut(Function f)
        {
            f.accumulator = accumulator;

        }

        public void MoveIn(Function f)
        {
            accumulator = f.accumulator;
        }

        public void Output()
        {
            Console.WriteLine(accumulator);
        }

        public void Input()
        {
            Console.Write("Waiting for integer input: ");
            string s = Console.ReadLine();
            int l = s.Length;
            for (int i = 0; i < l; i++)
            {
                if (!Char.IsDigit(s[i]))
                {
                    throw new ArgumentException("Faut mettre un int bro");
                }
            }

            accumulator += Convert.ToInt32(s);
        }
        
        public void Execute()
        {
            int l = execution.Count;
            for (int i = 0; i < l; i++)
            {
                execution[i]();
            }
        }
        
        private void ParseCommands(string commands, FunctionTable ftable)
        // Given the command string and the ftable, parses commands by creating
        // a lambda expression and adding it to the list every time.
        {
            int index = 0; 
            
            while (index < commands.Length)
            {
                Action act;
                switch (LexerParser.CommandLexer(commands, index++))
                {
                    case LexerParser.CommandToken.TOKEN_INCREMENT:
                    {
                        throw new NotImplementedException("Do it");
                    }
                    case LexerParser.CommandToken.TOKEN_DECREMENT:
                    {
                        throw new NotImplementedException("Do it");
                    }
                    case LexerParser.CommandToken.TOKEN_OUTPUT:
                    {
                        throw new NotImplementedException("Do it");
                    }
                    case LexerParser.CommandToken.TOKEN_INPUT:
                    {
                        throw new NotImplementedException("Do it");
                    }
                    case LexerParser.CommandToken.TOKEN_MOVE_IN:
                    {
                        throw new NotImplementedException("Do it");
                    }
                    case LexerParser.CommandToken.TOKEN_MOVE_OUT:
                    {
                        throw new NotImplementedException("Do it");
                    }
                    case LexerParser.CommandToken.TOKEN_CALL:
                    {
                        // calling a function simply consists in executing it...
                        throw new NotImplementedException("Do it");
                    }
                    case LexerParser.CommandToken.TOKEN_CALL_ACC:
                    {
                        throw new NotImplementedException("Do it");
                    }
                    default:
                    {
                        throw new Exception("Unexpected token in parsing");
                    }
                }

                execution.Add(act);
            }
        }
        
    }
}