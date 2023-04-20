using System.Diagnostics;
using System.Threading.Tasks.Dataflow;

namespace TPLDataflowByExample.Link
{
    public class LinkExample6
    {
        public static async Task Run()
        {
            var actionBlock1 = GeneralActionBlock("JOJO", 5000);
            var actionBlock2 = GeneralActionBlock("HOWHOW", 1000);
            var actionBlock3 = GeneralActionBlock("KK", 2000);

            var bufferBlock = new BufferBlock<int>();


            bufferBlock.LinkTo(actionBlock1, new DataflowLinkOptions { PropagateCompletion = true });
            bufferBlock.LinkTo(actionBlock2, new DataflowLinkOptions { PropagateCompletion = true });
            bufferBlock.LinkTo(actionBlock3, new DataflowLinkOptions { PropagateCompletion = true });

            for (int i = 0; i < 10; i++)
            {
                await bufferBlock.SendAsync(i);
            }

            bufferBlock.Complete();
            await Task.WhenAll(actionBlock1.Completion, actionBlock2.Completion, actionBlock3.Completion);

            Console.WriteLine("Done");
        }

        private static ActionBlock<int> GeneralActionBlock(string id, int workTime = 1000)
        {
            return new ActionBlock<int>(async i =>
            {
                Console.WriteLine($"ActionBlock [{id}] data processing: {i}...");
                await Task.Delay(workTime);                                                 // 模擬長時間執行的工作
                Console.WriteLine($"ActionBlock [{id}] data processing: {i} finish！");

            }, new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 1, BoundedCapacity = 1, EnsureOrdered = true });
        }
    }
}
