using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.Genericity
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] nums = new int[] { 1, 2, 3 };

            char[] chars = new char[] { 'a', 'b' };

            //泛型方法
            SortHepler.Sort(nums);
            SortHepler.Sort(chars);
            SortHepler.Sort<char>(chars);

            //泛型约束
            Person[] persons = new Person[] { new Person("tom", 30) };
            SortHepler.Sort<Person>(persons);
        }
    }

    public class Person : IComparable
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }

    public static class SortHepler
    {

        public static void Sort<T>(T[] nums) where T : IComparable
        {
            Console.WriteLine("Sort");
        }

    }
}
