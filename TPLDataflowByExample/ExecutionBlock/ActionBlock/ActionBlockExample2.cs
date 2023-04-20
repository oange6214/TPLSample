using System.Threading.Tasks.Dataflow;

namespace TPLDataflowByExample.ExecutionBlock.ActionBlock
{
    public class ActionBlockExample2
    {
        public static void Run()
        {
            Console.WriteLine($"Run thread id : {Environment.CurrentManagedThreadId}");

            var actionBlock = new ActionBlock<int>(n =>
            {
                Thread.Sleep(1000);
                Console.WriteLine(n);
            });

            for (int i = 0; i < 10; i++)
            {
                actionBlock.Post(i);
            }
            Console.WriteLine($"Done thread id : {Environment.CurrentManagedThreadId}");
        }
    }
}
