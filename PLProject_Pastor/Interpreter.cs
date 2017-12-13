using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PLProject_Pastor
{
    class Interpreter
    {
        string[] keywords = { "read", "write", "len", "sum" };
        public Interpreter() { }

        void read() {

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
