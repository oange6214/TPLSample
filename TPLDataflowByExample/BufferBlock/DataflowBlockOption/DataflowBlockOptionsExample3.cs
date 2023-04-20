using System.Diagnostics;
using System.Threading.Tasks.Dataflow;

namespace TPLDataflowByExample.BufferBlock.DataflowBlockOption
{
    public class DataflowBlockOptionsExample3
    {
        public static void Run()
        {
            var block1 = new BufferBlock<int>(new DataflowBlockOptions
            {
                NameFormat = "Fu"
            });

            var block2 = new BufferBlock<int>(new DataflowBlockOptions
            {
                NameFormat = "Bar, Class: {0}, Id: {1}"
            });

            Debug.Assert(false);
        }
    }
}
