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
            var entities = new NorthwindModel.NorthwindEntities(new Uri(serviceUri)); // breakpoint #1.1

            IAsyncResult asyncResult = entities.Employees.BeginExecute((ar) =>
            {
                var employees = entities.Employees.EndExecute(ar); // breakpoint #1.2

                Console.WriteLine("Employees in Northwind service:");
                foreach (var person in employees)
                {
                    Console.WriteLine("\t{0} {1}", person.FirstName, person.LastName);
                }
            }, null);

            WaitHandle.WaitAny(new[] { asyncResult.AsyncWaitHandle }); // breakpoint #1.3
        }
    }
}
