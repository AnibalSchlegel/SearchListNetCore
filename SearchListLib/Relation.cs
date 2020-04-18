
namespace SearchList
{
    public class Relation
    {
        public Node SourceNode;
        public Node TargetNode;
        public int Cost;

        public Relation(Node source, Node target, int cost)
        {
            this.SourceNode = source;
            this.TargetNode = target;
            this.Cost = cost;
        }

        public Relation() { }

        internal Relation Clone()
        {
            return new Relation() { SourceNode = this.SourceNode.Clone(), TargetNode = this.TargetNode.Clone(), Cost = this.Cost };
        }

        internal bool InverseEquals(Relation other)
        {
            if (this.SourceNode.Name == other.TargetNode.Name && this.TargetNode.Name == other.SourceNode.Name)
                return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            Relation other = (Relation)obj;

            if (this.SourceNode.Name == other.SourceNode.Name && this.TargetNode.Name == other.TargetNode.Name)
                return true;
            else return false;
        }

        public override string ToString()
        {
            return string.Format("{0}->{1} ({2})", this.SourceNode.Name, this.TargetNode.Name, this.Cost);
        }
    }
}
