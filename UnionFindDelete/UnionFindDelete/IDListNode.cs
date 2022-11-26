namespace UnionFindDelete
{
    public interface IDListNode<TNode> : IListNode<TNode>
    {
        TNode Prev { get; set; }
    }
}
