using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda
{
    class Program
    {
        delegate int MyFunc(int a);
        static void Main(string[] args)
        {
            MyFunc f1 = FuncInst;
            f1(2121);
            MyFunc f2 = delegate (int a)
            {
                return a;
            };
            f2(2222);

            TestFunc((int x) => { return x; });
            TestFunc2((int a,  int b) => { });
        }
        static int FuncInst(int a)
        {
            return a;
        }
        static void TestFunc(MyFunc f)
        {
            f(2);
        }
        static void TestFunc2(Action<int ,int> action)
        {
            action(1, 2);
        }
    }
}
