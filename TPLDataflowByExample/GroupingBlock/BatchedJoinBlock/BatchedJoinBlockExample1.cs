using System.Text;
using System.Threading.Tasks.Dataflow;
using TPLDataflowByExample.Common;

namespace TPLDataflowByExample.GroupingBlock.BatchedJoinBlock
{
    public class BatchedJoinBlockExample1
    {
        public static void Run()
        {
            var bjBlock = new BatchedJoinBlock<int, int>(2);

            for (int i = 0; i < 10; i++)
            {
                bjBlock.Target1.Post(i);
            }

            for (int i = 0; i < 10; i++)
            {
                bjBlock.Target2.Post(i);
            }

            for (int i = 0; i < 10; i++)
            {
                Tuple<IList<int>, IList<int>> result = bjBlock.Receive();
                Console.WriteLine(Util.TupleListToString(result));
            }

            Console.WriteLine("Done");
        }
    }
}
