﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataFlowAnalysis.IntermediateRepresentation.BasicBlockCode;
using DataFlowAnalysis.IntermediateRepresentation.ControlFlowGraph;
using DataFlowAnalysis.IntermediateRepresentation.EdgeClassification;
using DataFlowAnalysis.IntermediateRepresentation.EdgeClassification.Model;
using DataFlowAnalysis.IntermediateRepresentation.ThreeAddressCode;
using GPPGParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyntaxTree.SyntaxNodes;

namespace UnitTests.IntermediateRepresentationTests
{
    [TestClass]
    public class EdgesClassificationTest
    {
        [TestMethod]
        public void EdgeClassificationTest()
        {
            string programText_1 = @"
b = 1;
if 1 
  b = 3;
else
  b = 2;
";

            string programText_2 = @"
b = 1;
if 1
  b = 3;
else
  for i = 1..5
    b = 6;
";

            string programText_3 = @"
i = 10;
1: i = i - 1;
if i > 0
  goto 1;
";

            Trace.WriteLine("===============");
            Trace.WriteLine("Тест 1");
            Trace.WriteLine("===============");
            SyntaxNode root = ParserWrap.Parse(programText_1);
            var threeAddressCode = ThreeAddressCodeGenerator.CreateAndVisit(root).Program;
            Trace.WriteLine(threeAddressCode);

            var basicBlocks = BasicBlocksGenerator.CreateBasicBlocks(threeAddressCode);
            Trace.WriteLine(Environment.NewLine + "Базовые блоки");
            Trace.WriteLine(basicBlocks);

            Trace.WriteLine(Environment.NewLine + "Управляющий граф программы");
            Graph g = new Graph(basicBlocks);
            Trace.WriteLine(g);

            var edgeClassify = EdgeClassification.ClassifyEdge(g);
            List<Tuple<int, int, EdgeType>> tuples = new List<Tuple<int, int, EdgeType>>();
            foreach (var v in edgeClassify)
            {
                Trace.WriteLine(v.Key.Source.BlockId + " -> " + v.Key.Target.BlockId + " : " + v.Value);
                tuples.Add(new Tuple<int, int, EdgeType>(v.Key.Source.BlockId, v.Key.Target.BlockId, v.Value));
            }

            int start = g.GetMinBlockId();

            Assert.IsTrue(tuples.SequenceEqual(new List<Tuple<int, int, EdgeType>>()
            { new Tuple<int, int, EdgeType>(start, start + 1, EdgeType.Advancing),
              new Tuple<int, int, EdgeType>(start, start + 2, EdgeType.Advancing),
              new Tuple<int, int, EdgeType>(start + 1, start + 3, EdgeType.Cross),
              new Tuple<int, int, EdgeType>(start + 2, start + 3, EdgeType.Cross)}
            ));

            Trace.WriteLine("===============");
            Trace.WriteLine("Тест 2");
            Trace.WriteLine("===============");

            root = ParserWrap.Parse(programText_2);
            threeAddressCode = ThreeAddressCodeGenerator.CreateAndVisit(root).Program;
            Trace.WriteLine(threeAddressCode);

            basicBlocks = BasicBlocksGenerator.CreateBasicBlocks(threeAddressCode);
            Trace.WriteLine(Environment.NewLine + "Базовые блоки");
            Trace.WriteLine(basicBlocks);

            Trace.WriteLine(Environment.NewLine + "Управляющий граф программы");
            g = new Graph(basicBlocks);
            Trace.WriteLine(g);

            edgeClassify = EdgeClassification.ClassifyEdge(g);
            tuples = new List<Tuple<int, int, EdgeType>>();
            foreach (var v in edgeClassify)
            {
                Trace.WriteLine(v.Key.Source.BlockId + " -> " + v.Key.Target.BlockId + " : " + v.Value);
                tuples.Add(new Tuple<int, int, EdgeType>(v.Key.Source.BlockId, v.Key.Target.BlockId, v.Value));
            }

            start = g.GetMinBlockId();

            Assert.IsTrue(tuples.SequenceEqual(new List<Tuple<int, int, EdgeType>>()
            { new Tuple<int, int, EdgeType>(start, start + 1, EdgeType.Advancing),
              new Tuple<int, int, EdgeType>(start, start + 2, EdgeType.Advancing),
              new Tuple<int, int, EdgeType>(start + 1, start + 6, EdgeType.Cross),
              new Tuple<int, int, EdgeType>(start + 2, start + 3, EdgeType.Advancing),
              new Tuple<int, int, EdgeType>(start + 3, start + 4, EdgeType.Advancing),
              new Tuple<int, int, EdgeType>(start + 3, start + 5, EdgeType.Advancing),
              new Tuple<int, int, EdgeType>(start + 4, start + 3, EdgeType.Retreating),
              new Tuple<int, int, EdgeType>(start + 5, start + 6, EdgeType.Cross)
            }
            ));

            Trace.WriteLine("===============");
            Trace.WriteLine("Тест 3");
            Trace.WriteLine("===============");

            root = ParserWrap.Parse(programText_3);
            threeAddressCode = ThreeAddressCodeGenerator.CreateAndVisit(root).Program;
            Trace.WriteLine(threeAddressCode);

            basicBlocks = BasicBlocksGenerator.CreateBasicBlocks(threeAddressCode);
            Trace.WriteLine(Environment.NewLine + "Базовые блоки");
            Trace.WriteLine(basicBlocks);

            Trace.WriteLine(Environment.NewLine + "Управляющий граф программы");
            g = new Graph(basicBlocks);
            Trace.WriteLine(g);

            edgeClassify = EdgeClassification.ClassifyEdge(g);
            tuples = new List<Tuple<int, int, EdgeType>>();
            foreach (var v in edgeClassify)
            {
                Trace.WriteLine(v.Key.Source.BlockId + " -> " + v.Key.Target.BlockId + " : " + v.Value);
                tuples.Add(new Tuple<int, int, EdgeType>(v.Key.Source.BlockId, v.Key.Target.BlockId, v.Value));
            }

            start = g.GetMinBlockId();

            Assert.IsTrue(tuples.SequenceEqual(new List<Tuple<int, int, EdgeType>>()
            { new Tuple<int, int, EdgeType>(start, start + 1, EdgeType.Advancing),
              new Tuple<int, int, EdgeType>(start + 1, start + 2, EdgeType.Advancing),
              new Tuple<int, int, EdgeType>(start + 1, start + 3, EdgeType.Advancing),
              new Tuple<int, int, EdgeType>(start + 2, start + 1, EdgeType.Retreating)}
            ));
        }
    }
}
