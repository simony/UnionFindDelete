namespace UnionFindDelete
{
    public class ListNode<TNode, T> : Node<T>, IListNode<TNode>
        where TNode : ListNode<TNode, T>
    {
        public TNode Next { get; set; }
    }

    public sealed class ListNode<T> : ListNode<ListNode<T>, T>
    {
    }
}
