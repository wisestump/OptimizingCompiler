﻿using System;
using GPPGParser;
using SyntaxTree.SyntaxNodes;
using DataFlowAnalysis.IntermediateRepresentation.ThreeAddressCode;
using DataFlowAnalysis.IntermediateRepresentation.ControlFlowGraph;
using DataFlowAnalysis.IntermediateRepresentation.BasicBlockCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.IntermediateRepresentationTests
{
    [TestClass]
    public class DFNTest
    {
        [TestMethod]
        public void TestDFN()
        {
            string programText = @"
                                a = 4;
                                b = 4;
                                c = a + b;
                                if 1 
                                    a = 3;
                                else
                                    b = 2;
                                print(c);
                                ";

            SyntaxNode root = ParserWrap.Parse(programText);
            var threeAddressCode = ThreeAddressCodeGenerator.CreateAndVisit(root).Program;
            Graph g = new Graph(BasicBlocksGenerator.CreateBasicBlocks(threeAddressCode));
            var DFN = g.GetDFN();

            Assert.IsTrue(DFN[0] == 1);
            Assert.IsTrue(DFN[1] == 3);
            Assert.IsTrue(DFN[2] == 2);
            Assert.IsTrue(DFN[3] == 4);
        }
    }
}
