using System;

namespace AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new Factory2());
            productManager.GetAll();

            Console.ReadKey();
        }
    }

    public abstract class Logging
    {
        public abstract void Log(string message);
    }

    public class Log4NetLogger : Logging
    {
        public override void Log(string message)
        {
            Console.WriteLine("Logged with log4net");
        }
    }

    public class NLogger : Logging
    {
        public override void Log(string message)
        {
            Console.WriteLine("Logged with nLogger");
        }
    }

    public abstract class Caching
    {
        public abstract void Cache(string data);
    }

    public class MemCache : Caching
    {
        public override void Cache(string data)
        {
            Console.WriteLine("Cached with MemCache");
        }
    }

    public class RedisCache : Caching
    {
        public override void Cache(string data)
        {
            Console.WriteLine("Cached with RedisCache");
        }
    }

    public abstract class CrossCuttingConcernsFactory1
    {
        public abstract Logging CreateLogger();
        public abstract Caching CreateCaching();
    }

    public class Factory1 : CrossCuttingConcernsFactory1
    {
        public override Caching CreateCaching()
        {
            return new RedisCache();
        }

        public override Logging CreateLogger()
        {
            return new Log4NetLogger();
        }
    }

    public class Factory2 : CrossCuttingConcernsFactory1
    {
        public override Caching CreateCaching()
        {
            return new MemCache();
        }

        public override Logging CreateLogger()
        {
            return new NLogger();
        }
    }

    public class ProductManager : IProductService
    {
        CrossCuttingConcernsFactory1 _crossCuttingConcernsFactory1;

        private Logging _logging;
        private Caching _caching;
        public ProductManager(CrossCuttingConcernsFactory1 crossCuttingConcernsFactory1)
        {
            _crossCuttingConcernsFactory1 = crossCuttingConcernsFactory1;
            _logging = _crossCuttingConcernsFactory1.CreateLogger();
            _caching = crossCuttingConcernsFactory1.CreateCaching();
        }
        public void GetAll()
        {
            _logging.Log("Logged");
            _caching.Cache("Data");
            Console.WriteLine("Products listed!");
        }
    }

    public interface IProductService
    {
        void GetAll();
    }
}
