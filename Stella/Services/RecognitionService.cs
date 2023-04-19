using Microsoft.Extensions.Hosting;
using NAudio.Wave;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stella.Tasks;
using Stella.Tasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vosk;

namespace Stella.Workers
{
    public class RecognitionService : BackgroundService
    {
        const string _modelPath = @"C:\Users\kEdo-PC\source\repos\Stella\Stella\vosk-model-small-ru-0.22";
        const int _sampleRate = 16000;
        List<IRecognitionTask> _tasks = new List<IRecognitionTask>();

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            using (var recognizer = new VoskRecognizer(new Model(_modelPath), _sampleRate))
            {
                var waveInEvent = new WaveInEvent
                {
                    DeviceNumber = 0,
                    WaveFormat = new WaveFormat(_sampleRate, 1),
                    BufferMilliseconds = 50,
                    NumberOfBuffers = 3
                };

                waveInEvent.DataAvailable += (sender, e) =>
                {
                    if (recognizer.AcceptWaveform(e.Buffer, e.BytesRecorded))
                    {
                        var result = (JsonConvert.DeserializeObject(recognizer.Result()) as JToken)["text"].ToString();
                        if (result.Contains("стелла"))
                        {
                            RecognitionTaskFactory.StartRecognitionTask(result, _tasks);
                        }
                        Console.WriteLine(result);  
                    }
                };

                waveInEvent.StartRecording();

                Console.WriteLine("Speak now...");

                while (!stoppingToken.IsCancellationRequested)
                {
                
                }

                return Task.CompletedTask;
            }
        }
    }
}
