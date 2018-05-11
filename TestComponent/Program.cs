using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleDistanceWebAppMVC.ViewModels;

namespace TestComponent
{
    class Program
    {
        static void Main(string[] args)
        {
            DistanceRoute route = new DistanceRoute();

            Console.Write("Enter the first place >> ");
            route.Location1 = Console.ReadLine();

            Console.Write("Enter the second place >> ");
            route.Location2 = Console.ReadLine();

            route.FindDistance();

            Console.WriteLine("Distance = " + route.Distance + "miles.");
            Console.WriteLine("Duration = " + route.Duration + "in time.");
        }
    }
}
