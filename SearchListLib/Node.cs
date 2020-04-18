namespace SearchList
{
    public class Node
    {
        internal int Weight = 0;
        internal int TotalWeight = 0;
        internal string Name;

        public Node(string name) => this.Name = name;

        internal Node() { }

        internal Node Clone() => new Node() { Name = this.Name, TotalWeight = this.TotalWeight, Weight = this.Weight };
    
    }
}
