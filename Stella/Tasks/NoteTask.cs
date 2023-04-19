using OpenQA.Selenium;
using Stella.Tasks.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stella.Tasks
{
    public class NoteTask : IRecognitionTask
    {
        private readonly string _command;
        public NoteTask(string command)
        {
            _command = command;
        }
        public void Run()
        {
            var path = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\Note.txt";
            File.WriteAllText(path, _command);
        }

        public void Stop()
        {

        }
    }
}
