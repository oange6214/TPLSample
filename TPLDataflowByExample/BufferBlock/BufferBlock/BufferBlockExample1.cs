﻿using System.Threading.Tasks.Dataflow;

namespace TPLDataflowByExample.BufferBlock.BufferBlock
{
    public class BufferBlockExample1
    {
        public static void Run()
        {
            var bufferBlock = new BufferBlock<int>();

            for (int i = 0; i < 10; i++)
            {
                bufferBlock.Post(i);
            }

            for (int i = 0; i < 10; i++)
            {
                int result = bufferBlock.Receive();
                Console.WriteLine(result);
            }

            Console.WriteLine("Done");
        }
    }
}
