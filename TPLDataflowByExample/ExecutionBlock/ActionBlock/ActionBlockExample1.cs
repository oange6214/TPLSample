using System.Threading.Tasks.Dataflow;

namespace TPLDataflowByExample.ExecutionBlock.ActionBlock
{
    public class ActionBlockExample1
    {
        public static void Run()
        {
            Console.WriteLine($"Run thread id : {Environment.CurrentManagedThreadId}");

            var actionBlock = new ActionBlock<int>(Console.WriteLine);

            for (int i = 0; i < 10; i++)
            {
                actionBlock.Post(i);
            }

            Console.WriteLine($"Done thread id : {Environment.CurrentManagedThreadId}");
        }
    }
}