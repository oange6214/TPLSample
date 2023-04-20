using System.Threading.Tasks.Dataflow;

namespace TPLDataflowByExample.BufferBlock.DataflowBlockOption
{
    public class DataflowBlockOptionsExample1
    {
        public static void Run()
        {
            Action<int> fn = n =>
            {
                Thread.Sleep(1000);
                Console.WriteLine(n);
            };

            var opts = new ExecutionDataflowBlockOptions { BoundedCapacity = 1 };

            // Sets the block's buffer size to one message.
            var actionBlock = new ActionBlock<int>(fn, opts);

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(actionBlock.Post(i));
                //actionBlock.SendAsync(i);
            }

            Console.Write("Done");
        }
    }
}
