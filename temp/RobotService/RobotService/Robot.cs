using System;
using System.Diagnostics;
using System.IO;

namespace RobotService
{
    abstract class Robot
    {
        protected string folder;
        const string path = "d:\\Projects\\RobotServer\\data\\";
        public static Robot CreateRobot(string folder)
        {
            //folder =190102.194030.5.java
            
            string lang = Path.GetExtension(folder);
            switch(lang)
            {
                case ".cs": return new RobotSharp();
                case ".java": return new RobotJava();
                default: return null;
            }
        }

        public void Start(string folder)
        {
            this.folder = folder;
            if (Compile())
                foreach (string InputFile in Directory.GetFiles(folder, "*.in"))
                    Run(InputFile);
        }

        protected void Command(string command)
        {
            Process cmd = new Process();
            cmd.StartInfo.WorkingDirectory = folder;
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
        }


        abstract protected bool Compile();
        abstract protected void Run(string InputFileName);

    }
}
