using System;

namespace FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerLogger customerLogger = new CustomerLogger(new LoggerFactory2());
            customerLogger.Save();

            Console.ReadKey();
        }
    }

    public class LoggerFactory : ILoggerFactory
    {
        //Business to decide factory
        public ILogger CreateLogger()
        {
            return new CaLogger();
        }
    }

    public class LoggerFactory2 : ILoggerFactory
    {
        //Business to decide factory
        public ILogger CreateLogger()
        {
            return new Log4NetLogger();
        }
    }

    public interface ILoggerFactory
    {
        ILogger CreateLogger();
    }

    public interface ILogger
    {
        void Log();
    }

    public class CaLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Logged with CaLogger");
        }
    }

    public class Log4NetLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Logged with Log4NetLogger");
        }
    }

    public class CustomerLogger
    {
        private ILoggerFactory _loggerFactory;
        public CustomerLogger(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }
        public void Save()
        {
            Console.WriteLine("Saved!");
            ILogger logger = _loggerFactory.CreateLogger();
            logger.Log();
        }
    }
}
