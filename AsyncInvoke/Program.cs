using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncInvoke
{
    class Program
    {
        public delegate int AddDelegate(int x, int y);
        static void Main(string[] args)
        {
            Console.WriteLine("Client application started");
            Thread.CurrentThread.Name = "Main Thread";

            Calculator cal = new Calculator();
            AddDelegate del = new AddDelegate(cal.Add);
            IAsyncResult asyncResult = del.BeginInvoke(1, 2, null, null);



            for (int i = 1; i <= 3; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(i));
                Console.WriteLine("{0}:Client excuted {1} seconds.", Thread.CurrentThread.Name, i);
            }

            int result = del.EndInvoke(asyncResult);
            Console.WriteLine("Result:" + result);

            Console.WriteLine("Press any key exist");
            Console.Read();

        }
    }
    class Calculator
    {
        public int Add(int x, int y)
        {
            if (Thread.CurrentThread.IsThreadPoolThread)
            {
                Thread.CurrentThread.Name = "Pool Thread";
            }
            Console.WriteLine("Method invoked");

            for (int i = 1; i <= 2; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(i));
                Console.WriteLine("{0}:Add excuted {1} seconds.", Thread.CurrentThread.Name, i);
            }

            Console.WriteLine("Method complete");
            return x + y;
        }
    }
}
