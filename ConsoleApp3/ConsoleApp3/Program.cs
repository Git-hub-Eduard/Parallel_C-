using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Program
    {
        static int  people = 50;
        static int sizetrain = 5;
        static int _sizetrain = 5;
        static Semaphore semaphorePas;
        static Thread  thread;
        static void Main(string[] args)
        {
            int sizeSemaphore = sizetrain;
            semaphorePas = new Semaphore(5, sizeSemaphore);
            takeRide(people);
            Console.ReadLine();

        }
        public static void takeRide(int sizePesenger )
        {
            Console.WriteLine("Викликаємо вагончик");
            for(int i = 1; i <= sizePesenger; i++)
            {
                thread = new Thread(load);
                thread.Name = $"Пасажир {i}";
                thread.Start();
            }
            

        }
        public static void load()
        {
           
           while(sizetrain>0)
           {
                semaphorePas.WaitOne();
                Console.WriteLine($"{Thread.CurrentThread.Name} ciв до вагончика");
                Thread.Sleep(2000);
                unload();
                sizetrain--;
           }

        }
        public  static void unload()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} вийшов з вагончика");
            Thread.Sleep(2000);
            semaphorePas.Release();
        }
    }
}
