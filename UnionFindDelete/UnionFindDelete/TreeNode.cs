namespace UnionFindDelete
{
    public abstract class TreeNode<TNode, T> : Node<T>, ITreeNode<TNode>
        where TNode : TreeNode<TNode, T>
    {
        #region Public Properties

        public TNode Parent { get; set; }

        #endregion
    }

    public sealed class TreeNode<T> : TreeNode<TreeNode<T>, T>
    {
        #region Constructors

        public TreeNode()
        {
            this.Parent = this;
        }

        public TreeNode(T value)
            : this()
        {
            this.Value = value;
        }

        #endregion
    }
}
