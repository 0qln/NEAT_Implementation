using System.Collections;
using System.Collections.Generic;
using System;
using System.Data;
using System.Runtime.CompilerServices;
using Utilities;

namespace NeatImplementation {
    /// <summary>
    /// Neural Evolution of Augmenting Topologies
    /// </summary>
    public class NEAT : Activations {
        // !Attributes
        public List<Genome> genomes;
        public float c1 = 1, c2 = 1, c3 = 1;
        public const float DISTANCE_THRESHHOLD = 100;
        public const int KILL_PERCENTAGE = 50;

        // a global array of the Nodes and connections. Each genomes just gets a reference of the objects
        public Dictionary<(int, int), Connection> allConnections;
        public HashList<Node> allNodes;

        // !Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="outputs"></param>
        public NEAT(int inputs, int outputs, int genomesCount) {
            genomes = new List<Genome>();
            for (int i = 0; i < genomesCount; i ++) {
                genomes.Add(new Genome(inputs, outputs, this));
            }
            allNodes = new HashList<Node>();
            allConnections = new Dictionary<(int, int), Connection>();
        }

        // !Mutations
        // ?Testing Required
        /// <summary>
        /// Asigns a new random value to a <paramref name="percentage"/> amount of the weights.
        /// </summary>
        /// <param name="percentage">The amount of weights that will get changed.</param>
        /// <param name="max">The maximum amount, that the new random value can have.</param>
        public void Mutate_WeightRandom(int percentage, int max, int genomeIndex) {
            List<Connection> shuffleGenes = new List<Connection>(genomes[genomeIndex].connectionGenes);
            shuffleGenes.Shuffle();

            for (int i = 0; i < shuffleGenes.Count * (percentage / 100); i++) {
                shuffleGenes[i].weight = (float)((new Random().NextDouble() * 2 * max) - max);
            }
        }
        /// <summary>
        /// Shifts a <paramref name="percentage"/> amount of weights.
        /// </summary>
        /// <param name="percentage">The percentage of random weights that will be chosen. Between 0 and 100.</param>
        /// <param name="max">The maximum amount, that the weight will get shifted.</param>
        public void Mutate_WeightShift(int percentage, int max, int genomeIndex) {
            List<Connection> shuffleGenes = new List<Connection>(genomes[genomeIndex].connectionGenes);
            shuffleGenes.Shuffle();

            for (int i = 0; i < shuffleGenes.Count * (percentage / 100); i++) {
                shuffleGenes[i].weight += (float)((new Random().NextDouble() * 2 * max) - max);
            }
        }
        /// <summary>
        /// Picks a <percentage> amount of the weights. Foreach wight: If the weight is enabled, disable it; if it is disabled, enable it.
        /// percentage is between 0 and 100.
        /// </summary>
        public void Mutate_EnableDisable(int percentage, int genomeIndex) {
            List<Connection> shuffleGenes = new List<Connection>(genomes[genomeIndex].connectionGenes);
            shuffleGenes.Shuffle();

            for (int i = 0; i < shuffleGenes.Count * (percentage / 100); i++) {
                if (shuffleGenes[i].enabled)
                    shuffleGenes[i].enabled = false;
                else
                    shuffleGenes[i].enabled = true;
            }
        }
        /// <summary>
        /// Adds a new Node, which will sit on a already existing Connection
        /// </summary>
        public void Mutate_Node(int genomeIndex) {
            // Weight of the first connection is set to 1
            // Weight of the second connection is set to the value of the original connection
            // Create two new connections and disable the old one

            // Pick a random weight that the new Node will be placed on
            // vars
            int oldGeneIndex = new Random().Next(genomes[genomeIndex].connectionGenes.Count);
            Node oldIn_ = genomes[genomeIndex].connectionGenes[oldGeneIndex].in_;
            Node oldOut_ = genomes[genomeIndex].connectionGenes[oldGeneIndex].out_;

            // Create the new Node
            Node newNode = new Node(false);
            genomes[genomeIndex].addNode(newNode);

            // Deactivate the old Connection
            genomes[genomeIndex].connectionGenes[oldGeneIndex].enabled = false;

            // Create two new Connections and add them to the connections list
            Connection newConnection_1 = new Connection(oldIn_, oldOut_, 1);
            Connection newConnection_2 = new Connection(newNode, oldOut_, genomes[genomeIndex].connectionGenes[oldGeneIndex].weight);
            genomes[genomeIndex].addConnection(newConnection_1);
            genomes[genomeIndex].addConnection(newConnection_2);

            newNode.inputConnections.Add(newConnection_1);
            oldOut_.inputConnections.Add(newConnection_2);
        }
        /// <summary>
        /// Adds a new connection between two nodes.
        /// </summary>
        public void Mutate_Connection(int genomeIndex) {
            // New Connection might get an innovation number that already exists, important for doing crossovers

            // Pick Random Input, Output Nodes for the new connection and a random weight between -2 and 2
            Random random = new Random();
            int inputIndex = (int)random.Next(genomes[genomeIndex].hiddenNodes + genomes[genomeIndex].inputNodes);
            int outputIndex = (int)random.Next(genomes[genomeIndex].hiddenNodes + genomes[genomeIndex].outputNodes);
            Node inputNode = genomes[genomeIndex].nodes[inputIndex];
            Node outputNode = genomes[genomeIndex].nodes[outputIndex];
            float weight = (float)random.NextDouble() * 4 - 2;

            // Create the new Connection and implement it into the Genome of <genomeIndex>
            int innovationNumber = 0;
            if (allConnections.ContainsKey((inputIndex, outputIndex))) {
                innovationNumber = allConnections[(inputIndex, outputIndex)].innovationNumber;
            }
            allConnections.Add((inputIndex, outputIndex), new Connection(inputNode, outputNode, weight, innovationNumber));
            outputNode.inputConnections.Add(allConnections[(inputIndex, outputIndex)]);
            genomes[genomeIndex].connectionGenes.Add(allConnections[(inputIndex, outputIndex)]);
        }

