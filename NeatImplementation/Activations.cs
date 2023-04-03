using System.Collections;
using System.Collections.Generic;
using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace NeatImplementation {
    public abstract class Activations
    {
        //activation functions and their corrosponding derivatives
        public float sigmoid(float x)
        {
            float k = (float)Math.Exp(x);
            return k / (1.0f + k);
        }
        public float tanh(float x)
        {
            return (float)Math.Tanh(x);
        }
        public float relu(float x)
        {
            return (0 >= x) ? 0 : x;
        }
        public float ownRelu(float x)
        {
            if (float.IsNaN(x)) return 0;
            return (x < 0) ? (0) : ((x > 1) ? (1) : (x));
        }
        public float leakyrelu(float x)
        {
            return (0 >= x) ? 0.01f * x : x;
        }
        public float sigmoidDer(float x)
        {
            return x * (1 - x);
        }
        public float tanhDer(float x)
        {
            return 1 - (x * x);
        }
        public float reluDer(float x)
        {
            return (0 >= x) ? 0 : 1;
        }
        public float ownReluDer(float x)
        {
            if (float.IsNaN(x)) return 0;
            return (x < 0) ? (0) : ((x > 1) ? (1) : (x));
        }
        public float leakyreluDer(float x)
        {
            return (0 >= x) ? 0.01f : 1;
        }
    }
}