using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Plotting
{
    public enum GraphPlotType
    {
        Line,
        Points,
        Circle
    }

    public class Graph
    {
        Form plotForm;
        Bitmap graphBase;

        float xBegin = -1;
        float xEnd = 1;
        float yBegin = -1;
        float yEnd = 1;

        bool autoScale = false;
        float xScale = 1;
        float yScale = 1;
        float xOffset = 1;
        float yOffset = 1;
        
        public Graph(int width, int height)
        {
            plotForm = new Form();
            plotForm.Text = "Graph";
            plotForm.ClientSize = new Size(width, height);
            plotForm.FormBorderStyle = FormBorderStyle.Fixed3D;
            plotForm.MaximizeBox = false;
            plotForm.MinimizeBox = false;
            plotForm.StartPosition = FormStartPosition.CenterScreen;
            plotForm.Paint += PlotForm_Paint;

            graphBase = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            SetBounds(-1, 1, -1, 1);
        }

        private void PlotForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(graphBase, new PointF(0, 0));
        }

        public void ShowGraph()
        {
            if(!plotForm.Visible)
            {
                plotForm.Show();
            }
            plotForm.Invalidate();
            plotForm.Refresh();
        }

        public void SetBounds(float x1, float x2, float y1, float y2)
        {
            xBegin = x1;
            xEnd = x2;
            yBegin = y1;
            yEnd = y2;

            xScale = (float)graphBase.Width / (xEnd - xBegin);
            yScale = (float)graphBase.Height / (yEnd - yBegin);
            xOffset = xBegin * xScale;
            yOffset = yBegin * yScale;
            DrawPlotBackground();
        }

        public void SetAutoScale(bool autoscale)
        {
            this.autoScale = autoscale;
        }

        public void DrawPlotBackground()
        {
            using (Graphics g = Graphics.FromImage(graphBase))
            {
                g.Clear(Color.LightGray);
                //draw axis lines
                float xaxis = (float)graphBase.Height + yOffset;
                float yaxis = (float)graphBase.Width + xOffset;
                g.DrawLine(Pens.Black, 0, xaxis, (float)graphBase.Width, xaxis);
                g.DrawLine(Pens.Black, yaxis, 0, yaxis, (float)graphBase.Height);
            }
        }

        public void PlotCircle(float x, float y, float size, Color clr)
        {
            using (Graphics g = Graphics.FromImage(graphBase))
            {
                float xpos = (x * xScale) - xOffset;
                float ypos = (float)graphBase.Height - ((y * yScale) - yOffset);
                float half = size / 2;
                g.DrawEllipse(new Pen(clr, 3), xpos - half, ypos - half, size, size);
            }
        }

        public void Plot(PlotPoint[] plotValues, float pointSize, Color clr, GraphPlotType plotType = GraphPlotType.Points)
        {
            using(Graphics g = Graphics.FromImage(graphBase))
            {
                //draw points/lines
                if(plotType == GraphPlotType.Points)
                {
                    for (int i = 0; i < plotValues.Length; i++)
                    {
                        float xpos = (plotValues[i].X * xScale) - xOffset;
                        float ypos = (float)graphBase.Height - ((plotValues[i].Y * yScale) - yOffset);
                        float half = pointSize / 2;
                        g.FillEllipse(new SolidBrush(clr), xpos - half, ypos - half, pointSize, pointSize);
                    }
                }
                else
                {
                    List<PointF> points = new List<PointF>();
                    for (int i = 0; i < plotValues.Length; i++)
                    {
                        float xpos = (plotValues[i].X * xScale) - xOffset;
                        float ypos = (float)graphBase.Height - ((plotValues[i].Y * yScale) - yOffset);
                        points.Add(new PointF(xpos, ypos));
                    }
                    System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                    gp.AddLines(points.ToArray());
                    g.DrawPath(new Pen(clr, pointSize), gp);
                }
            }
        }
    }
}
