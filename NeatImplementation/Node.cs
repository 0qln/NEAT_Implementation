using System.Collections;
using System.Collections.Generic;
using System;
using System.Data;
using System.Runtime.CompilerServices;
using Utilities;

namespace NeatImplementation {


    /// <summary>
    /// The nodex that connect the genes
    /// </summary>
    public class Node : Activations {
        // !Attributes
        public List<Connection> inputConnections;
        public float value;
        public bool isCalculated;

        // !Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">0 for input, 1 for output or hidden</param>
        public Node(bool isInputNode) {
            inputConnections = new List<Connection>();

            if (isInputNode) {
                isCalculated = true;
            }
            else {
                isCalculated = false;
            }
        }


        // !Functions
        /// <summary>
        /// Calculates its value.
        /// </summary>
        public float getOutput() {
            if (isCalculated) {
                return value;
            }

            float sum = 0;
            foreach (Connection connection in inputConnections) {
                if (connection.enabled) {
                    sum += connection.weight * connection.in_.getOutput();
                }
            }

            sum = sigmoid(sum);
            value = sum;
            isCalculated = true;
            return sum;
        }

    }

}