﻿using System;
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
        string[] keywords = { "read", "write", "len", "sum", "if" };
        string[] datatypes = { "#", "@", "$", "~" };
        public Interpreter() { }

        Regex arithmetics = new Regex(@"([-+]?[0-9]*\.?[0-9]+[\/\+\-\*])+([-+]?[0-9]*\.?[0-9]+)");
        Dictionary<string, string> stringType = new Dictionary<string, string>();
        Dictionary<string, double> numberType = new Dictionary<string, double>();
        Dictionary<string, List<double>> listType = new Dictionary<string, List<double>>();
        Dictionary<string, bool> boolType = new Dictionary<string, bool>();

        string output;
        public void read(string code)
        {
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
                tokens[0] = tokens[0].Trim();
                tokens[1] = tokens[1].Trim();

                if (tokens[0].Trim().StartsWith(datatypes[0]))//number
                {
                    numberType.Add(tokens[0], int.Parse(tokens[1]));
                }
                else if (tokens[0].Trim().StartsWith(datatypes[1]))//list
                {
                    List<double> listOfDouble = new List<double>();
                    //TODO:
                    //split tokens[1] into list of numbers
                    listType.Add(tokens[0], listOfDouble);
                }
                else if (tokens[0].Trim().StartsWith(datatypes[2]))//string
                {
                    stringType.Add(tokens[0], tokens[1]);
                    MessageBox.Show("[" +tokens[0] + "] added to string [" + tokens[1] + "]");
                }
                else if (tokens[0].Trim().StartsWith(datatypes[3]))//bool
                {
                    boolType.Add(tokens[0], bool.Parse(tokens[1]));
                }
                if (tokens[0].Contains(keywords[0]))//read
                {
                    if (consoleIsOpen == false)
                    {
                        ConsoleOutput.OpenConsole();
                        consoleIsOpen = true;
                    }
                    string value = ConsoleOutput.ReadLineConsole();
                    if (stringType.ContainsKey(tokens[0]))
                    {
                        stringType.Remove(tokens[0]);
                        stringType.Add(tokens[0], value);
                    }
                }
                else if (tokens[0].Contains(keywords[1]))//write
                {
                    if (consoleIsOpen == false)
                    {
                        ConsoleOutput.OpenConsole();
                        consoleIsOpen = true;
                    }
                    if (tokens[1].StartsWith("\'"))
                    {
                        output = tokens[1].Substring(1, tokens[1].Length - 2) + "\n";
                        ConsoleOutput.WriteConsole(output);
                    }
                    else if (stringType.ContainsKey(tokens[0].Trim()))
                    {
                        MessageBox.Show("pasok");
                        if (stringType[tokens[1]].Trim().ToString().StartsWith("\'"))
                        {
                            output = stringType[tokens[1]].Trim().ToString().Substring(1, stringType[tokens[1]].Trim().ToString().Length - 2) + "\n";
                            ConsoleOutput.WriteConsole(output);
                        }
                        else
                        {
                            output = stringType[tokens.ElementAt(1)] + "\n";
                            ConsoleOutput.WriteConsole(output);
                        }
                    }
/*                    else if (numberType.ContainsKey(tokens.ElementAt(i + 1)))
                    {
                        output = numberType[tokens.ElementAt(i + 1)] + "\n";
                        ConsoleOutput.WriteConsole(output);
                    }
                    */
                }



                if (consoleIsOpen == true)
                {
                    ConsoleOutput.WriteConsole("\n\n--------------------------------------------------------------------------------\nPress any key to continue. . .");
                    ConsoleOutput.ReadLineConsole();
                    ConsoleOutput.CloseConsole();
                    consoleIsOpen = false;
                }
            }

        }
    }
}