        List<Species> GenerateSpecies() {
            List<Species> species = new List<Species>() { new Species(genomes[0]) };

            for (int i = 1; i < genomes.Count; i++) {
                for (int j = 0; j < species.Count; j++) {
                    if (Distance(species[j].GetRepresentative(), genomes[i]) < DISTANCE_THRESHHOLD) {
                        species[j].genomes.Add(genomes[i]);
                    }
                }
            }

            return species;
        }

        //! Functions 
        public void NextGeneration() {
            // Mutate

            // Crossover

            // Let the Genomes play against each other in a game of Reversi
            // Calculate Fittness = The amount of discs the Genome has at the end 

            // Create Species

            // Kill 


        }

        /// <summary>
        /// Calculates the difference between two genomes.
        /// </summary>
        /// <param name="genome1"></param>
        /// <param name="genome2"></param>
        /// <returns></returns>
        public float Distance(Genome genome1, Genome genome2) {

            int highestInnovationNum_g1 = 0;
            int highestInnovationNum_g2 = 0;
            if (genome1.connectionGenes.Count != 0) highestInnovationNum_g1 = genome1.connectionGenes[genome1.connectionGenes.Count- 1].innovationNumber;
            if (genome2.connectionGenes.Count != 0) highestInnovationNum_g2 = genome2.connectionGenes[genome2.connectionGenes.Count -1].innovationNumber;

            if (highestInnovationNum_g1 < highestInnovationNum_g2) {
                Genome temp = genome1;
                genome1 = genome2;
                genome2 = temp;
            }

            int index_g1 = 0;
            int index_g2 = 0;

            int disjointCount = 0;
            int excessCount = 0;
            float weight_diff = 0;
            int similar = 0;

            while (index_g1 < genome1.connectionGenes.Count && index_g2 < genome2.connectionGenes.Count) {
                Connection connection1 = genome1.connectionGenes[index_g1];
                Connection connection2 = genome2.connectionGenes[index_g2];
                int innovationNum1 = connection1.innovationNumber;
                int innovationNum2 = connection2.innovationNumber;

                if (innovationNum1 == innovationNum2) {
                    // Similar gene
                    index_g1++;
                    index_g2++;
                    similar++;
                    weight_diff += Math.Abs(connection1.weight - connection2.weight);
                }
                else if (innovationNum1 > innovationNum2) {
                    // Disjoint gene of B
                    index_g2++;
                    disjointCount++;
                }
                else {
                    // Disjoint gene of A
                    index_g1++;
                    disjointCount++;
                }
            }

            weight_diff /= similar;
            excessCount = genome1.connectionGenes.Count - index_g1;

            float N = Math.Max(genome1.connectionGenes.Count, genome2.connectionGenes.Count);
            if (N < 20) {
                N = 1;
            }

            return (c1 * disjointCount / N) + (c2 * excessCount / N) + c3 * weight_diff;
        }

