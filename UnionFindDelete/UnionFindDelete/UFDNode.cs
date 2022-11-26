namespace UnionFindDelete
{
    public abstract class UFDNode<TNode, T> : UFNode<TNode, T>
        where TNode : UFDNode<TNode, T>
    {
        #region Public Properties

        public DListNode<TNode> DFSNode { get; set; }
        // In the root node NonLeafNode is an anchor, and in its immediate children that aren't leafs it is a node.
        // Everywhere else it is unused.
        public DListNode<TNode> NonLeafNode { get; set; }
        // NeighborAnchor resides in the parent node while the NeighborNode resides in the direct children
        public DListNode<TNode> NeighborAnchor { get; set; }
        public DListNode<TNode> NeighborNode { get; set; }

        #endregion
    }

    public sealed class UFDNode<T> : UFDNode<UFDNode<T>, UFDElement<T>>
    {
        #region Constructors

        public UFDNode()
        {
            this.Parent = this;
            this.DFSNode = new DListNode<UFDNode<T>>(this);
            this.NeighborNode = new DListNode<UFDNode<T>>(this);
            this.NeighborAnchor = new DListNode<UFDNode<T>>(this);
            this.NonLeafNode = new DListNode<UFDNode<T>>(this);
        }

        public UFDNode(UFDElement<T> element)
            : this()
        {
            this.Value = element;
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return string.Format("{0}({1}, {2})", base.ToString(), this.GetHashCode(), this.Value.Value);
        }

        #endregion
    }
}
