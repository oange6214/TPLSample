using System.Threading.Tasks.Dataflow;

namespace TPLDataflowByExample.Cancellation
{
    public class CancellationTokenSourceExample1
    {
        public static async Task Run()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));

            var actionBlock = new ActionBlock<int>(async i =>
            {
                await Task.Delay(5000, cts.Token); // 假設這裡需要等待 5 秒鐘才能處理輸入資料
                                                   // 處理輸入資料的代碼
            }, new ExecutionDataflowBlockOptions { CancellationToken = cts.Token });

            try
            {
                // 傳遞輸入資料到 ActionBlock
                actionBlock.Post(1);

                // 等待 ActionBlock 完成處理所有輸入資料
                await actionBlock.Completion;
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine($"Operation was cancelled due to timeout: {ex.Message}");
            }
        }

    }
}
