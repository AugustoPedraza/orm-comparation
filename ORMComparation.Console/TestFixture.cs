using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMComparation.ConsoleApp
{
    public class TestFixture : List<TestCase>
    {
        public void Add(Action<byte> action, string name)
        {
            Add(new TestCase { Action = action, Name = name });
        }

        public void Run(int iterations)
        {
            //warmup
            foreach (var testcase in this)
            {
                testcase.Action((byte)(iterations % 4 + 1));
                testcase.Watch = new Stopwatch();
                testcase.Watch.Reset();
            }

            var random = new Random();

            for (int i = 1; i <= iterations; i++)
            {
                var status = new Random().Next(1, 3);
                foreach(var testcase in this.OrderBy( x => random.Next()))
                {
                    testcase.Watch.Start();
                    testcase.Action((byte)status); //Execute the action
                    testcase.Watch.Stop();
                }
            }

            foreach (var testcase in this.OrderBy(t => t.Watch.ElapsedMilliseconds))
            {
                Console.WriteLine("{0} took {1} ms", testcase.Name, testcase.Watch.ElapsedMilliseconds);
            }
        }
    }
}
