﻿using DataFlowAnalysis.BasicBlockCode.Model;
using DataFlowAnalysis.ControlFlowGraph;
using DataFlowAnalysis.IterativeAlgorithmParameters;
using DataFlowAnalysis.IterativeAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFlowAnalysis.IterativeAlgorithm
{
    public static class IterativeAlgorithm
    {
        public static IterativeAlgorithmOutput<V> Apply<T, V>(Graph graph, BasicIterativeAlgorithmParameters<V> param) where T : BasicIterativeAlgorithmParameters<V>
        {
            IterativeAlgorithmOutput<V> result = new IterativeAlgorithmOutput<V>();
            
            foreach (BasicBlock bb in graph)
                result.Out[bb.BlockId] = param.StartingValue;

            bool changed = true;
            while (changed)
            {
                changed = false;
                foreach (BasicBlock bb in graph)
                {
                    BasicBlocksList parents = param.ForwardDirection ? graph.getParents(bb.BlockId) : graph.getChildren(bb.BlockId);
                    if (parents.Blocks.Count > 0)
                        result.In[bb.BlockId] = param.GatherOperation(parents.Blocks.Select(b => result.Out[b.BlockId]));
                    else
                        result.In[bb.BlockId] = param.FirstValue;
                    V newOut = param.TransferFunction(result.In[bb.BlockId], bb);
                    changed = changed || !param.Compare(result.Out[bb.BlockId], newOut);
                    result.Out[bb.BlockId] = param.TransferFunction(result.In[bb.BlockId], bb);
                }
            }
            if (!param.ForwardDirection)
                result = new IterativeAlgorithmOutput<V> { In = result.Out, Out = result.In };
            return result;
        }
    }
}
