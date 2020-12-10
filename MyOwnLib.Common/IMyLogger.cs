using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnLib.Common
{
    public interface IMyLogger
    {
        void Debug(string message, string arg = null);
    
        void Info(string message, string arg = null);

        void Warning(string message, string arg = null);

        void Error(string message, string arg = null);
    }
}
