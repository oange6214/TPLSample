using System.Threading.Tasks.Dataflow;

namespace TPLDataflowByExample.Link
{
    /// <summary>
    /// Multiple Sources
    /// </summary>
    public class LinkExample3
    {
        public static void Run()
        {
            var source1 = MakeDelayBlock(1000);
            var source2 = MakeDelayBlock(800);

            var printBlock = new ActionBlock<int>(n => Console.WriteLine(n));

            for (int i = 0; i < 10; i++)
            {
                source1.Post(i);
                source2.Post(i - 2 * i); // negate i  
            }

            source1.LinkTo(printBlock);
            source2.LinkTo(printBlock);
        }

        private static TransformBlock<int, int> MakeDelayBlock(int delay)
        {
            return new TransformBlock<int, int>(n => {
                Thread.Sleep(new Random().Next(delay));
                return n;
            });
        }
    }
}
