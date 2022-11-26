namespace UnionFindDelete
{
    public interface IUnionFindDelete<TNode, T> : IUnionFind<TNode, T>
        where TNode : INode<T>
    {
        void Delete(TNode node);
    }
}
