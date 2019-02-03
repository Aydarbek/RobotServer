using System;
using System.IO;

namespace RobotService
{
    class RobotJava : Robot
    {
        protected override bool Compile()
        {
            RunCommand("\"C:\\Program Files\\Java\\jdk1.8.0_191\\bin\\javac\" Program.java 2> compiler.out");
            return (new FileInfo(folder + "compiler.out").Length == 0);

        }

        protected override void RunTest(string InFile, string OutFile)
        {
            Console.WriteLine("Run test... " + InFile);
            RunCommand("\"C:\\Program Files\\Java\\jdk1.8.0_191\\bin\\java\" Program < " + InFile + " >" + OutFile + " 2>&1");
        }

        protected override void TaskKill()
        {
            throw new NotImplementedException();
        }
    }
}
