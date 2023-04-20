using System.Threading.Tasks.Dataflow;

namespace TPLDataflowByExample.ExecutionBlock.BroadcastBlock
{
    public class BroadcastBlockExample1
    {
        public static void Run()
        {
            var printer1 = MakePrintBlock("printer1");
            var printer2 = MakePrintBlock("printer2");

            var bcBlock = new BroadcastBlock<int>(n =>
            {
                return n * 10;
            });

            bcBlock.LinkTo(printer1);
            bcBlock.LinkTo(printer2);

            for (int i = 0; i < 10; i++)
            {
                bcBlock.Post(i);
            }
            Console.WriteLine("Done");
        }

        static ActionBlock<int> MakePrintBlock(string prefix)
        {
            return new ActionBlock<int>(n => Console.WriteLine(prefix + ": " + n));
        }
    }
}
