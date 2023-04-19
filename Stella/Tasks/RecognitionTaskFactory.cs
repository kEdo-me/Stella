using Stella.Tasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stella.Tasks
{
    public static class RecognitionTaskFactory
    {
        public static void StartRecognitionTask(string command, List<IRecognitionTask> tasks)
        {
            if(command.Contains("включи") 
                || command.Contains("поставь"))
            {
                var musicTask = new MusicTask(command.Replace("включи ", null).Replace("поставь ", null).Replace("стелла ", null));
                musicTask.Run();
                tasks.Add(musicTask);
            }
            if(command.Contains("выключи музыку") || command.Contains("останови музыку"))
            {
                var taskForStop = tasks.Where(x=>x.GetType() == typeof(MusicTask));
                foreach(var task in taskForStop)
                {
                    task.Stop();
                }
                tasks = tasks.Except(taskForStop).ToList();
            }
            if (command.Contains("найди"))
            {
                var searchTask = new SearchTask(command.Replace("стелла ", null).Replace("найди ", null));
                searchTask.Run();
                tasks.Add(searchTask);
            }
            if(command.Contains("закрой поиск")){
                var taskForStop = tasks.Where(x => x.GetType() == typeof(SearchTask));
                foreach (var task in taskForStop)
                {
                    task.Stop();
                }
                tasks = tasks.Except(taskForStop).ToList();
            }
            if(command.Contains("запусти") || command.Contains("открой"))
            {
                var programTask = new ProgramTask(command.Replace("запусти ", null).Replace("открой ", null).Replace("стелла ", null));
                programTask.Run();
            }
            if (command.Contains("запиши"))
            {
                var noteTask = new NoteTask(command.Replace("запиши ", null).Replace("стелла ", null));
                noteTask.Run();
            }
        }
    }
}
