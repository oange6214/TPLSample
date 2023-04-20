using System.Diagnostics;
using System.Threading.Tasks.Dataflow;

namespace TPLDataflowByExample.ExecutionBlock.ExecutionDataflowBlockOption
{
    public class ExecutionDataflowBlockOptionsExample2
    {
        const int ITERS = 6000000;

        public static void Benchmark1()
        {
            var sw = new Stopwatch();
            var are = new AutoResetEvent(false);
            var ab = new ActionBlock<int>(i =>
            {
                if (i == ITERS) are.Set();
            }
            );

            while (true)
            {
                sw.Restart();

                for (int i = 1; i <= ITERS; i++) ab.Post(i);

                are.WaitOne();
                sw.Stop();

                Console.WriteLine("Messages / sec: {0:N0}", ITERS / sw.Elapsed.TotalSeconds);
            }
        }

        public static void Benchmark2()
        {
            var sw = new Stopwatch();
            var are = new AutoResetEvent(false);
            var ab = new ActionBlock<int>(i =>
            {
                if (i == ITERS) are.Set();
            },
            new ExecutionDataflowBlockOptions
            {
                SingleProducerConstrained = true
            });

            while (true)
            {
                sw.Restart();

                for (int i = 1; i <= ITERS; i++) ab.Post(i);

                are.WaitOne();
                sw.Stop();

                Console.WriteLine("Messages / sec: {0:N0}", ITERS / sw.Elapsed.TotalSeconds);
            }
        }
    }
}
