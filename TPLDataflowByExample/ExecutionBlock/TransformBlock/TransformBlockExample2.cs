using System.Threading.Tasks.Dataflow;

namespace TPLDataflowByExample.ExecutionBlock.TransformBlock
{
    public class TransformBlockExample2
    {
        public static void Run()
        {
            Console.WriteLine($"Run thread id : {Environment.CurrentManagedThreadId}");

            Func<int, int> fn = n =>
            {
                Thread.Sleep(1000);
                return n * n;
            };

            var tfBlock = new TransformBlock<int, int>(fn);
            for (int i = 0; i < 10; i++)
            {
                tfBlock.Post(i);
            }

            // RecieveAsync returns a Task
            for (int i = 0; i < 10; i++)
            {
                Task<int> resultTask = tfBlock.ReceiveAsync();
                int result = resultTask.Result;

                // Calling Result will wait until it has a value ready
                Console.WriteLine(result);
            }

            Console.WriteLine($"Done thread id : {Environment.CurrentManagedThreadId}");
        }
    }
}
