using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace TPLDataflowByExample.GroupingBlock.GroupingDataflowBlockOption
{
    public class GroupingDataflowBlockOptionsExample2
    {
        public static void Run()
        {
            var opts = new GroupingDataflowBlockOptions { Greedy = false };

            var jBlock = new JoinBlock<int, int>(opts);

            for (int i = 0; i < 20; i++)
            {
                Task<bool> task = jBlock.Target1.SendAsync(i);

                // needed to capture 'i' so we can use it in `ContinueWith`
                int iCopy = i;

                task.ContinueWith(t =>
                {
                    if (t.Result)
                    {
                        Console.WriteLine($"Target1 accepted: {iCopy}, thread id: { Environment.CurrentManagedThreadId}");
                    }
                    else
                    {
                        Console.WriteLine($"Target1 REFUSED: {iCopy}, thread id: {Environment.CurrentManagedThreadId}");
                    }
                });
            }

            for (int i = 0; i < 20; i++)
            {
                Task<bool> task = jBlock.Target2.SendAsync(i);

                // needed to capture 'i' so we can use it in `ContinueWith`
                int iCopy = i;

                task.ContinueWith(t =>
                {
                    if (t.Result)
                    {
                        Console.WriteLine($"Target2 accepted: {iCopy}, thread id: {Environment.CurrentManagedThreadId}");
                    }
                    else
                    {
                        Console.WriteLine($"Target2 REFUSED: {iCopy}, thread id: {Environment.CurrentManagedThreadId}");
                    }
                });
            }

            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(jBlock.Receive());
            }
            Console.WriteLine("Done");
        }
    }
}
