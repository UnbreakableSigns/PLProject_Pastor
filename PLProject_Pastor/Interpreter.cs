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
        string[] keywords = { "darllenwch", "ysgrifennu", "hyd", "svm", "os","arall" };// read,write,length,sum,if,else
        string[] datatypes = { "#", "@", "$", "~" };
        public Interpreter() { }

        Regex arithmetics = new Regex(@"([-+]?[0-9]*\.?[0-9]+[\/\+\-\*])+([-+]?[0-9]*\.?[0-9]+)");
        Dictionary<string, string> stringType = new Dictionary<string, string>();
        Dictionary<string, double> numberType = new Dictionary<string, double>();
        Dictionary<string, List<double>> listType = new Dictionary<string, List<double>>();
        Dictionary<string, bool> boolType = new Dictionary<string, bool>();

        string output;
        bool consoleIsOpen = false;

        public void read(string code)
        {
            consoleIsOpen = false;
            stringType.Clear();
            numberType.Clear();
            boolType.Clear();
            listType.Clear();

            output = "";
            stringType = new Dictionary<string, string>();
            numberType = new Dictionary<string, double>();
            string[] splitArray = Regex.Split(code, @"(?:,\s+)");
            List<string> statements = new List<string>();


            for (int i = 0; i < splitArray.Length; i++)
            {
                statements.AddRange(splitArray[i].Split('\n', '\t'));
            }

            if (statements.Count!=0)
            for (int i = 0; i < statements.Count; i++)
            {
                //token[0]: token[1]
                List<string> tokens = new List<string>();
                tokens.AddRange(statements.ElementAt(i).Split(new char[] { ':' }, 2));
                tokens[0] = tokens[0].Trim();
                tokens[1] = tokens[1].Trim();

                //----------------------------------------------------NUMBER
                if (tokens[0].Trim().StartsWith(datatypes[0]))
                {
                        if (tokens[1].Contains(keywords[2]))
                        {
                            tokens[1] = tokens[1].Replace("{", "");
                            tokens[1] = tokens[1].Replace("}", "");
                            string[] get = tokens[1].Split(':');
                            if (listType.ContainsKey(get[1].Trim()))
                                numberType.Add(tokens[0], listType[get[1].Trim()].Count);
                        }
                        //----------------------------------------------------SUM
                        else if (tokens[1].Contains(keywords[3]))
                        {
                            tokens[1] = tokens[1].Replace("{", "");
                            tokens[1] = tokens[1].Replace("}", "");
                            string[] get = tokens[1].Split(':');
                            if (listType.ContainsKey(get[1].Trim()))
                            {
                                double sum = 0;
                                foreach (double d in listType[get[1].Trim()])
                                    sum += d;
                                numberType.Add(tokens[0], sum);
                            }
                        }
                        else
                            numberType.Add(tokens[0], int.Parse(tokens[1]));
                }
                //----------------------------------------------------LIST
                else if (tokens[0].Trim().StartsWith(datatypes[1]))
                {
                    List<double> listOfDouble = new List<double>();

                    tokens[1] = tokens[1].Substring(1, tokens[1].Length - 2);
                    string[] nums = tokens[1].Split(new char[0]);

                    foreach (string n in nums)
                        listOfDouble.Add(double.Parse(n));


                    listType.Add(tokens[0].Trim(), listOfDouble);
                    MessageBox.Show("[" + tokens[0] + "] added to list [" + tokens[1] + "]");
                }
                //----------------------------------------------------STRING
                else if (tokens[0].Trim().StartsWith(datatypes[2]))
                {
                    stringType.Add(tokens[0], tokens[1]);
                }
                //----------------------------------------------------BOOL
                else if (tokens[0].Trim().StartsWith(datatypes[3]))
                {
                    boolType.Add(tokens[0], bool.Parse(tokens[1]));
                }
                //----------------------------------------------------READ
                else if (tokens[0].Contains(keywords[0]))
                {   
                    Read(tokens);
                }
                //----------------------------------------------------WRITE
                else if (tokens[0].Contains(keywords[1]))
                {
                    Write(tokens);
                }
                //----------------------------------------------------LENGTH
                else if (tokens[0].Contains(keywords[2]))
                {
                        if (listType.ContainsKey(tokens[1]))
                        {
                            ConsoleOutput.WriteConsole(listType[tokens[1]].Count+"");
                        }
                }
                //----------------------------------------------------SUM
                else if (tokens[0].Contains(keywords[3]))
                {
                        if (listType.ContainsKey(tokens[1]))
                        {
                            double sum = 0;
                            foreach (double d in listType[tokens[1]])
                                sum += d;
                            ConsoleOutput.WriteConsole(sum + "");
                        }
                }
            }

            //Display Console
            if (consoleIsOpen == true)
            {

                ConsoleOutput.WriteConsole("\n\n--------------------------------------------------------------------------------\nPress any key to continue. . .");
                ConsoleOutput.ReadLineConsole();
                ConsoleOutput.CloseConsole();
                consoleIsOpen = false;
            }
        }

        void Read(List<string> tokens) {
            if (consoleIsOpen == false)
            {
                ConsoleOutput.OpenConsole();
                consoleIsOpen = true;
            }
            string value = ConsoleOutput.ReadLineConsole();
            if (stringType.ContainsKey(tokens[1]))
                stringType.Remove(tokens[1]);
            
                stringType.Add(tokens[1], value);
        }

        void Write(List<string> tokens) {
            
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
            else if (stringType.ContainsKey(tokens[1].Trim()))
            {
                
                if (stringType[tokens[1]].Trim().ToString().StartsWith("\'"))
                {
                    output = stringType[tokens[1]].Trim().ToString().Substring(1, stringType[tokens[1]].Trim().ToString().Length - 2) + "\n";
                    ConsoleOutput.WriteConsole(output);
                }
                else
                {
                    output = stringType[tokens[1]] + "\n";
                    ConsoleOutput.WriteConsole(output);
                }
            }
            else if (numberType.ContainsKey(tokens[1]))
            {
                output = numberType[tokens[1]] + "\n";
                ConsoleOutput.WriteConsole(output);
            }
            else if (boolType.ContainsKey(tokens[1]))
            {
                output = boolType[tokens[1]] + "\n";
                ConsoleOutput.WriteConsole(output);
            }
            else if (listType.ContainsKey(tokens[1]))
            {
                output = "[";
                foreach(double t in listType[tokens[1]])
                    output += t + " ";
                output = output.Trim() + "]\n";
                ConsoleOutput.WriteConsole(output);
            }
        }

        void Ternary(string code)
        {
            string varName, condition, ;
            string[] tern = code.Split(new char[] { ':' }, 2);
            tern.
        }

    }
}