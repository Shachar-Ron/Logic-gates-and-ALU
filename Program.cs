using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class Program
    {
         static void Main(string[] args)
        {

            AndGate and = new AndGate();
            if (!and.TestGate())
                Console.WriteLine("bugbug");

            


            OrGate.Corrupt = true;
            if(and.TestGate())
                Console.WriteLine("bugbug");


            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
