using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace RobotService
{
    internal class RobotJava : Robot
    {
        protected override bool Compile()
        {
            try
            {
                Command("\"C:\\Program Files\\Java\\jdk1.8.0_191\\bin\\javac\" Program.java 2> compile.out");
                Console.WriteLine("Compiling...." + folder);
                Thread.Sleep(3000);
                long length = new FileInfo(folder + "\\compile.out").Length;
                if (length == 0)
                {
                    Console.WriteLine("Compilation succesfull");
                    return true;
                }
                Console.WriteLine("Compilation error!");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        protected override void Run(string InputFileName)
        {
            try
            {
                Console.WriteLine("Run test..." + InputFileName);
                string OutputFileName = InputFileName.Replace(".in", ".out");
                Command("\"C:\\Program Files\\Java\\jdk1.8.0_191\\bin\\java\" Program < " + InputFileName + " >" + OutputFileName + " 2>&1");
                
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}