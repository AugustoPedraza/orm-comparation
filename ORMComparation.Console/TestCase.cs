using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMComparation.ConsoleApp
{
    public class TestCase
    {
        public Action<byte> Action { get; set; }
        public string Name { get; set; }
        public Stopwatch Watch { get; set; }
    }
}
