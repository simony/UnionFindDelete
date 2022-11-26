using System.Collections.Generic;

namespace UnionFindDelete
{
    public static class DListNodeExtensions
    {
        public static IEnumerable<TNode> EnumerateBackward<TNode>(TNode anchor, TNode startNode)
            where TNode : IDListNode<TNode>
        {
            TNode nextNode = startNode;
            while (false == object.ReferenceEquals(anchor, nextNode))
            {
                yield return nextNode;
                nextNode = nextNode.Prev;
            }
        }

        public static void Init<TNode>(TNode node)
            where TNode : IDListNode<TNode>
        {
            node.Next = node;
            node.Prev = node;
        }

        public static void InsertAfter<TNode>(TNode currentNode, TNode newNode)
            where TNode : IDListNode<TNode>
        {
            DListNodeExtensions.InsertAfter(currentNode, newNode, newNode);
        }

        public static void InsertAfter<TNode>(TNode currentNode, TNode beginNode, TNode endNode)
            where TNode : IDListNode<TNode>
        {
            endNode.Next = currentNode.Next;
            beginNode.Prev = currentNode;
            currentNode.Next.Prev = endNode;
            currentNode.Next = beginNode;
        }

        public static void Remove<TNode>(TNode node)
            where TNode : IDListNode<TNode>
        {
            DListNodeExtensions.Remove(node, node);
        }

        public static void Remove<TNode>(TNode beginNode, TNode endNode)
            where TNode : IDListNode<TNode>
        {
            beginNode.Prev.Next = endNode.Next;
            endNode.Next.Prev = beginNode.Prev;
            beginNode.Prev = endNode;
            endNode.Next = beginNode;
        }
    }
}
