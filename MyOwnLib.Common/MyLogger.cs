using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnLib.Common
{
    public class MyLogger : IMyLogger
    {

        private static MyLogger _instace;
        private static Logger _logger;

        public MyLogger()
        {

        }

        public static MyLogger GetInstance()
        {
            if(_instace == null)
           _instace = new MyLogger();
            return _instace;      
        }

        private Logger GetLogger(string logger)
        {
            if (MyLogger._logger == null)
                MyLogger._logger = LogManager.GetLogger(logger);

            return MyLogger._logger; 
        }

        public void Debug(string message, string arg = null)
        {
            if (arg == null)
                GetLogger("LogRule").Debug(message);
            else
                GetLogger("LogRule").Debug(message, arg);
        }

        public void Error(string message, string arg = null)
        {
            if (arg == null)
                GetLogger("LogRule").Error(message);
            else
                GetLogger("LogRule").Error(message, arg);
        }

        public void Info(string message, string arg = null)
        {
            if (arg == null)
                GetLogger("LogRule").Info(message);
            else
                GetLogger("LogRule").Info(message, arg);
        }

        public void Warning(string message, string arg = null)
        {
            if (arg == null)
                GetLogger("LogRule").Warn(message);
            else
                GetLogger("LogRule").Warn(message, arg);
        }
    }
}
