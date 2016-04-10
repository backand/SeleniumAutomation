using Core;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Playground
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Task task = Task.Factory.StartNew(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (sw.Elapsed < TimeSpan.FromSeconds(10))
                {
                }
                Console.WriteLine("Test aborted due to timeout.");
            });

            Task task2 = Task.Factory.StartNew(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (true)
                {
                    Thread.Sleep(2000);
                    Console.WriteLine(sw.Elapsed);
                }
            });

            Task.WaitAny(task, task2);
            if(task.Status == TaskStatus.RanToCompletion)
                throw new TimeoutException();
            Console.ReadKey();
        }
    }

    public class A
    {
        
    }

    public class B : A
    {
        
    }

    public class C : B
    {
        
    }
}