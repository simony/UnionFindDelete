using System.Collections.Generic;

namespace UnionFindDelete
{
    public static class ListNodeExtensions
    {
        public static IEnumerable<TNode> Enumerate<TNode>(TNode anchor, TNode startNode)
            where TNode : IListNode<TNode>
        {
            TNode nextNode = startNode;
            while (false == object.ReferenceEquals(anchor, nextNode))
            {
                yield return nextNode;
                nextNode = nextNode.Next;
            }
        }

        public static bool IsEmpty<TNode>(TNode anchor)
            where TNode : IListNode<TNode>
        {
            return (false == ListNodeExtensions.HasNext(anchor, anchor));
        }

        public static bool HasNext<TNode>(TNode anchor, TNode node)
            where TNode : IListNode<TNode>
        {
            return (false == object.ReferenceEquals(anchor, node.Next));
        }

        public static bool TryGetNext<TNode>(TNode anchor, TNode node, out TNode nextNode)
            where TNode : IListNode<TNode>
        {
            nextNode = default(TNode);
            if (false == ListNodeExtensions.HasNext(anchor, node))
            {
                return false;
            }
            nextNode = node.Next;
            return true;
        }
    }
}
