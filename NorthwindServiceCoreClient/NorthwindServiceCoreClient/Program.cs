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

            ManualResetEventSlim mre = new ManualResetEventSlim(); // (1) - Инициализация примитива синхронизации "событие".

            IAsyncResult asyncResult = entities.Employees.BeginExecute((ar) =>
            {
                var employees = entities.Employees.EndExecute(ar); // breakpoint #1.2

                Console.WriteLine("Employees in Northwind service:");
                foreach (var person in employees)
                {
                    Console.WriteLine("\t{0} {1}", person.FirstName, person.LastName);
                }

                mre.Set(); // (2) - Отправить сигнал методу WaitAll.

            }, null);

            WaitHandle.WaitAny(new[] { mre.WaitHandle }); // (3) - Блокировать поток выполнения, пока не будет получен сигнал.
        }
    }
}
