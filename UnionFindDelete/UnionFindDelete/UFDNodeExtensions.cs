using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnionFindDelete
{
    public static class UFDNodeExtensions
    {
        public static IEnumerable<UFDNode<T>> EnumerateChildren<T>(UFDNode<T> node)
        {
            return ListNodeExtensions.Enumerate(node.NeighborAnchor, node.NeighborAnchor.Next).Select(
                n => n.Value);
        }

        public static IEnumerable<UFDNode<T>> EnumerateChildrenBackward<T>(UFDNode<T> node)
        {
            return DListNodeExtensions.EnumerateBackward(node.NeighborAnchor, node.NeighborAnchor.Prev).Select(
                n => n.Value);
        }

        public static IEnumerable<UFDNode<T>> EnumerateDFS<T>(UFDNode<T> node)
        {
            yield return node;
            foreach (var nextNode in ListNodeExtensions.Enumerate(node.DFSNode, node.DFSNode.Next).Select(n => n.Value))
            {
                yield return nextNode;
            }
        }

        public static bool HasSizeAtLeast<T>(UFDNode<T> node, int size)
        {
            if (false == TreeNodeExtensions.IsRoot(node))
            {
                node = TreeNodeExtensions.EnumerateRootPath(node).Take(size).Last();
                if (false == TreeNodeExtensions.IsRoot(node))
                {
                    return true;
                }
            }
            return (size == UFDNodeExtensions.EnumerateDFS(node).Take(size).Count());
        }

        public static bool HasChildrenAtLeast<T>(UFDNode<T> node, int count)
        {
            return (count == UFDNodeExtensions.EnumerateChildren(node).Take(count).Count());
        }

        public static bool IsLeaf<T>(UFDNode<T> node)
        {
            return (false == UFDNodeExtensions.HasChildrenAtLeast(node, 1));
        }

        public static string ToString<T>(UFDNode<T> node)
        {
            StringBuilder builder = new StringBuilder();
            UFDNodeExtensions.BuildString(node, builder, 0);
            return builder.ToString();
        }

        private static void BuildString<T>(UFDNode<T> node, StringBuilder builder, int index)
        {
            builder.Append('\t', index);
            builder.AppendFormat("<{0}>{1}", node.Value.Value, Environment.NewLine);
            foreach (var child in UFDNodeExtensions.EnumerateChildren(node))
            {
                UFDNodeExtensions.BuildString(child, builder, index + 1);
            }
            builder.Append('\t', index);
            builder.AppendFormat("</{0}>{1}", node.Value.Value, Environment.NewLine);
        }
    }
}
