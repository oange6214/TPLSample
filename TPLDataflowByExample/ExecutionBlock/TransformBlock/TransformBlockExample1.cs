using System.Threading.Tasks.Dataflow;

namespace TPLDataflowByExample.ExecutionBlock.TransformBlock
{
    public class TransformBlockExample1
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

            for (int i = 0; i < 10; i++)
            {
                int result = tfBlock.Receive();
                Console.WriteLine(result);
            }

            Console.WriteLine($"Done thread id : {Environment.CurrentManagedThreadId}");
        }
    }
}
