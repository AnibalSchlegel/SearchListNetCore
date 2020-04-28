
using System.Runtime.CompilerServices;

namespace SearchList
{
    public class Relation
    {
        public Node SourceNode;
        public Node TargetNode;
        public int Cost;

        public Relation() { }

        internal Relation Clone() => new Relation() { SourceNode = this.SourceNode.Clone(), TargetNode = this.TargetNode.Clone(), Cost = this.Cost };

        internal bool IsEquals(Relation other) => this.SourceNode.Name == other.SourceNode.Name && this.TargetNode.Name == other.TargetNode.Name;
        

        internal bool InverseEquals(Relation other) => this.SourceNode.Name == other.TargetNode.Name && this.TargetNode.Name == other.SourceNode.Name;

        internal Relation GetInverse() => new Relation() { SourceNode = this.TargetNode.Clone(), TargetNode = this.SourceNode.Clone(), Cost = this.Cost };

        public override string ToString() => string.Format("{0}->{1} ({2})", this.SourceNode.Name, this.TargetNode.Name, this.Cost);
    }
}
