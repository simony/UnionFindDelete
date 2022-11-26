using System;
using System.Linq;

namespace UnionFindDelete
{
    public static class UnionFindDeleteExtensions
    {
        public static bool IsFullNode<T>(UFDNode<T> node)
        {
            return (UFDNodeExtensions.HasChildrenAtLeast(node, UnionFindDelete<T>.FULL_TREE_MIN_AMOUNT_OF_CHILDREN));
        }

        public static bool IsFullTree<T>(UFDNode<T> node)
        {
            UFDNode<T> root = TreeNodeExtensions.FindRoot(node);
            return (UFDNodeExtensions.EnumerateDFS(root).All(n =>
                UFDNodeExtensions.IsLeaf(n) || UnionFindDeleteExtensions.IsFullNode(n)));
        }

        public static bool IsReducedTree<T>(UFDNode<T> node)
        {
            UFDNode<T> root = TreeNodeExtensions.FindRoot(node);
            return (UFDNodeExtensions.EnumerateChildren(root).All(UFDNodeExtensions.IsLeaf));
        }

        public static void ValidateTreeFullOrReduced<T>(UFDNode<T> node)
        {
            UFDNode<T> root = TreeNodeExtensions.FindRoot(node);
            if (UnionFindDeleteExtensions.IsReducedTree(root))
            {
                return;
            }
            if (UnionFindDeleteExtensions.IsFullTree(root))
            {
                return;
            }
            throw new Exception("Tree is not full nor reduced, Invariant 2 does not hold.");
        }

        public static void ValidateRanks<T>(UFDNode<T> node)
        {
            UFDNode<T> root = TreeNodeExtensions.FindRoot(node);
            if (UnionFindDeleteExtensions.IsReducedTree(root))
            {
                if (UFDNodeExtensions.IsLeaf(root))
                {
                    if (0 != root.Rank)
                    {
                        throw new Exception("Reduced root has rank != 0.");
                    }
                }
                else
                {
                    if (1 != root.Rank)
                    {
                        throw new Exception("Reduced tree has root rank != 1.");
                    }
                }
                if (false == UFDNodeExtensions.EnumerateChildren(root).All(n => (0 == n.Rank)))
                {
                    throw new Exception("Reduced tree has leaves with rank != 0.");
                }
                return;
            }
            if (false == UFDNodeExtensions.EnumerateDFS(node).Where(n => false == TreeNodeExtensions.IsRoot(n)).All(
                n => n.Rank < n.Parent.Rank))
            {
                throw new Exception("Tree has non increasing ranks, Invariant 1 does not hold.");
            }
            if (false == UFDNodeExtensions.EnumerateDFS(node).Where(UFDNodeExtensions.IsLeaf).All(
                n => 0 == n.Rank))
            {
                throw new Exception("Tree has leaves with rank != 0.");
            }
        }

        public static void Validate<T>(UFDElement<T> element)
        {
            UnionFindDeleteExtensions.ValidateTreeFullOrReduced(element.Node);
            UnionFindDeleteExtensions.ValidateRanks(element.Node);
        }

        public static string ToString<T>(UFDNode<T> node)
        {
            UFDNode<T> root = TreeNodeExtensions.FindRoot(node);
            return UFDNodeExtensions.ToString(root);
        }
    }
}