        /// <summary>
        /// Returns an offspring of the two input Genomes.
        /// </summary>
        /// <param name="genome1"></param>
        /// <param name="genome2"></param>
        /// <returns></returns>
        public Genome Crossover(Genome genome1, Genome genome2) {
            Genome offspring = new Genome(genome1.inputNodes, genome2.outputNodes, this).Empty();

            int index_g1 = 0;
            int index_g2 = 0;

            while (index_g1 < genome1.connectionGenes.Count && index_g2 < genome2.connectionGenes.Count) {
                Connection connection1 = genome1.connectionGenes[index_g1];
                Connection connection2 = genome2.connectionGenes[index_g2];
                int innovationNum1 = connection1.innovationNumber;
                int innovationNum2 = connection2.innovationNumber;

                if (innovationNum1 == innovationNum2) {
                    // Similar gene

                    if (new Random().NextDouble() > 0.5) {
                        offspring.connectionGenes.Add(connection1);
                    }
                    else {
                        offspring.connectionGenes.Add(connection2);
                    }

                    index_g1++;
                    index_g2++;
                }
                else if (innovationNum1 > innovationNum2) {
                    // Disjoint gene of B
                    offspring.connectionGenes.Add(connection2);
                    index_g2++;
                }
                else {
                    // Disjoint gene of A
                    offspring.connectionGenes.Add(connection1);
                    index_g1++;
                }
            }

            while (index_g1 < genome1.connectionGenes.Count) {
                offspring.connectionGenes.Add(genome1.connectionGenes[index_g1]);
            }

            foreach (Connection connection in genome1.connectionGenes) {
                offspring.nodes.Add(connection.in_);
                offspring.nodes.Add(connection.out_);
            }
            foreach (Connection connection in genome2.connectionGenes) {
                if (!offspring.nodes.Contains(connection.in_))
                    offspring.nodes.Add(connection.in_);
                if (!offspring.nodes.Contains(connection.out_))
                    offspring.nodes.Add(connection.out_);
            }

            return offspring;
        }


        // !Calculations
        /// <summary>
        /// Feeds the inputs through the NN.  inputs >==> outputs
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public float[] FeedForward(float[] inputs, int genomeIndex) {
            // Reset Calculations
            genomes[genomeIndex].resetCalculations();
            // Set inputs
            genomes[genomeIndex].setInputs(inputs);

            // Calculate Outputs
            float[] outputs = new float[genomes[genomeIndex].outputNodes];
            for (int i = 0; i < genomes[genomeIndex].outputNodes; i++) {
                outputs[i] = genomes[genomeIndex].nodes[genomes[genomeIndex].inputNodes + i].getOutput();
            }

            return outputs;
        }


        // !Saving and Loading the Genomes (Files)
        // TODO
        /// <summary>
        /// Loads the genome in the text file to this NEAT.
        /// </summary>
        /// <param name="path"></param>
        public void LoadGenome(string path) {

        }
        // TODO
        /// <summary>
        /// Saves the Genome corresponding to this NEAT in a text file.
        /// </summary>
        /// <param name="path"></param>
        public void SaveGenome(string path) {

        }
    }
}