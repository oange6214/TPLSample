using System.Diagnostics;
using System.Text;

namespace TPLDataflowByExample.Common
{
    public class Util
    {
        public static void Log()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();



            stopwatch.Stop();
            Console.WriteLine("Time: {0}", stopwatch.Elapsed);
        }

        public static string TupleListToString(Tuple<IList<int>, IList<int>> sources)
        {

            StringBuilder stringBuilder = new();

            stringBuilder.Append("[");
            stringBuilder.Append(CombineString(sources.Item1));
            stringBuilder.Append("] [");
            stringBuilder.Append(CombineString(sources.Item2));
            stringBuilder.Append("]");

            return stringBuilder.ToString();
        }

        private static string CombineString(IList<int> lists)
        {
            string combine = string.Empty;
            int count = lists.Count - 1;

            foreach (int source in lists)
            {
                if (count != 0)
                    combine += source.ToString() + ",";
                else
                    combine += source.ToString();

                count--;
            }

            return combine;
        }
    }
}
