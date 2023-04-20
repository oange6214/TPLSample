using System.Threading.Tasks.Dataflow;

namespace TPLDataflowByExample.BufferBlock.DataflowBlockOption
{
    public class DataflowBlockOptionsExample2
    {
        public static void Run()
        {
            Action<int> fn = n =>
            {
                Thread.Sleep(1000);
                Console.WriteLine($"{n} ThreadId: {Environment.CurrentManagedThreadId}");
            };

            var opts = new ExecutionDataflowBlockOptions
            {
                MaxMessagesPerTask = 1
            };

            // Each Task will only process one message
            // A new task will be created for every new message
            var actionBlock = new ActionBlock<int>(fn, opts);

            for (int i = 0; i < 10; i++)
            {
                actionBlock.Post(i);
            }

            Console.WriteLine("Done");
        }
    }
}
