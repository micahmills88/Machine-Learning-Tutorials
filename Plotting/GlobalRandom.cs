using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotting
{
    public static class GlobalRandom
    {
        private static Random rnd = new Random();

        public static float NextFloat()
        {
            //mean of 0 and std deviation of 1
            //matches test in mathnet random
            double u1 = 1.0 - rnd.NextDouble();
            double u2 = 1.0 - rnd.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            return (float)randStdNormal;
        }

        public static PlotPoint[] BuildRandom(
            float xmean, float xdeviation,
            float ymean, float ydeviation,
            float angle, int count
        )
        {
            PlotPoint[] out_data = new PlotPoint[count];
            for (int i = 0; i < count; i++)
            {
                float xval = NextFloat() * xdeviation + xmean;
                float yval = NextFloat() * ydeviation + ymean;
                out_data[i] = new PlotPoint(xval, yval).RotateAround(angle, new PlotPoint(xmean, ymean));
            }
            return out_data;
        }
    }
}
