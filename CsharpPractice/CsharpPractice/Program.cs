using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;
namespace CsharpPractice
{
#if UNITY_ANDROID && !UNITY_EDITOR
		OVR_Media_Surface_Init();
#endif

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

        public static string[] GetAllExceptions()
        {
            string[] lines = System.IO.File.ReadAllLines(@"all_exceptions.txt");
            return lines;
        }

        static void Main(string[] args)
        {
            Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
            int n = 0;
            foreach(string exceptionName in GetAllExceptions())
            {
                try
                {
                    Type type = Type.GetType(exceptionName);
                    if (type == null)
                        continue;
                    n++;
                    object[] parameters = new object[1];
                    string lpCh = exceptionName;
                    parameters[0] = lpCh;
                    object obj = Activator.CreateInstance(type, parameters);
                    throw new Exception("Damn!!!, Exception happened", (Exception)(obj));
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine("========================================");
            }
            return;
            DateTime dt = DateTime.Now;
            Console.WriteLine(dt);
            string time = "Current Time:" + dt.ToString("yyyy-MM-dd:HH:mm:ss");
            Console.WriteLine(time);
            string mAppVersionName = "01.3.0";
            if (mAppVersionName.Contains("CB.") == false)
            {
                string[] list = mAppVersionName.Split('.');
                string tmp = list.Length > 1 ? list[0] : "";//first version handle
                if (tmp.Length == 1)// first version number length is 1
                    tmp = "0" + tmp;
                list[0] = tmp;
                StringBuilder sb = new StringBuilder();
                foreach (string str in list)
                    sb.Append("." + str);
                mAppVersionName = "CB" + sb.ToString() + string.Format("({0})", "Official_Chanel");
            }


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
