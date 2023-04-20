using System.Threading.Tasks.Dataflow;

namespace TPLDataflowByExample.ExecutionBlock.ActionBlock
{
    public class ActionBlockExample3
    {
        public static async Task Run()
        {
            Console.WriteLine($"Run thread id : {Environment.CurrentManagedThreadId}");

            var actionBlock = new ActionBlock<int>(n =>
            {
                Thread.Sleep(1000);
                Console.WriteLine($"Accepted: {n}, thread id: {Environment.CurrentManagedThreadId}");
            });

            List<Task<bool>> tasks = new List<Task<bool>>();

            for (int i = 0; i < 100; i++)
            {
                Task<bool> t = actionBlock.SendAsync(i);
                tasks.Add(t);
            }

            while (tasks.Count > 0)
            {
                Task<bool> completedTask = await Task.WhenAny(tasks);
                tasks.Remove(completedTask);
            }

            Console.WriteLine($"Done thread id : {Environment.CurrentManagedThreadId}");

        }
    }
}
