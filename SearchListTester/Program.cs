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
                new Relation(){ SourceNode = new Node("A"), TargetNode = new Node("B"), Cost = 4 },
                new Relation(){ SourceNode = new Node("A"), TargetNode = new Node("C"), Cost = 3 },
                new Relation(){ SourceNode = new Node("A"), TargetNode = new Node("D"), Cost = 7 },
                new Relation(){ SourceNode = new Node("B"), TargetNode = new Node("C"), Cost = 2 },
                new Relation(){ SourceNode = new Node("B"), TargetNode = new Node("E"), Cost = 4 },
                new Relation(){ SourceNode = new Node("B"), TargetNode = new Node("F"), Cost = 3 },
                new Relation(){ SourceNode = new Node("C"), TargetNode = new Node("D"), Cost = 1 },
                new Relation(){ SourceNode = new Node("C"), TargetNode = new Node("E"), Cost = 2 },
                new Relation(){ SourceNode = new Node("D"), TargetNode = new Node("E"), Cost = 3 },
                new Relation(){ SourceNode = new Node("D"), TargetNode = new Node("I"), Cost = 5 },
                new Relation(){ SourceNode = new Node("E"), TargetNode = new Node("F"), Cost = 4 },
                new Relation(){ SourceNode = new Node("E"), TargetNode = new Node("I"), Cost = 8 },
                new Relation(){ SourceNode = new Node("E"), TargetNode = new Node("G"), Cost = 4 },
                new Relation(){ SourceNode = new Node("E"), TargetNode = new Node("H"), Cost = 5 },
                new Relation(){ SourceNode = new Node("F"), TargetNode = new Node("G"), Cost = 2 },
                new Relation(){ SourceNode = new Node("G"), TargetNode = new Node("H"), Cost = 1 },
                new Relation(){ SourceNode = new Node("H"), TargetNode = new Node("I"), Cost = 3 },
                
                //two ways
                new Relation(){ SourceNode = new Node("B"), TargetNode = new Node("A"), Cost = 4 },
                new Relation(){ SourceNode = new Node("C"), TargetNode = new Node("A"), Cost = 3 },
                new Relation(){ SourceNode = new Node("D"), TargetNode = new Node("A"), Cost = 7 },
                new Relation(){ SourceNode = new Node("C"), TargetNode = new Node("B"), Cost = 2 },
                new Relation(){ SourceNode = new Node("E"), TargetNode = new Node("B"), Cost = 4 },
                new Relation(){ SourceNode = new Node("D"), TargetNode = new Node("C"), Cost = 1 },
                new Relation(){ SourceNode = new Node("E"), TargetNode = new Node("C"), Cost = 2 },
                new Relation(){ SourceNode = new Node("E"), TargetNode = new Node("D"), Cost = 3 },
                new Relation(){ SourceNode = new Node("F"), TargetNode = new Node("B"), Cost = 3 },
                new Relation(){ SourceNode = new Node("F"), TargetNode = new Node("E"), Cost = 4 },
                new Relation(){ SourceNode = new Node("I"), TargetNode = new Node("D"), Cost = 5 },
                new Relation(){ SourceNode = new Node("I"), TargetNode = new Node("E"), Cost = 8 },
                new Relation(){ SourceNode = new Node("I"), TargetNode = new Node("H"), Cost = 3 },
                new Relation(){ SourceNode = new Node("G"), TargetNode = new Node("E"), Cost = 4 },
                new Relation(){ SourceNode = new Node("G"), TargetNode = new Node("F"), Cost = 2 },
                new Relation(){ SourceNode = new Node("H"), TargetNode = new Node("G"), Cost = 1 },
                new Relation(){ SourceNode = new Node("H"), TargetNode = new Node("E"), Cost = 5 }
            };

            Node start = new Node("F");
            Node end = new Node("D");

            var search = new Search(start, end, relations);
            foreach (Relation r in search.Run())
                Console.WriteLine(r.ToString());
        }
    }
}
