using System.Collections;
using System.Collections.Generic;
using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace NeatImplementation {
    /// <summary>
    /// Genome consisting of Nodes and Genes
    /// </summary>
    internal class Genome {
        // !Attributes
        private NEAT neat;
        public List<Node> nodes;
        public List<Connection> connectionGenes;
        public int inputNodes;
        public int outputNodes;
        public int hiddenNodes;
        public float fittness;


        // !Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="outputs"></param>
        public Genome(int inputs, int outputs, NEAT neat) {
            this.neat = neat;
            inputNodes = inputs;
            outputNodes = outputs;
            hiddenNodes = 1;
            nodes = new List<Node>();
            connectionGenes = new List<Connection>();

            // Add the input nodes
            for (int i = 0; i < inputs; i++) {

                neat.allNodes.AddElement(new Node(true));
                nodes.Add(neat.allNodes.GetElement(i));
            }
            // Add the output nodes
            for (int i = 0; i < outputs; i++) {
                neat.allNodes.AddElement(new Node(false));
                nodes.Add(neat.allNodes.GetElement(i));
            }
            // Add the first hidden Node
            neat.allNodes.AddElement(new Node(false));
            nodes.Add(neat.allNodes.GetElement(nodes.Count));
        }

        public Genome Empty() {
            hiddenNodes = 0;
            nodes.Clear();
            connectionGenes.Clear();
            return this;
        }


        // !Functions
        /// <summary>
        /// Returns a new object, with the same attribute values as this one.
        /// </summary>
        /// <returns></returns>
        public Genome Clone() {
            Genome clone = new Genome(inputNodes, outputNodes, neat);
            clone.nodes = new List<Node>(nodes);
            clone.connectionGenes = new List<Connection>(connectionGenes);
            clone.hiddenNodes = hiddenNodes;
            clone.inputNodes = inputNodes;
            clone.outputNodes = outputNodes;
            clone.fittness = fittness;
            return clone;
        }
        /// <summary>
        /// Increments the InnovationNumber of every NodeGene of the Genome.
        /// </summary>
        public void Innovate() {
            foreach (var nodeGene in connectionGenes) {
                nodeGene.innovationNumber++;
            }
        }
        /// <summary>
        /// Sets the values of the input nodes
        /// </summary>
        /// <param name="inputs"></param>
        public void setInputs(float[] inputs) {
            for (int i = 0; i < inputNodes; i++) {
                nodes[i].value = inputs[i];
                nodes[i].isCalculated = true;
            }
        }
        /// <summary>
        /// Sets the isCalculated bool of each Node, which is not an inputNode, to false
        /// </summary>
        public void resetCalculations() {
            for (int i = inputNodes; i < nodes.Count; i++) {
                nodes[i].isCalculated = false;
            }
        }
        /// <summary>
        /// Adds a Connection to the connectionGenes of this Object and to the global Connections collection
        /// </summary>
        /// <param name="connection"></param>
        public void addConnection(Connection connection) {
            neat.allConnections.Add((connection.inIndex, connection.outIndex), connection);
            connectionGenes.Add(connection);
        }
        /// <summary>
        /// Adds a new Node to this local list and the global set
        /// </summary>
        /// <param name="node"></param>
        public void addNode(Node node) {
            neat.allNodes.AddElement(node);
            nodes.Add(node);
        }
    }
}