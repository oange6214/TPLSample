using System.Threading.Tasks.Dataflow;

namespace TPLDataflowByExample.GroupingBlock.GroupingDataflowBlockOption
{
    public class GroupingDataflowBlockOptionsExample1
    {
        public static void Run()
        {
            var opts = new GroupingDataflowBlockOptions { Greedy = false };

            var jBlock = new JoinBlock<int, int>(opts);

            for (int i = 0; i < 10; i++)
            {
                if (jBlock.Target1.Post(i))
                {
                    Console.WriteLine($"Target1 accepted: {i}, thread id: {Environment.CurrentManagedThreadId}");
                }
                else
                {
                    Console.WriteLine($"Target1 REFUSED: {i}, thread id: {Environment.CurrentManagedThreadId}");
                }
            }

            for (int i = 0; i < 10; i++)
            {
                if (jBlock.Target2.Post(i))
                {
                    Console.WriteLine($"Target2 accepted: {i}, thread id: {Environment.CurrentManagedThreadId}");
                }
                else
                {
                    Console.WriteLine($"Target2 REFUSED: {i}, thread id: {Environment.CurrentManagedThreadId}");
                }
            }

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(jBlock.Receive());
            }
            Console.WriteLine("Done");
        }
    }
}
