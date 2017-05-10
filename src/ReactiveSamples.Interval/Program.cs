using System;
using System.Linq;
using System.Reactive.Linq;

namespace ReactiveSamples.Interval
{
    class Program
    {
        static void Main(string[] args)
        {
            IObservable<long> observable = Observable.Interval(TimeSpan.FromMilliseconds(500)).Take(5);

            observable.Subscribe(
                value => Console.WriteLine($"OnNext: {value}"),
                () => Console.WriteLine("OnCompleted"));

            Console.ReadKey();
        }
    }
}
