using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace ThreadPoolSimple
{
    class Program
    {
        public static int COUNT = 1000;
        static void Main(string[] args)
        {
            ThreadPool.SetMaxThreads(1, 0);
            
            ThreadPool.QueueUserWorkItem(DoWork, "+");
            ThreadPool.QueueUserWorkItem(DoWork, "+");
            ThreadPool.QueueUserWorkItem(DoWork, "+");
            ThreadPool.QueueUserWorkItem(DoWork, "+");
            ThreadPool.QueueUserWorkItem(DoWork, "+");
            ThreadPool.QueueUserWorkItem(DoWork, "+");
            ThreadPool.QueueUserWorkItem(DoWork, "+");
            for (int i = 0; i < COUNT; i++)
            {
                Console.Write("---");
            }
            Thread.Sleep(10000);
        }

        public static void DoWork(object obj )
        {
            for (int i = 0; i < COUNT; i++)
            {
                Thread.Sleep(10);
                Console.Write(Thread.CurrentThread.ManagedThreadId + " " + (string)obj);
            }
        }
    }
}
