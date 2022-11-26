namespace UnionFindDelete
{
    public class DListNode<TNode, T> : ListNode<TNode, T>, IDListNode<TNode>
        where TNode : DListNode<TNode, T>
    {
        public TNode Prev { get; set; }
    }

    public class DListNode<T> : DListNode<DListNode<T>, T>
    {
        #region Constructors

        public DListNode()
        {
            DListNodeExtensions.Init(this);
        }

        public DListNode(T value)
            : this()
        {
            this.Value = value;
        }

        #endregion
    }
}
