using System.Collections;
using System.Collections.Generic;
using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace NeatImplementation {
    internal static class Activations
    {
        //activation functions and their corrosponding derivatives
        public static float sigmoid(float x)
        {
            float k = (float)Math.Exp(x);
            return k / (1.0f + k);
        }
        public static float tanh(float x)
        {
            return (float)Math.Tanh(x);
        }
        public static float relu(float x)
        {
            return (0 >= x) ? 0 : x;
        }
        public static float ownRelu(float x)
        {
            if (float.IsNaN(x)) return 0;
            return (x < 0) ? (0) : ((x > 1) ? (1) : (x));
        }
        public static float leakyrelu(float x)
        {
            return (0 >= x) ? 0.01f * x : x;
        }
        public static float sigmoidDer(float x)
        {
            return x * (1 - x);
        }
        public static float tanhDer(float x)
        {
            return 1 - (x * x);
        }
        public static float reluDer(float x)
        {
            return (0 >= x) ? 0 : 1;
        }
        public static float ownReluDer(float x)
        {
            if (float.IsNaN(x)) return 0;
            return (x < 0) ? (0) : ((x > 1) ? (1) : (x));
        }
        public static float leakyreluDer(float x)
        {
            return (0 >= x) ? 0.01f : 1;
        }
    }
}