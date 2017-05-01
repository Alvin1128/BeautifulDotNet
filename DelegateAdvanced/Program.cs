using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DelegateAdvanced
{
    class Program
    {
        static void Main(string[] args)
        {
            Publisher pub = new Publisher();
            Subscriber1 sub1 = new Subscriber1();
            Subscriber2 sub2 = new Subscriber2();
            Subscriber3 sub3 = new Subscriber3();
            pub.MyEvent += sub1.OnNumberChanged;
            pub.MyEvent += sub2.OnNumberChanged;
            pub.MyEvent += sub3.OnNumberChanged;

            pub.DoSomething();

            Console.WriteLine("Control back to client");


            Console.Read();
        }


        public static object[] FireEvent(Delegate del, params object[] args)
        {
            List<object> strList = new List<object>();
            if (del != null)
            {
                Delegate[] delArray = del.GetInvocationList();
                foreach (Delegate method in delArray)
                {
                    try
                    {
                        strList.Add(method.DynamicInvoke(100));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error Message:" + ex.Message);
                    }
                }
            }
            return strList.ToArray();
        }
    }

    public delegate string GeneralEventHandler(int num);

    public class Publisher
    {
        public event EventHandler MyEvent;

        public void DoSomething()
        {
            Console.WriteLine("Do something invoked");
            if (MyEvent != null)
            {
                Delegate[] delArray = MyEvent.GetInvocationList();
                foreach (var del in delArray)
                {
                    EventHandler method = (EventHandler)del;
                    method.BeginInvoke(null, EventArgs.Empty, null, null);
                }
            }

        }
    }
    public class Subscriber1
    {
        public void OnNumberChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Subscriber1 Invoked");
        }
    }

    public class Subscriber2
    {
        public void OnNumberChanged(object sender, EventArgs e)
        {
            Thread.Sleep(3000);
            Console.WriteLine("Subscriber2 Invoked");
        }
    }
    public class Subscriber3
    {
        public void OnNumberChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Subscriber3 Invoked");
        }
    }
}
