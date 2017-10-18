using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GACLibrary;

namespace GACTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person("Artyom");
            Console.WriteLine(person.Name);

            Console.ReadLine();
        }
    }
}
