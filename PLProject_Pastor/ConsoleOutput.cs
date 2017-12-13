using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PLProject_Pastor
{
    class ConsoleOutput
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern int FreeConsole();
        [DllImport("kernel32")]
        static extern bool AllocConsole();
        public static void OpenConsole()
        {
            AllocConsole();
        }
        public static void WriteConsole(string outputString)
        {
            using (Stream stream = Console.OpenStandardOutput())
            {
                using (TextWriter writer = new StreamWriter(stream))
                {
                    writer.Write(outputString);
                }
            }
        }
        public static string ReadLineConsole()
        {
            string str;
            using (Stream stream = Console.OpenStandardInput())
            {
                using (TextReader reader = new StreamReader(stream))
                {
                    str = reader.ReadLine();
                }
            }
            return str;
        }
        public static void CloseConsole()
        {
            FreeConsole();
        }
    }

}
