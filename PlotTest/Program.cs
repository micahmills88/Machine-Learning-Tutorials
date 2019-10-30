using System;
using System.Drawing;
using Plotting;

namespace PlotTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph(1000, 1000);
            graph.SetBounds(-10f, 10f, -10f, 10f);

            float deviation = 1f;
            PlotPoint[] g1 = GlobalRandom.BuildRandom(-2, deviation, 1, deviation, 0, 150);
            PlotPoint[] g2 = GlobalRandom.BuildRandom(2, deviation, -1, deviation, 0, 150);


            Neuron neuron = new Neuron();
            for (int i = 0; i < g1.Length; i++)
            {
                graph.DrawPlotBackground();
                graph.Plot(g1, 4, Color.Red, GraphPlotType.Points);
                graph.Plot(g2, 4, Color.Blue, GraphPlotType.Points);

                PlotPoint[] line = new PlotPoint[]
                {
                    neuron.GetPoint(-5),
                    neuron.GetPoint(5)
                };
                graph.Plot(line, 2, Color.Yellow, GraphPlotType.Line);
                graph.PlotCircle(g1[i].X, g1[i].Y, 8, Color.Green);
                graph.PlotCircle(g2[i].X, g2[i].Y, 8, Color.Green);
                graph.ShowGraph();
                Console.WriteLine(i);
                System.Threading.Thread.Sleep(250);

                neuron.Train(g1[i].X, g1[i].Y, 0);
                neuron.Train(g2[i].X, g2[i].Y, 1);
            }

            Console.ReadLine();
        }
    }
}
