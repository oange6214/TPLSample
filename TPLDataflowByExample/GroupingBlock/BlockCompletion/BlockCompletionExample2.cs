using System.Threading.Tasks.Dataflow;

namespace TPLDataflowByExample.GroupingBlock.BlockCompletion
{
    public class BlockCompletionExample2
    {
        public static void Run()
        {
            var block = new ActionBlock<bool>(_ =>
            {
                Console.WriteLine("Block started");
                Thread.Sleep(5000);
                Console.WriteLine("Block ended");
            });

            block.Post(true);
            Console.WriteLine("Waiting");

            block.Complete();
            block.Completion.Wait();
            Console.WriteLine("Task done");
        }
    } 
}
