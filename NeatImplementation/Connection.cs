using NeuralNetworks_LIB;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Data;
using UnityEngine.XR;
using System.Runtime.CompilerServices;

namespace NeatImplementation {
    /// <summary>
    /// The Genes connecting the nodes
    /// </summary>
    public class Connection {
        // !Attributes
        public Node in_, out_;
        public int inIndex, outIndex;
        public float weight;
        public bool enabled;
        public int innovationNumber;

        // !Constructor
        /// <summary>
        /// Constructor, sets the innovationNumber to 1.
        /// </summary>
        /// <param name="in_"></param>
        /// <param name="out_"></param>
        /// <param name="weight"></param>
        public Connection(Node in_, Node out_, float weight) {
            enabled = true;
            innovationNumber = 1;
            this.in_ = in_;
            this.out_ = out_;
            this.weight = weight;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="in_"></param>
        /// <param name="out_"></param>
        /// <param name="weight"></param>
        /// <param name="innovationNumber"></param>
        public Connection(Node in_, Node out_, float weight, int innovationNumber) {
            enabled = true;
            this.innovationNumber = innovationNumber;
            this.weight = weight;
            this.in_ = in_;
            this.out_ = out_;
        }
    }

}