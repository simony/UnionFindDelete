namespace UnionFindDelete
{
    public interface IUnionFind<TNode, T>
        where TNode : INode<T>
    {
        TNode Make(T value);
        TNode Union(TNode root1, TNode root2);
        TNode Find(TNode node);
    }
}
