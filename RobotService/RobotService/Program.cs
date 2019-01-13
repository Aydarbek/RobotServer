﻿using System;
using System.IO;
using System.Threading;

namespace RobotService
{
    class Program
    {
        const string path = @"d:\Projects\RobotServer\data\";
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Manager();
        }

        private void Manager()
        {
            while(true)
            {
                Thread.Sleep(1000);
                Console.Write(".");
                string folder = GetNextFolder();
                if (folder == "")
                    continue;
                Console.WriteLine();
                Console.WriteLine("Get Folder" + folder);
                MoveFolder(folder, "wait", "work");
                Robot robot = Robot.CreateRobot(path + "work\\" + folder);
                robot.Start();
                MoveFolder(folder, "work", "done");

            }
        }

        private void MoveFolder(string folder, string from, string to)
        {
            Directory.Move(path + from + "\\" + folder,
               path + to + "\\" + folder);
        }

        private string GetNextFolder()
        {
            foreach (string folder in Directory.GetDirectories(path + "wait\\"))
                return Path.GetFileName(folder);
            return "";
        }
    }
}
