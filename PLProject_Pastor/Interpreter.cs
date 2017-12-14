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
        string[] keywords = { "darllenwch", "ysgrifennu", "hyd", "svm", "os", "am", "yn"};// read,write,length,sum,if
        string[] datatypes = { "#", "@", "$", "~" };
        string[] ariths = { "+", "-", "*", "/", "%" };
        public Interpreter() { }

        Regex arithmetics = new Regex(@"([-+]?[0-9]*\.?[0-9]+[\/\+\-\*])+([-+]?[0-9]*\.?[0-9]+)");
        Dictionary<string, string> stringType = new Dictionary<string, string>();
        Dictionary<string, double> numberType = new Dictionary<string, double>();
        Dictionary<string, List<double>> listType = new Dictionary<string, List<double>>();
        Dictionary<string, bool> boolType = new Dictionary<string, bool>();
        Dictionary<string, double> arithTypes = new Dictionary<string, double>();

        string output;
        bool consoleIsOpen = false;

        public void read(string code)
        {
            try{
                consoleIsOpen = false;
                stringType.Clear();
                numberType.Clear();
                boolType.Clear();
                listType.Clear();

                output = "";

                if (code.StartsWith("{") && code.EndsWith("}"))
                {
                    code = code.TrimStart('{').TrimEnd('}').Trim();
                }
                else
                    throw new Exception();

                stringType = new Dictionary<string, string>();
                numberType = new Dictionary<string, double>();
                string[] splitArray = Regex.Split(code, @"(?:,\s+)");
                //splitArray = Regex.Split(code, @"(?:,\s+)^(\(\*\\\))");
                List<string> statements = new List<string>();


                for (int i = 0; i < splitArray.Length; i++)
                {
                    statements.AddRange(splitArray[i].Split('\n', '\t'));
                }

                for (int i = 0; i < statements.Count; i++)
                {
                    //token[0]: token[1]
                    List<string> tokens = new List<string>();
                    tokens.AddRange(statements.ElementAt(i).Split(new char[] { ':' }, 2));
                    tokens[0] = tokens[0].Trim();
                    tokens[1] = tokens[1].Trim();

                    if (tokens[0].Contains(keywords[5]))//for
                    {
                        MessageBox.Show("pasok");
                        string[] splitter = Regex.Split(tokens[1], @"\(\{\*\}\)");
                        for(int x=0; x<splitter.Length;x++)
                        {
                            MessageBox.Show(splitter[x]);
                        } 
                        string var = tokens[1].Split(':')[0].TrimStart('{').Split(' ')[0];
                        
                        string min = tokens[1].Split(':')[0].TrimStart('{').Split(' ')[3].TrimStart('[').TrimEnd(']').Split(' ')[0];
                        string max = tokens[1].Split(':')[0].TrimStart('{').Split(' ')[3].TrimStart('[').TrimEnd(']').Split(' ')[1];
                        string code1 = tokens[1].Split(':')[1].Replace("{", "");
                        code1 = tokens[1].Split(':')[1].Replace("}", "");

                        MessageBox.Show(var + " " + min + " " + max + " " + code1);
                        For(code1, int.Parse(min), int.Parse(max));
                    }
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
                        else if (tokens[1].Contains(keywords[4]))
                        { 
                            numberType.Add(tokens[0], Ternary(tokens[1], 0));
                        }
                        //--------------------------------------------------ADD
                        else if (tokens[1].Contains(ariths[0]))
                        {
                            string[] toks = tokens[1].Split(' ');
                            double val1, val2;
                            if (toks[1].Trim().StartsWith("#"))
                                val1 = numberType[toks[1].Trim()];
                            else
                                val1 = double.Parse(toks[1]);

                            if (toks[2].Trim().StartsWith("#"))
                                val2 = numberType[toks[2].Trim()];
                            else
                                val2 = double.Parse(toks[2]);


                            double ans = val1 + val2;
                            
                            numberType.Add(tokens[0], ans);

                        }
                        //--------------------------------------------------SUBTRACT
                        else if (tokens[1].Contains(ariths[1]))
                        {
                            string[] toks = tokens[1].Split(' ');
                            double val1, val2;
                            if (toks[1].Trim().StartsWith("#"))
                                val1 = numberType[toks[1].Trim()];
                            else
                                val1 = double.Parse(toks[1]);

                            if (toks[2].Trim().StartsWith("#"))
                                val2 = numberType[toks[2].Trim()];
                            else
                                val2 = double.Parse(toks[2]);


                            double ans = val1 - val2;

                            numberType.Add(tokens[0], ans);
                        }
                        //--------------------------------------------------MULTIPLY
                        else if (tokens[1].Contains(ariths[2]))
                        {
                            string[] toks = tokens[1].Split(' ');
                            double val1, val2;
                            if (toks[1].Trim().StartsWith("#"))
                                val1 = numberType[toks[1].Trim()];
                            else
                                   val1 = double.Parse(toks[1]);

                            if (toks[2].Trim().StartsWith("#"))
                                val2 = numberType[toks[2].Trim()];
                            else
                                val2 = double.Parse(toks[2]);


                            double ans = val1 * val2;

                            numberType.Add(tokens[0], ans);
                        }
                        //-------------------------------------------------- DIVIDE
                        else if (tokens[1].Contains(ariths[3]))
                        {
                                string[] toks = tokens[1].Split(' ');
                                double val1, val2;
                                if (toks[1].Trim().StartsWith("#"))
                                    val1 = numberType[toks[1].Trim()];
                                else
                                    val1 = double.Parse(toks[1]);

                                if (toks[2].Trim().StartsWith("#"))
                                    val2 = numberType[toks[2].Trim()];
                                else
                                    val2 = double.Parse(toks[2]);


                                double ans = val1 / val2;

                                numberType.Add(tokens[0], ans);
                            }
                        //--------------------------------------------------Remainder
                        else if (tokens[1].Contains(ariths[4]))
                        {
                            string[] toks = tokens[1].Split(' ');
                            double val1, val2;
                            if (tokens[1].Trim().StartsWith("#"))
                                val1 = numberType[toks[1].Trim()];
                            else
                                val1 = double.Parse(toks[1]);

                            if (tokens[2].Trim().StartsWith("#"))
                                val2 = numberType[toks[2].Trim()];
                            else
                                val2 = double.Parse(toks[2]);

                            double ans = val1 % val2;
                            numberType.Add(tokens[1], ans);

                        }
                        else
                            numberType.Add(tokens[0], int.Parse(tokens[1]));
                    }
                    //----------------------------------------------------LIST
                    else if (tokens[0].Trim().StartsWith(datatypes[1]))
                    {
                        List<double> listOfDouble = new List<double>();

                        
                        tokens[1] = tokens[1].Replace("[", "");
                        tokens[1] = tokens[1].Replace("]", "");
                        string[] nums = tokens[1].Split(new char[0]);

                        foreach (string n in nums)
                            listOfDouble.Add(double.Parse(n));


                        listType.Add(tokens[0].Trim(), listOfDouble);
                    }
                    //----------------------------------------------------STRING
                    else if (tokens[0].Trim().StartsWith(datatypes[2]))
                    {
                        stringType.Add(tokens[0], tokens[1]);
                    }
                    //----------------------------------------------------BOOL
                    else if (tokens[0].Trim().StartsWith(datatypes[3]))
                    {
                        boolType.Add(tokens[0], getWelshBoolean(tokens[1]));
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
                            ConsoleOutput.WriteConsole(listType[tokens[1]].Count + "");
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

                    ConsoleOutput.WriteConsole("\n\n---------------------------------------------------------------\nPress any key to continue. . .");
                    ConsoleOutput.ReadLineConsole();
                    ConsoleOutput.CloseConsole();
                    consoleIsOpen = false;
                }
            }
            catch (Exception ex) {
                //MessageBox.Show("ERROR. Unable to execute.");
                MessageBox.Show(ex.Message);
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
                    output = stringType[tokens[1]].Substring(1, stringType[tokens[1]].Trim().ToString().Length - 2) + "\n";
                    ConsoleOutput.WriteConsole(output);
                }
                else
                {
                    output = stringType[tokens[1]].ToString().Replace("\'", "").Trim() + "\n";
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
                //output = getWelshBoolean(boolType[tokens[1]]) + "\n";
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

        public String Ternary(string code, string dataType)
        {
            //{if:1>2 | 1;2}
            string condition, c1, c2;
            code= code.Replace("{", "");
            code= code.Replace("}", "");

            string[] tern2 = code.Split(new char[] { '|' }, 2);
            if ((tern2[0].Split(':')[0]).Contains(keywords[4]))
            {
                condition = tern2[0].Split(':')[1];
                c1 = tern2[1].Split(';')[0];
                c2 = tern2[1].Split(';')[1];
            }
            else throw new Exception();

                if (Condition(condition))
                    return c1;
                else
                    return c2;

        }

        public void For(string code, int min, int max) {
            for (int i = min; i <= max; i++) {
                read(code);
            }


        }

        public double Ternary(string code, double dataType)
        {
            //{if:1>2 , 1;2}
            string condition, c1, c2;
            code = code.Replace("{", "");
            code = code.Replace("}", "");

            string[] tern2 = code.Split(new char[] { '|' }, 2);
            if ((tern2[0].Split(':')[0]).Contains(keywords[4]))
            {
                condition = tern2[0].Split(':')[1].Trim();
                c1 = tern2[1].Trim().Split(';')[0];
                c2 = tern2[1].Trim().Split(';')[1];
            }
            else throw new Exception();
            
            if (Condition(condition))
                return double.Parse(c1);
            else
                return double.Parse(c2);
        }

        bool Condition(string parse) {
            if (parse.Contains("<")) { 
                if(!parse.Split('<')[0].Trim().StartsWith(datatypes[0]) && !parse.Split('<')[1].Trim().StartsWith(datatypes[0]))
                    return double.Parse(parse.Split('<')[0]) < double.Parse(parse.Split('<')[1]);
                else if (parse.Split('<')[0].Trim().StartsWith(datatypes[0]) && !parse.Split('<')[1].Trim().StartsWith(datatypes[0]))
                {
                    if (numberType.ContainsKey(parse.Split('<')[0].Trim()))
                        return numberType[parse.Split('<')[0].Trim()] < double.Parse(parse.Split('<')[1]);
                }
                else if (!parse.Split('<')[0].Trim().StartsWith(datatypes[0]) && parse.Split('<')[1].Trim().StartsWith(datatypes[0]))
                {
                    if (numberType.ContainsKey(parse.Split('<')[1].Trim()))
                        return double.Parse(parse.Split('<')[0]) < numberType[parse.Split('<')[1].Trim()];
                }
                else if (parse.Split('<')[0].Trim().StartsWith(datatypes[0]) && parse.Split('<')[1].Trim().StartsWith(datatypes[0]))
                {
                    if (numberType.ContainsKey(parse.Split('<')[0].Trim())&& numberType.ContainsKey(parse.Split('<')[1].Trim()))
                        return numberType[parse.Split('<')[0].Trim()] < numberType[parse.Split('<')[1].Trim()];
                }
            }
            else if (parse.Contains(">"))
            {
                if (!parse.Split('>')[0].Trim().StartsWith(datatypes[0]) && !parse.Split('>')[1].Trim().StartsWith(datatypes[0]))
                    return double.Parse(parse.Split('>')[0]) > double.Parse(parse.Split('>')[1]);
                else if (parse.Split('>')[0].Trim().StartsWith(datatypes[0]) && !parse.Split('>')[1].Trim().StartsWith(datatypes[0]))
                {
                    if (numberType.ContainsKey(parse.Split('>')[0].Trim()))
                        return numberType[parse.Split('>')[0].Trim()] > double.Parse(parse.Split('>')[1]);
                }
                else if (!parse.Split('>')[0].Trim().StartsWith(datatypes[0]) && parse.Split('>')[1].Trim().StartsWith(datatypes[0]))
                {
                    if (numberType.ContainsKey(parse.Split('>')[1].Trim()))
                        return double.Parse(parse.Split('>')[0]) > numberType[parse.Split('>')[1].Trim()];
                }
                else if (parse.Split('>')[0].Trim().StartsWith(datatypes[0]) && parse.Split('>')[1].Trim().StartsWith(datatypes[0]))
                {
                    if (numberType.ContainsKey(parse.Split('>')[0].Trim()) && numberType.ContainsKey(parse.Split('>')[1].Trim()))
                        return numberType[parse.Split('>')[0].Trim()] > numberType[parse.Split('>')[1].Trim()];
                }
            }
            else if (parse.Contains("="))
            {
                if (!parse.Split('=')[0].Trim().StartsWith(datatypes[0]) && !parse.Split('=')[1].Trim().StartsWith(datatypes[0]))
                    return double.Parse(parse.Split('=')[0]) == double.Parse(parse.Split('=')[1]);
                else if (parse.Split('=')[0].Trim().StartsWith(datatypes[0]) && !parse.Split('=')[1].Trim().StartsWith(datatypes[0]))
                {
                    if (numberType.ContainsKey(parse.Split('=')[0].Trim()))
                        return numberType[parse.Split('=')[0].Trim()] == double.Parse(parse.Split('=')[1]);
                }
                else if (!parse.Split('=')[0].Trim().StartsWith(datatypes[0]) && parse.Split('=')[1].Trim().StartsWith(datatypes[0]))
                {
                    if (numberType.ContainsKey(parse.Split('=')[1].Trim()))
                        return double.Parse(parse.Split('=')[0]) == numberType[parse.Split('=')[1].Trim()];
                }
                else if (parse.Split('=')[0].Trim().StartsWith(datatypes[0]) && parse.Split('=')[1].Trim().StartsWith(datatypes[0]))
                {
                    if (numberType.ContainsKey(parse.Split('=')[0].Trim()) && numberType.ContainsKey(parse.Split('=')[1].Trim()))
                        return numberType[parse.Split('=')[0].Trim()] == numberType[parse.Split('=')[1].Trim()];
                }
            }

            return false;
        }

        string getWelshBoolean(bool x) {
            return x ? "wir" : "ffug";
        }

        bool getWelshBoolean(string welsh) {
            return welsh == "wir";
        }

    }
}