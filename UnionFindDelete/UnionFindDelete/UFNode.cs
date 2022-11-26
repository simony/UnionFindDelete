namespace UnionFindDelete
{
    public abstract class UFNode<TNode, T> : TreeNode<TNode, T>
        where TNode : UFNode<TNode, T>
    {
        public int Rank { get; set; }
    }

    public sealed class UFNode<T> : UFNode<UFNode<T>, T>
    {
        #region Constructors

        public UFNode()
        {
            this.Parent = this;
        }

        public UFNode(T value)
            : this()
        {
            this.Value = value;
        }

        #endregion
    }
}
