using System.Threading.Tasks.Dataflow;

namespace TPLDataflowByExample.Link
{
    public static class LinkExample7
    {
        public static async Task Run()
        {
            // 建立一個 BufferBlock 並設置其容量為 10
            var bufferBlock = new BufferBlock<int>(new DataflowBlockOptions { BoundedCapacity = 10 });

            // 建立五個 ActionBlock，每個都執行相同的任務
            //var actionBlocks = new List<ActionBlock<int>>();
            //for (int i = 0; i < 5; i++)
            //{
            //    actionBlocks.Add(new ActionBlock<int>(async num =>
            //    {
            //        Console.WriteLine($"ActionBlock {i} processing numbers {num}");
            //        await Task.Delay(1000);
            //    }));
            //}

            //// 將 BufferBlock 和每個 ActionBlock 連接
            //foreach (var actionBlock in actionBlocks)
            //{
            //    bufferBlock.LinkTo(actionBlock, new DataflowLinkOptions { PropagateCompletion = true });
            //}


            var actionBlock1 = new ActionBlock<int>(async num =>
            {
                await Task.Delay(100);
                Console.WriteLine($"ActionBlock1: {num}");
            }, new ExecutionDataflowBlockOptions { BoundedCapacity = 10 });

            var actionBlock2 = new ActionBlock<int>(async num =>
            {
                await Task.Delay(100);
                Console.WriteLine($"ActionBlock2: {num}");
            }, new ExecutionDataflowBlockOptions { BoundedCapacity = 10 });

            var actionBlock3 = new ActionBlock<int>(async num =>
            {
                await Task.Delay(100);
                Console.WriteLine($"ActionBlock3: {num}");
            }, new ExecutionDataflowBlockOptions { BoundedCapacity = 10 });

            var actionBlock4 = new ActionBlock<int>(async num =>
            {
                await Task.Delay(100);
                Console.WriteLine($"ActionBlock4: {num}");
            }, new ExecutionDataflowBlockOptions { BoundedCapacity = 10 });

            var actionBlock5 = new ActionBlock<int>(async num =>
            {
                await Task.Delay(100);
                Console.WriteLine($"ActionBlock5: {num}");
            }, new ExecutionDataflowBlockOptions { BoundedCapacity = 10 });

            bufferBlock.LinkTo(actionBlock1, new DataflowLinkOptions { PropagateCompletion = true });
            bufferBlock.LinkTo(actionBlock2, new DataflowLinkOptions { PropagateCompletion = true });
            bufferBlock.LinkTo(actionBlock3, new DataflowLinkOptions { PropagateCompletion = true });
            bufferBlock.LinkTo(actionBlock4, new DataflowLinkOptions { PropagateCompletion = true });
            bufferBlock.LinkTo(actionBlock5, new DataflowLinkOptions { PropagateCompletion = true });



            // 填充 BufferBlock
            for (int i = 0; i < 50; i++)
            {
                await bufferBlock.SendAsync(i);
            }

            // 標記 BufferBlock 為已完成並等待所有 ActionBlock 完成
            bufferBlock.Complete();
            //await Task.WhenAll(actionBlocks.Select(ab => ab.Completion));
            await Task.WhenAll(actionBlock1.Completion, actionBlock2.Completion, actionBlock3.Completion, actionBlock4.Completion, actionBlock5.Completion);


            Console.WriteLine("Finished.");
        }
    }
}
