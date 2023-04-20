using TPLReactiveByExample;


Console.WriteLine($"Start Main thread id : {Environment.CurrentManagedThreadId}");

CancelationTokenReactive.Run();

Console.WriteLine($"End Main thread id : {Environment.CurrentManagedThreadId}");