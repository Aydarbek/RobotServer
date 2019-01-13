using System;
using System.IO;
using System.Threading;

namespace RobotService
{
    class Program
    {
        const string path = "d:\\Projects\\RobotServer\\data\\";
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Start();
        }

        void Start()
        {
            while(true)
            {
                Thread.Sleep(1000);
                string folder = GetFolderFromWait();
                Console.Write(".");
                if (folder == "")
                    continue;
                Console.WriteLine();
                Console.WriteLine("Get Folder" + folder);
                MoveFolder(folder, "wait", "work");

                Robot robot = Robot.CreateRobot(folder);
                robot.Start(path + "work\\" + folder); //start new thread
                MoveFolder(folder, "work", "done");               

            }
        }

        private void MoveFolder(string folder, string from, string to)
        {
            // переместить рабочий каталог в папку work для компиляции и запуска

            try
            {
                Directory.Move(path + from + "\\" + folder,
                               path + to   + "\\" + folder);
                    
            } catch (Exception e) {
                Console.WriteLine(e.Message); }
            
        }

        private string GetFolderFromWait()
        {
            // Просканнроивать папку wait на наличие новых запросов на выполнение
            string wait = path + "wait\\";
            try
            {
            foreach (var folder in Directory.GetDirectories(wait))
                return Path.GetFileName(folder);
            } catch { }
            return "";
        }
    }
}
