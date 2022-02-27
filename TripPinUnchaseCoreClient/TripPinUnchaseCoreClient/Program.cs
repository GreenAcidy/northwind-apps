const string serviceUri = "https://services.odata.org/TripPinRESTierService";
var container = new Trippin.Container(new Uri(serviceUri));

IAsyncResult asyncResult = container.People.BeginExecute((ar) =>
{
    Console.WriteLine("People in TripPin service:");
    var people = container.People.EndExecute(ar);

    foreach (var person in people)
    {
        Console.WriteLine("\t{0} {1}", person.FirstName, person.LastName);
    }

}, null);

WaitHandle.WaitAny(new[] { asyncResult.AsyncWaitHandle });

Console.WriteLine("Press any key to continue.");
Console.ReadLine();
