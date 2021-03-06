﻿using System.Collections.Generic;
using System.Linq;
using DataFlowAnalysis.IntermediateRepresentation.BasicBlockCode.Model;
using DataFlowAnalysis.IntermediateRepresentation.ControlFlowGraph;
using DataFlowAnalysis.Utilities;
using DataFlowAnalysis.IterativeAlgorithm.IterativeAlgorithmParameters;

namespace DataFlowAnalysis.SpecificIterativeAlgorithmParametrs.Dominators
{
    public class DominatorsIterativeAlgorithmParametrs : BasicIterativeAlgorithmParameters<ISet<int>>
    {
        private Graph graph;

        public DominatorsIterativeAlgorithmParametrs(Graph g)
        {
            graph = g;
        }

        public override ISet<int> GatherOperation(IEnumerable<ISet<int>> blocks)
        {
            ISet<int> intersection = SetFactory.GetSet((IEnumerable<int>)blocks.First());
            foreach (var block in blocks.Skip(1))
                intersection.IntersectWith(block);

            return intersection;
        }

        public override ISet<int> TransferFunction(ISet<int> input, BasicBlock block)
        {
            return SetFactory.GetSet<int>(input.Union(new int[] { block.BlockId }));
        }

        public override bool AreEqual(ISet<int> t1, ISet<int> t2)
        {
            return t1.IsSubsetOf(t2) && t2.IsSubsetOf(t1);
        }

        public override ISet<int> StartingValue { get { return SetFactory.GetSet<int>(Enumerable.Range(graph.GetMinBlockId(), graph.Count())); } }

        public override ISet<int> FirstValue { get { return SetFactory.GetSet<int>(Enumerable.Repeat(graph.GetMinBlockId(), 1)); } }

        public override bool ForwardDirection { get { return true; } }

    }
}
