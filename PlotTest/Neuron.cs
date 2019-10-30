using System;
using Plotting;

namespace PlotTest
{
    public class Neuron
    {
        float a;
        float b;
        float c;

        float learning_rate = 1f;

        public Neuron()
        {
            a = GlobalRandom.NextFloat();
            b = GlobalRandom.NextFloat();
            c = GlobalRandom.NextFloat();
        }

        public PlotPoint GetPoint(float x)
        {
            float y = (-c - (a * x)) / b;
            return new PlotPoint(x, y);
        }

        public float Test(float x, float y)
        {
            return Activate(a * x + b * y + c);
        }

        public void Train(float x, float y, float expected)
        {
            float output = Activate(a * x + b * y + c);
            float error = expected - output;
            a += learning_rate * x * error;
            b += learning_rate * y * error;
            c += learning_rate * error;
        }

        private float Activate(float input)
        {
            float result = 0f;
            if (input > 0f)
            {
                result = 1f;
            }
            return result;
        }
    }
}
