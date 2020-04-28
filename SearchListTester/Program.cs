using SearchList;
using System;
using System.Collections.Generic;

namespace SearchListTester
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Relation> relations = new List<Relation>()
            {
                new Relation(){ SourceNode = new Node("A"), TargetNode = new Node("B"), Cost = 2 },
                new Relation(){ SourceNode = new Node("A"), TargetNode = new Node("C"), Cost = 3 },
                new Relation(){ SourceNode = new Node("A"), TargetNode = new Node("G"), Cost = 10 },
                new Relation(){ SourceNode = new Node("B"), TargetNode = new Node("D"), Cost = 2 },
                new Relation(){ SourceNode = new Node("B"), TargetNode = new Node("E"), Cost = 3 },
                new Relation(){ SourceNode = new Node("C"), TargetNode = new Node("E"), Cost = 2 },
                new Relation(){ SourceNode = new Node("C"), TargetNode = new Node("F"), Cost = 3 },
                new Relation(){ SourceNode = new Node("C"), TargetNode = new Node("G"), Cost = 5 },
                new Relation(){ SourceNode = new Node("D"), TargetNode = new Node("E"), Cost = 1 },
                new Relation(){ SourceNode = new Node("D"), TargetNode = new Node("H"), Cost = 4 },
                new Relation(){ SourceNode = new Node("E"), TargetNode = new Node("H"), Cost = 3 },
                new Relation(){ SourceNode = new Node("E"), TargetNode = new Node("I"), Cost = 4 },
                new Relation(){ SourceNode = new Node("F"), TargetNode = new Node("G"), Cost = 1 },
                new Relation(){ SourceNode = new Node("F"), TargetNode = new Node("I"), Cost = 5 },
                new Relation(){ SourceNode = new Node("F"), TargetNode = new Node("J"), Cost = 3 },
                new Relation(){ SourceNode = new Node("G"), TargetNode = new Node("J"), Cost = 2 },
                new Relation(){ SourceNode = new Node("G"), TargetNode = new Node("N"), Cost = 3 },
                new Relation(){ SourceNode = new Node("H"), TargetNode = new Node("K"), Cost = 2 },
                new Relation(){ SourceNode = new Node("H"), TargetNode = new Node("L"), Cost = 3 },
                new Relation(){ SourceNode = new Node("I"), TargetNode = new Node("L"), Cost = 2 },
                new Relation(){ SourceNode = new Node("I"), TargetNode = new Node("M"), Cost = 3 },
                new Relation(){ SourceNode = new Node("J"), TargetNode = new Node("M"), Cost = 1 },
                new Relation(){ SourceNode = new Node("J"), TargetNode = new Node("N"), Cost = 1 },
                new Relation(){ SourceNode = new Node("N"), TargetNode = new Node("M"), Cost = 2 },
                new Relation(){ SourceNode = new Node("K"), TargetNode = new Node("L"), Cost = 4 },
                new Relation(){ SourceNode = new Node("K"), TargetNode = new Node("O"), Cost = 5 },
                new Relation(){ SourceNode = new Node("L"), TargetNode = new Node("O"), Cost = 1 },
                new Relation(){ SourceNode = new Node("M"), TargetNode = new Node("O"), Cost = 2 }
            };

            Node start = new Node("B");
            Node end = new Node("O");

            var search = new Search(start, end, relations);

            var total = 0;

            foreach (Relation r in search.Run())
            {
                Console.WriteLine(r.ToString());
                total += r.Cost;
            }

            Console.WriteLine($"Total: {total}");
        }
    }
}
