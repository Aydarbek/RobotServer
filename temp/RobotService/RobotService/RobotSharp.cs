using System;
using System.IO;
using System.Threading;

namespace RobotService
{
    internal class RobotSharp : Robot
    {
        
        protected override bool Compile()
        {
            try
            {               
                Command("\"C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\csc.exe\" Program.cs > compile.out");
                Console.WriteLine("Compiling...." + folder);
                Thread.Sleep(3000);
                string CompileLog = File.ReadAllText(folder + "\\compile.out");
                if (!CompileLog.Contains("error"))
                {
                    Console.WriteLine("Compilation succesfull");
                    return true;
                }
                else
                {
                    Console.WriteLine("Compilation error!");
                    return false;
                }                

            }
            catch
            {                
                return false;
            }
            

        }
        protected override void Run(string InputFileName)
        {
            try
            {
                Console.WriteLine("Run test..." + InputFileName);
                string OutputFileName = InputFileName.Replace(".in", ".out");
                Command("Program.exe < " + InputFileName + " >" + OutputFileName + " 2>&1");
            }
            catch (OverflowException)
            {

            }
        }
    }
}