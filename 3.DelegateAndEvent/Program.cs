using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.DelegateAndEvent
{

    public delegate void NumberChangedEventHandler(int count);
    public delegate void GreetingDelegate(string name);

    class Publisher
    {
        private int count;
        public event NumberChangedEventHandler NumberChanged;

        public void DoSomething()
        {
            if (NumberChanged != null)
            {
                count++;
                NumberChanged(count);
            }

        }

    }
    class Subscriber
    {
        public void OnNumberChanged(int count)
        {
            Console.WriteLine("Subsucriber notified: count = {0}", count);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Publisher pub = new Publisher();
            Subscriber sub = new Subscriber();

            pub.NumberChanged += new NumberChangedEventHandler(sub.OnNumberChanged);
            pub.DoSomething();
            pub.DoSomething();
            pub.DoSomething();


            //GreetingDelegate delegate1;
            //delegate1 = EnglishGreeting;
            //delegate1 += ChineseGreeting;
            //delegate1 -= ChineseGreeting;
            //delegate1("tom");

            ////使用接口 利用多态特性实现
            //GreetingPeople("小红", new ChineseGreeting());
            //GreetingPeople("tom", new EnglishGreeting());

            //GreetingManager gm = new GreetingManager();
            //gm.delegate1 += ChineseGreeting;
            //gm.delegate1 += EnglishGreeting;
            //gm.Greeting("tom");

            Console.Read();
        }



        public static void Greeting(string name, GreetingDelegate greeting)
        {
            greeting(name);
        }

        public static void ChineseGreeting(string name)
        {
            Console.WriteLine("早上好," + name);
        }

        public static void EnglishGreeting(string name)
        {
            Console.WriteLine("Good morning," + name);
        }

        public static void GreetingPeople(string name, IGreeting greeting)
        {
            greeting.GreetingPeople(name);
        }
    }

    class GreetingManager
    {
        public delegate void GreetingDelegate(string name);

        public event GreetingDelegate delegate1;

        public void Greeting(string name)
        {
            if (delegate1 != null)
                delegate1(name);
        }
    }

    #region 多态实现
    public interface IGreeting
    {
        void GreetingPeople(string name);
    }

    public class EnglishGreeting : IGreeting
    {
        public void GreetingPeople(string name)
        {
            Console.WriteLine("Good morning," + name);
        }
    }

    public class ChineseGreeting : IGreeting
    {
        public void GreetingPeople(string name)
        {
            Console.WriteLine("早上好," + name);
        }
    }
    #endregion



}
