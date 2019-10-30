using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotting
{
    public struct PlotPoint
    {
        public float X;
        public float Y;
        public PlotPoint(float x, float y)
        {
            X = x;
            Y = y;
        }

        public PlotPoint RotateAround(float degrees, PlotPoint center)
        {
            double sin = Math.Sin(degrees * Math.PI / 180f);
            double cos = Math.Cos(degrees * Math.PI / 180f);

            double tx = this.X - center.X;
            double ty = this.Y - center.Y;

            double vX = ((cos * tx) - (sin * ty)) + center.X;
            double vY = ((sin * tx) + (cos * ty)) + center.Y;

            return new PlotPoint((float)vX, (float)vY);
        }
    }
}
