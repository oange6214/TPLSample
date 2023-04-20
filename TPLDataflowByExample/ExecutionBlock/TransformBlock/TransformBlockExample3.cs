using System.Threading.Tasks.Dataflow;

namespace TPLDataflowByExample.ExecutionBlock.TransformBlock
{
    public class TransformBlockExample3
    {
        public static void Run()
        {
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

            Action<Task<int>> whenReady = task =>
            {
                int n = task.Result;
                Console.WriteLine(n);
            };

            for (int i = 0; i < 10; i++)
            {
                Task<int> resultTask = tfBlock.ReceiveAsync();
                resultTask.ContinueWith(whenReady);
                // When 'resultTask' is done, call 'whenReady' with the Task.
            }

            Console.WriteLine("Done");
        }
    }
}
