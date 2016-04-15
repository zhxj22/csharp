using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CsharpPractice
{
    class Program
    {
        public static void CountTo100()
        {
            for(int index = 0; index<100; index++)
            {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ": " + index + 1);
            }
        }
        //创建一个符合 ParameterizedThreadStart 代理签名的方法
        public static void AddTo(object a)
        {
            Console.WriteLine( 1 + (int)a);
        }
        static void Main(string[] args)
        {
            //如何启动无参数的线程
            Thread thread = new Thread(CountTo100);
            thread.Start();
            thread.Join();
            Thread thread2 = new Thread(CountTo100);
            thread2.Start();
            thread2.Join();

            //如何启动有参数的线程
            Thread thread3 = new Thread(AddTo);
            //start里面传入 参数
            thread3.Start(2);

            Console.ReadKey();
        }
    }
}
