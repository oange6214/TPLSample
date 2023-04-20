using System.Threading.Tasks.Dataflow;

namespace TPLDataflowByExample.DataflowLinkOption
{
    public class DataflowLinkOptionsExample2
    {
        public static void Run()
        {
            var block1 = MakePrinter("block1");
            var block2 = MakePrinter("block2");

            var source = new BufferBlock<int>();
            source.LinkTo(block1);

            var opt = new DataflowLinkOptions { Append = false };
            source.LinkTo(block2, opt);

            for (int i = 0; i < 10; i++)
            {
                source.SendAsync(i);
            }
        }

        private static ActionBlock<int> MakePrinter(string prefix)
        {
            return new ActionBlock<int>(
                   n =>
                   {
                       Thread.Sleep(1000);
                       Console.WriteLine(prefix + ": " + n);
                   });
        }
    }
}
