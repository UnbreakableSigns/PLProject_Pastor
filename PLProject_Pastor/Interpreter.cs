using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLProject_Pastor
{
    class Interpreter
    {
        string[] keywords = { "read", "write", "len", "sum" ,"if"};
        string[] datatypes = { "#", "@", "$", "~" };
        public Interpreter() { }

        Regex arithmetics = new Regex(@"([-+]?[0-9]*\.?[0-9]+[\/\+\-\*])+([-+]?[0-9]*\.?[0-9]+)");
        Dictionary<string, string> stringType = new Dictionary<string, string>();
        Dictionary<string, double> numberType = new Dictionary<string, double>();

        string output;
        void read(string code) { 
            output = "";
            stringType = new Dictionary<string, string>();
            numberType = new Dictionary<string, double>();
            string[] splitArray = Regex.Split(code, @"(?:,\s+)");
            List<string> statements = new List<string>();

            bool consoleIsOpen = false;

            for (int i = 0; i < splitArray.Length; i++)
            {
                statements.AddRange(splitArray[i].Split('\n', '\t', '='));
            }

            for (int i = 0; i < statements.Count; i++)
            {
                //token: token
                List<string> tokens = new List<string>();
                tokens.AddRange(statements.ElementAt(i).Split(':'));
            }

                if (consoleIsOpen == true)
            {
                ConsoleOutput.WriteConsole("\n\n--------------------------------------------------------------------------------\nPress any key to continue. . .");
                ConsoleOutput.ReadLineConsole();
                ConsoleOutput.CloseConsole();
                consoleIsOpen = false;
            }
        }

        /* public int SkipThisScope()
         {
             string[] cmd;
             string buffer;
             int count = 0;
             int statement = 0;
             lineCode++;
             while (sCode.Count > lineCode)
             {
                 buffer = (string)sCode[lineCode];
                 buffer = buffer.Replace("\t", String.Empty);
                 buffer = buffer.TrimStart();
                 cmd = buffer.Split(new string[] { " ", " \n" }, StringSplitOptions.RemoveEmptyEntries);
                 switch (cmd[0])
                 {
                     case "begin":
                         {
                             if (statement == 0)
                                 statement++;
                             else
                                 count++; break;
                         }

                     case "end":
                         {
                             if (count == 0) return 1;
                             count--;
                         }
                         break;

                     case "{":
                         {
                             if (statement == 0)
                                 statement++;
                             else
                                 count++; break;
                         }

                     case "}":
                         {
                             if (count == 0) return 1;
                             count--;
                         }
                         break;


                     case "if": count++; break;
                     case "else":
                         {
                             if (count == 0) return 1;
                             count--;
                         }
                         break;
                     case "endif":
                         {
                             if (count == 0) return 1;
                             count--;
                         }
                         break;

                     /* case "while":
                          //{
                             //count++; break;
                          //}
                          //*
                     case "endwhile":
                         {
                             if (count == 0) return 1;
                             count--;
                         }
                         break;
                 }
                 lineCode++;
             }
             return 1;
         }
     */
    }
}
