using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.ObserverPattern
{
    /// <summary>
    /// Observer是一种松耦合的设计模式，适用于监听某个状态变化，当状态变化时通知所有订阅了该事件的人
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //Heater heater = new Heater();
            //heater.Boiled += Alarm.Alert;
            //heater.Boiled += Monitor.Display;
            //heater.BoilWater();

            Heater heater = new Heater();
            IObserver alarm = new Alarm();
            IObserver monitor = new Monitor();
            heater.Register(alarm);
            heater.Register(monitor);
            heater.BoilWater();

            Console.Read();
        }
    }

    public interface IObservable
    {
        void Register(IObserver obj);
        void UnRegister(IObserver obj);
    }
    public interface IObserver
    {
        void Update(EventArgs e);
    }

    public abstract class SubjectBase : IObservable
    {
        private List<IObserver> container = new List<IObserver>();

        public void Register(IObserver obj)
        {
            container.Add(obj);
        }

        public void UnRegister(IObserver obj)
        {
            container.Remove(obj);
        }
        protected virtual void Notify(EventArgs e)
        {
            foreach (IObserver observer in container)
            {
                observer.Update(e);
            }
        }
    }

    public class BoiledEventArgs : EventArgs
    {
        public readonly int temperature;
        public string type;
        public BoiledEventArgs(int temperature, string type)
        {
            this.temperature = temperature;
            this.type = type;
        }
    }
    public class Heater : SubjectBase
    {
        private int temperature;

        public string type = "RealFire 001";
        public string area = "China";
        public delegate void BoilEventHandler(Object sender, BoiledEventArgs e);
        public event BoilEventHandler Boiled;


        protected virtual void OnBoiled(BoiledEventArgs e)
        {
            base.Notify(e);
        }

        public void BoilWater()
        {
            for (int i = 0; i <= 100; i++)
            {
                temperature = i;
                if (temperature >= 95)
                {
                    BoiledEventArgs e = new BoiledEventArgs(temperature, type);
                    OnBoiled(e);
                }

            }
        }

         
    }
    public class Alarm : IObserver
    {
        public static void Alert(Object sender, BoiledEventArgs e)
        {
            Console.WriteLine("滴滴滴，当前温度:{0},型号:{1}", e.temperature, ((Heater)sender).type);
        }

        public void Update(EventArgs e)
        {
            var args = (BoiledEventArgs)e;
            Console.WriteLine("滴滴滴，当前温度:{0},型号:{1}", args.temperature, args.type);
        }
    }
    public class Monitor : IObserver
    {
        public static void Display(Object sender, BoiledEventArgs e)
        {
            Console.WriteLine("水开啦，当前温度:{0},型号:{1}", e.temperature, ((Heater)sender).type);
        }
        public void Update(EventArgs e)
        {
            var args = (BoiledEventArgs)e;
            Console.WriteLine("水开啦，当前温度:{0},型号:{1}", args.temperature, args.type);
        }
    }
}
