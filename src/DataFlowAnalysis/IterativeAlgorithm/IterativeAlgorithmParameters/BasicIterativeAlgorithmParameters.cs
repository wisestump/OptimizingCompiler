﻿using DataFlowAnalysis.BasicBlockCode.Model;
using DataFlowAnalysis.IterativeAlgorithmParameters.Model;
using System.Collections.Generic;

namespace DataFlowAnalysis.IterativeAlgorithmParameters
{
    public abstract class BasicIterativeAlgorithmParameters<T>
    {
        public abstract bool ForwardDirection { get; }

        public abstract T StartingValue { get; }

        public abstract T FirstValue { get { return StartingValue; } }

        public abstract T GatherOperation(IEnumerable<T> blocks);

        public abstract T TransferFunction(T input, BasicBlock block);

        public abstract bool Compare(T t1, T t2);
    }
}
