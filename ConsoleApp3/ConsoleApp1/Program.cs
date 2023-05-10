using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static Thread thread;
        static int people = 50;
        static int sizeTrain = 10;
        static int size = 0;
        static object block = new object();
        static void Main(string[] args)
        {
            takeRide(people);
            Console.ReadLine();
        }
        public static void takeRide(int sizePesenger)
        {
            Console.WriteLine("Викликаємо вагончик");
            for (int i = 1; i <= sizePesenger; i++)
            {
                thread = new Thread(load);
                thread.Name = $"Пасажир {i}";
                thread.Start();
            }


        }
        public static void load()
        {
            Monitor.Enter(block);
            Console.WriteLine($"{Thread.CurrentThread.Name} ciв до вагончика");
            Thread.Sleep(1000);
            unload();

        }
        public static void unload()
        { 
            Console.WriteLine($"{Thread.CurrentThread.Name} вийшов з вагончика");
            Monitor.Exit(block);
  
        }
    }
}
