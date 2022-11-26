using System;

namespace UnionFindDelete
{
    public static class NodeExtensions
    {
        public static void SwapValues<T>(INode<T> node1, INode<T> node2)
        {
            T value = node1.Value;
            node1.Value = node2.Value;
            node2.Value = value;
        }

        public static void ValidateEquals<T>(INode<T> node1, INode<T> node2)
        {
            if (object.Equals(node1.Value, node2.Value))
            {
                return;
            }
            throw new Exception("Node values are different.");
        }
    }
}
