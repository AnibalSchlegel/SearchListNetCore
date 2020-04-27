
namespace SearchList
{
    public class Relation
    {
        public Node SourceNode;
        public Node TargetNode;
        public int Cost;

        public Relation() { }

        internal Relation Clone() => new Relation() { SourceNode = this.SourceNode.Clone(), TargetNode = this.TargetNode.Clone(), Cost = this.Cost };

        internal bool InverseEquals(Relation other)
        {
            if (this.SourceNode.Name == other.TargetNode.Name && this.TargetNode.Name == other.SourceNode.Name)
                return true;
            else return false;
        }

        internal Relation GetInverse() => new Relation() { SourceNode = this.TargetNode.Clone(), TargetNode = this.SourceNode.Clone(), Cost = this.Cost };

        public override string ToString() => string.Format("{0}->{1} ({2})", this.SourceNode.Name, this.TargetNode.Name, this.Cost);
    }
}
