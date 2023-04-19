using Newtonsoft.Json;
using Stella.Models;
using Stella.Tasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Stella.Tasks
{
    internal class ProgramTask : IRecognitionTask
    {
        private readonly string _command;
        public ProgramTask(string command)
        {
            _command = command;
        }
        public void Run()
        {
            var programs = JsonConvert.DeserializeObject<List<Programs>>(File.ReadAllText($"{Directory.GetCurrentDirectory()}\\programs.json"));
            var programForStart = programs.Where(x => x.Names.Contains(_command))?.FirstOrDefault()?.Uri;
            if(programForStart != null)
            {
                Process.Start(programForStart);
            }
        }

        public void Stop()
        {

        }
    }
}
