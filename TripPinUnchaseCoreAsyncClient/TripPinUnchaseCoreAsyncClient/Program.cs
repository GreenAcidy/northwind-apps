using System;
using System.Collections.Generic;
using System.Linq;

namespace TripPinUnchaseCoreAsyncClient 
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            const string serviceUri = "https://services.odata.org/TripPinRESTierService";
            var container = new Trippin.Container(new Uri(serviceUri));

            Console.WriteLine("People in TripPin service:");
            var people = await container.People.ExecuteAsync();

            foreach (var person in people)
            {
                Console.WriteLine("\t{0} {1}", person.FirstName, person.LastName);
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }
    }
}