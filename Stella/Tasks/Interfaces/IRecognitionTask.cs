using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stella.Tasks.Interfaces
{
    public interface IRecognitionTask
    {
        void Run();
        void Stop();
    }
}
