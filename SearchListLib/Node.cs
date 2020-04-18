namespace SearchList
{
    public class Node
    {
        internal int Weight = 0;
        internal int TotalWeight = 0;
        internal string Name;
        internal NodeType Type = NodeType.MiddleNode;

        public Node(string name) => this.Name = name;

        internal Node() { }

        public void SetAsStartNode() => this.Type = NodeType.StartNode;
        public void SetAsEndNode() => this.Type = NodeType.EndNode;

        internal Node Clone() => new Node() { Name = this.Name, Type = this.Type, TotalWeight = this.TotalWeight, Weight = this.Weight };
    
    }
    internal enum NodeType
    {
        StartNode,
        MiddleNode,
        EndNode
    }
}
