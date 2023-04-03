using NeuralNetworks_LIB;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Data;
using UnityEngine.XR;
using System.Runtime.CompilerServices;

namespace NeatImplementation {
    public class Species {
        public float score;
        public List<Genome> genomes;

        // !Constructors
        public Species(List<Genome> genomes) {
            this.genomes = genomes;
        }
        public Species() {
            genomes = new List<Genome>();
        }
        public Species(Genome genome) {
            genomes = new List<Genome> { genome };
        }


        // !Functions
        public Genome GetRepresentative() {
            return genomes[0];
        }
        public void Sort() {
            // Sort the species according to their fittness descending
        }
        public void Kill(int percentage) {
            // Kills a <percentage> amount of genomes in the species, from the bottom up
        }
    }
}