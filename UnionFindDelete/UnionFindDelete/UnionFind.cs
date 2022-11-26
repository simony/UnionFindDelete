using System.Collections.Generic;

namespace UnionFindDelete
{
    public class UnionFind<T> : IUnionFind<UFNode<T>, T>
    {
        #region IUnionFind<UFNode<T>,T> Members

        public UFNode<T> Make(T value)
        {
            return new UFNode<T>(value);
        }

        public UFNode<T> Union(UFNode<T> root1, UFNode<T> root2)
        {
            TreeNodeExtensions.ValidateRootNode(root1);
            TreeNodeExtensions.ValidateRootNode(root2);
            if (root1.Rank < root2.Rank)
            {
                root1.Parent = root2;
                return root2;
            }
            if (root2.Rank < root1.Rank)
            {
                root2.Parent = root1;
                return root1;
            }
            root1.Parent = root2;
            root2.Rank++;
            return root2;
        }

        public UFNode<T> Find(UFNode<T> node)
        {
            TreeNodeExtensions.ValidateNode(node);
            List<UFNode<T>> nodesToCompress = new List<UFNode<T>>();
            while (false == TreeNodeExtensions.IsRoot(node))
            {
                nodesToCompress.Add(node);
                node = node.Parent;
            }
            foreach (UFNode<T> innerNode in nodesToCompress)
            {
                innerNode.Parent = node;
            }
            return node;
        }

        #endregion
    }
}
