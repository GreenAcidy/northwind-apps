using System;
using System.Collections.Generic;
using System.Linq;

namespace NorthwindServiceCoreClient // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string serviceUri = "https://services.odata.org/V3/Northwind/Northwind.svc/";
            var entities = new NorthwindModel.NorthwindEntities(new Uri(serviceUri));

            IAsyncResult asyncResult = entities.Employees.BeginExecute((ar) =>
            {
                Console.WriteLine("People in Northwind service:");
                var people = entities.Employees.EndExecute(ar);

                foreach (var person in people)
                {
                    Console.WriteLine("\t{0} {1}", person.FirstName, person.LastName);
                }

            }, null);

            WaitHandle.WaitAny(new[] { asyncResult.AsyncWaitHandle });

            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }
    }
}
