using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace TPLReactiveByExample
{
    public class CancelationTokenReactive
    {
        public static void Run()
        {
            var source = Observable.Interval(TimeSpan.FromSeconds(1))
                .Do(x => Console.WriteLine($"Emitting: {x}"))
                .Take(10)
                .Finally(() => Console.WriteLine("Done!"));

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            var token = cts.Token;

            var cancellationTokenObservable = Observable.Create<Unit>(observer =>
            {
                token.Register(() =>
                {
                    observer.OnNext(Unit.Default);
                    observer.OnCompleted();
                });

                return Disposable.Empty;
            });

            var subscription = source
                .TakeUntil(cancellationTokenObservable)
                .Subscribe(
                    x => Console.WriteLine($"Received: {x}"),
                    ex => Console.WriteLine($"Error: {ex}"),
                    () => Console.WriteLine("Completed!")
                );

            cancellationTokenObservable.Subscribe(_ => Console.WriteLine("Task cancelled."));
        }

    }
}