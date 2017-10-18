using System;
using ConsoleWriter; 

class Client
{
    public static void Main()
    {
        Writer writer = new Writer();
        Console.WriteLine("Client code executes");
        writer.Write();
		Console.ReadLine();
    }
}