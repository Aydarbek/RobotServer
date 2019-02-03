using System;
using System.IO;
using System.Text.RegularExpressions;

namespace RobotService
{
    class RobotSharp : Robot
    {

        protected override bool Compile()
        {
            RunCommand("\"C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\csc.exe\" Program.cs > compiler.out");
            return !File.ReadAllText(folder + "compiler.out").Contains("error");
        }

        protected override void RunTest(string InFile, string OutFile)
        {
            Console.WriteLine("Run test... " + InFile);
            RunCommand(@"Program.exe < " + InFile + " > " + OutFile + " 2>&1");
        }

        protected override void TaskKill()
        {
            RunCommand(@"taskkill /IM program.exe /F");
        }
    }
}
