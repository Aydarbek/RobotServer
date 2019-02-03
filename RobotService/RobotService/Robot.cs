using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace RobotService
{
    abstract class Robot
    {
        protected string folder;

        public static Robot CreateRobot(string folder)
        {
            switch (Path.GetExtension(folder))
            {
                case (".java"): return new RobotJava().SetFolder(folder);
                case (".cs"): return new RobotSharp().SetFolder(folder);
                default: throw new SystemException("Not supported robot for " + folder);

            }
        }

        protected Robot SetFolder (string folder)
        {
            this.folder = folder + "\\";
            return this;
        }

        public void Start()
        {
            try
            {
                if (Compile())
                {
                    Console.WriteLine("Compilation successful!");
                    foreach (var file in Directory.GetFiles(folder, "*.in"))
                    {
                        Thread TestThread = new Thread(new ParameterizedThreadStart(Test));
                        TestThread.Start(file);
                        Thread.Sleep(3000);
                        if (TestThread.IsAlive)
                        {
                            TestThread.Abort();
                            TaskKill();
                        }                           
                    }
                    
                }
                else
                {
                    Console.WriteLine("Compilation error!");
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        protected void RunCommand(string command)
        {
            try
            {

                Process cmd = new Process();
                cmd.StartInfo.WorkingDirectory = folder;
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = false;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();

                cmd.StandardInput.WriteLine(command);
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
            } catch (Exception e) { Console.WriteLine(e.Message); }
        }

        private void Test (object file)
        {
            RunTest(Path.GetFileName((string)file),
                            Path.GetFileName((string)file).Replace(".in", ".out"));
        }
        

        abstract protected bool Compile();

        abstract protected void RunTest(string InFile, string OutFile);
        abstract protected void TaskKill();
    }
}
