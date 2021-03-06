﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataFlowAnalysis.IntermediateRepresentation.BasicBlockCode.Model;
using DataFlowAnalysis.IntermediateRepresentation.ThreeAddressCode.Model;
using QuickGraph;

namespace DataFlowAnalysis.Utilities
{
    public class SetFactory
    {
        public static ISet<T> GetSet<T>(IEnumerable<T> data)
        {
            return new SortedSet<T>(data);
        }

        public static ISet<T> GetSet<T>(params T[] data)
        {
            return new SortedSet<T>(data);
        }

        public static ISet<Expression> GetSet(IEnumerable<Expression> data)
        {
            return new HashSet<Expression>(data);
        }

        public static ISet<Expression> GetSet(params Expression[] data)
        {
            return new HashSet<Expression>(data);
        }

        public static ISet<Edge<BasicBlock>> GetEdgesSet(IEnumerable<Edge<BasicBlock>> data)
        {
            return new HashSet<Edge<BasicBlock>>(data, new EdgeHashComparer());
        }

        public static ISet<Edge<BasicBlock>> GetEdgesSet(params Edge<BasicBlock>[] data)
        {
            return new HashSet<Edge<BasicBlock>>(data, new EdgeHashComparer());
        }
    }
}
