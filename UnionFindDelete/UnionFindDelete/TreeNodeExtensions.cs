using System;
using System.Collections.Generic;
using System.Linq;

namespace UnionFindDelete
{
    public static class TreeNodeExtensions
    {
        public static bool IsRoot<TNode>(TNode node)
            where TNode : ITreeNode<TNode>
        {
            return (object.ReferenceEquals(node, node.Parent));
        }

        public static TNode FindRoot<TNode>(TNode node)
            where TNode : ITreeNode<TNode>
        {
            return TreeNodeExtensions.EnumerateRootPath(node).Last();
        }

        public static IEnumerable<TNode> EnumerateRootPath<TNode>(TNode node)
            where TNode : ITreeNode<TNode>
        {
            TreeNodeExtensions.ValidateNode(node);
            while (true)
            {
                yield return node;
                if (TreeNodeExtensions.IsRoot(node))
                {
                    yield break;
                }
                node = node.Parent;
            }
        }

        #region Validate Methods

        public static void ValidateNode<TNode>(TNode node)
        {
            if (null == node)
            {
                throw new ArgumentNullException("node is null");
            }
        }

        public static void ValidateRootNode<TNode>(TNode node)
            where TNode : ITreeNode<TNode>
        {
            TreeNodeExtensions.ValidateNode(node);
            if (false == TreeNodeExtensions.IsRoot(node))
            {
                throw new ArgumentException("node is not root");
            }
        }

        #endregion
    }
}
