using System;
using System.Linq;

namespace UnionFindDelete
{
    public class UnionFindDelete<T> : IUnionFindDelete<UFDElement<T>, T>
    {
        #region Constants

        public const int LOCAL_REBUILD_NON_ROOT_CHILDREN = 2;
        public const int LOCAL_REBUILD_ROOT_CHILDREN = 3;
        public static readonly int FULL_TREE_MIN_AMOUNT_OF_CHILDREN =
            Math.Max(LOCAL_REBUILD_ROOT_CHILDREN, LOCAL_REBUILD_NON_ROOT_CHILDREN);
        public static readonly int FULL_TREE_MIN_SIZE = FULL_TREE_MIN_AMOUNT_OF_CHILDREN + 1; //children + parent

        #endregion

        #region IUnionFindDelete<UFDElement<T>,T> Members

        public UFDElement<T> Make(T value)
        {
            return new UFDElement<T>(value);
        }

        public UFDElement<T> Union(UFDElement<T> rootElement1, UFDElement<T> rootElement2)
        {
            return this.Union(rootElement1.Node, rootElement2.Node).Value;
        }

        public UFDElement<T> Find(UFDElement<T> element)
        {
            return this.Find(element.Node).Value;
        }

        public void Delete(UFDElement<T> element)
        {
            this.Delete(element.Node);
        }

        #endregion

        #region Protected Methods

        protected UFDNode<T> Union(UFDNode<T> root1, UFDNode<T> root2)
        {
            TreeNodeExtensions.ValidateRootNode(root1);
            TreeNodeExtensions.ValidateRootNode(root2);
            if (this.IsSmallTree(root1))
            {
                return this.UnionSmall(root1, root2);
            }
            if (this.IsSmallTree(root2))
            {
                return this.UnionSmall(root2, root1);
            }
            if (root1.Rank < root2.Rank)
            {
                this.Link(root1, root2);
                return root2;
            }
            if (root2.Rank < root1.Rank)
            {
                this.Link(root2, root1);
                return root1;
            }
            this.Link(root1, root2);
            root2.Rank++;
            return root2;
        }

        protected UFDNode<T> Find(UFDNode<T> node)
        {
            UFDNode<T> root = null;
            foreach (UFDNode<T> pathNode in TreeNodeExtensions.EnumerateRootPath(node).ToArray())
            {
                this.RelinkToGrandParent(pathNode);
                root = pathNode;
            }
            return root;
        }

        protected void Delete(UFDNode<T> node)
        {
            if (this.IsSmallTree(node))
            {
                //After the delete the tree will remain reduced
                this.DeleteSmall(node);
            }
            else
            {
                //After the delete the tree will remain full
                UFDNode<T> leafNode = this.FindLeaf(node);
                this.SwapValues(node, leafNode);
                UFDNode<T> parentNode = leafNode.Parent;
                this.DeleteLeaf(leafNode);
                this.LocalRebuild(parentNode);
            }
        }

        protected bool IsSmallTree(UFDNode<T> node)
        {
            // Big tree considered a tree that can loose one node and still remain full, all other trees are considered
            // small and will be kept reduced and handled specially
            return (false == UFDNodeExtensions.HasSizeAtLeast(node, FULL_TREE_MIN_SIZE + 1));
        }

        protected void LocalRebuild(UFDNode<T> parentNode)
        {
            if (TreeNodeExtensions.IsRoot(parentNode))
            {
                if (ListNodeExtensions.IsEmpty(parentNode.NonLeafNode))
                {
                    // Tree is reduced
                    return;
                }
                this.LocalRebuildRoot(parentNode);
            }
            else
            {
                this.LocalRebuildNonRoot(parentNode);
            }
        }

        protected void LocalRebuildNonRoot(UFDNode<T> parent)
        {
            UFDNode<T> grandParent = parent.Parent;
            foreach (UFDNode<T> childNode in UFDNodeExtensions.EnumerateChildrenBackward(parent).Take(
                LOCAL_REBUILD_NON_ROOT_CHILDREN).ToArray())
            {
                this.RelinkLastChildToGrandParent(childNode, grandParent);
            }
            this.EnsureNodeFullOrLeaf(parent);
        }

        protected void LocalRebuildRoot(UFDNode<T> root)
        {
            UFDNode<T> nonLeafNode = root.NonLeafNode.Next.Value;
            foreach (UFDNode<T> childNode in UFDNodeExtensions.EnumerateChildrenBackward(nonLeafNode).Take(
                LOCAL_REBUILD_ROOT_CHILDREN).ToArray())
            {
                this.RelinkLastChildToGrandParent(childNode, root);
            }
            this.EnsureNodeFullOrLeaf(nonLeafNode);
        }

        protected void DeleteLeaf(UFDNode<T> leafNode)
        {
            leafNode.Parent = leafNode;
            DListNodeExtensions.Remove(leafNode.DFSNode);
            DListNodeExtensions.Remove(leafNode.NeighborNode);
        }

        protected UFDNode<T> FindLeaf(UFDNode<T> node)
        {
            if (UFDNodeExtensions.IsLeaf(node))
            {
                // Already a leaf
                return node;
            }
            DListNode<UFDNode<T>> nextNeighbor = null;
            if (ListNodeExtensions.TryGetNext(
                node.Parent.NeighborAnchor, node.NeighborNode, out nextNeighbor))
            {
                // Make sure that node is not the first child
                node = nextNeighbor.Value;
            }
            // In case that node is not the first child then the previous node in the DFS has to be the last child leaf
            // of the nodes previous neighbor
            return node.DFSNode.Prev.Value;
        }

        protected void SwapValues(UFDNode<T> node, UFDNode<T> leafNode)
        {
            NodeExtensions.SwapValues(node, leafNode);
            node.Value.Node = node;
            leafNode.Value.Node = leafNode;
        }

        protected void DeleteSmall(UFDNode<T> node)
        {
            UFDNode<T> root = node;
            UFDNode<T> leaf = node;
            if (TreeNodeExtensions.IsRoot(node))
            {
                DListNode<UFDNode<T>> firstChild = null;
                if (false == ListNodeExtensions.TryGetNext(
                    node.NeighborAnchor, node.NeighborAnchor, out firstChild))
                {
                    // Tree is only a root node
                    return;
                }
                // Has to be a leaf because the tree is small or minimal big
                leaf = firstChild.Value;
                this.SwapValues(node, leaf);
            }
            else
            {
                // Has to be the root because the tree is small or minimal big
                root = node.Parent;
            }
            this.DeleteLeaf(leaf);
            if (UFDNodeExtensions.IsLeaf(root))
            {
                root.Rank = 0;
            }
        }

        protected void RelinkToGrandParent(UFDNode<T> node)
        {
            if (TreeNodeExtensions.IsRoot(node))
            {
                return;
            }
            UFDNode<T> parent = node.Parent;
            if (TreeNodeExtensions.IsRoot(parent))
            {
                return;
            }
            UFDNode<T> grandParent = parent.Parent;
            DListNode<UFDNode<T>> nextNeighbor = null;
            if (ListNodeExtensions.TryGetNext(
                node.Parent.NeighborAnchor, node.NeighborNode, out nextNeighbor))
            {
                this.RelinkInnerChildToGrandParent(node, nextNeighbor, grandParent);
            }
            else
            {
                this.RelinkLastChildToGrandParent(node, grandParent);
            }
            this.EnsureNodeFullOrLeaf(parent);
        }

        protected void EnsureNodeFullOrLeaf(UFDNode<T> node)
        {
            if (UnionFindDeleteExtensions.IsFullNode(node))
            {
                return;
            }
            this.RelinkAllChildrenToGrandParent(node);
        }

        protected void RelinkAllChildrenToGrandParent(UFDNode<T> parent)
        {
            UFDNode<T> grandParent = parent.Parent;
            foreach (UFDNode<T> childNode in UFDNodeExtensions.EnumerateChildrenBackward(parent).ToArray())
            {
                this.RelinkLastChildToGrandParent(childNode, grandParent);
            }
            this.InitAsLeaf(parent);
        }

        protected void RelinkLastChildToGrandParent(UFDNode<T> lastChild, UFDNode<T> grandParent)
        {
            UFDNode<T> parent = lastChild.Parent;
            lastChild.Parent = grandParent;
            DListNodeExtensions.Remove(lastChild.NeighborNode);
            DListNodeExtensions.InsertAfter(parent.NeighborNode, lastChild.NeighborNode);
            //DFS list does not change, and we don't really know (efficiently) where our dfs ends
            this.UpdateNonLeafListAfterRelinkToGrandParent(lastChild, parent, grandParent);
        }

        protected void RelinkInnerChildToGrandParent(UFDNode<T> node,
            DListNode<UFDNode<T>> nextNeighbor, UFDNode<T> grandParent)
        {
            UFDNode<T> parent = node.Parent;
            DListNodeExtensions.Remove(node.NeighborNode);
            // the following is not consistent with the paper - but works just as well
            this.LinkAsFirstChild(node, grandParent);
            //DFS subtree end by left neighbor
            DListNode<UFDNode<T>> DFSEndNode = nextNeighbor.Value.DFSNode.Prev;
            DListNodeExtensions.Remove(node.DFSNode, DFSEndNode);
            DListNodeExtensions.InsertAfter(grandParent.DFSNode, node.DFSNode, DFSEndNode);
            this.UpdateNonLeafListAfterRelinkToGrandParent(node, parent, grandParent);
        }

        protected void UpdateNonLeafListAfterRelinkToGrandParent(
            UFDNode<T> node, UFDNode<T> parent, UFDNode<T> grandParent)
        {
            if (false == TreeNodeExtensions.IsRoot(grandParent))
            {
                return;
            }
            if (false == UFDNodeExtensions.IsLeaf(node))
            {
                DListNodeExtensions.InsertAfter(grandParent.NonLeafNode, node.NonLeafNode);
            }
            if (UFDNodeExtensions.IsLeaf(parent))
            {
                DListNodeExtensions.Remove(parent.NonLeafNode);
            }
            if (ListNodeExtensions.IsEmpty(grandParent.NonLeafNode))
            {
                // This means that the tree become reduced
                grandParent.Rank = 1;
            }
        }

        protected UFDNode<T> UnionSmall(UFDNode<T> small, UFDNode<T> root)
        {
            foreach (var node in UFDNodeExtensions.EnumerateDFS(small).ToArray())
            {
                this.RelinkAsLeaf(node, root);
            }
            if (0 == root.Rank)
            {
                root.Rank = 1;
            }
            return root;
        }

        protected void RelinkAsLeaf(UFDNode<T> node, UFDNode<T> root)
        {
            this.LinkAsFirstChild(node, root);
            DListNodeExtensions.InsertAfter(root.DFSNode, node.DFSNode);
            this.InitAsLeaf(node);
        }

        protected void InitAsLeaf(UFDNode<T> node)
        {
            node.Rank = 0;
            DListNodeExtensions.Init(node.NonLeafNode);
            DListNodeExtensions.Init(node.NeighborAnchor);
        }

        protected void LinkAsFirstChild(UFDNode<T> child, UFDNode<T> parent)
        {
            child.Parent = parent;
            DListNodeExtensions.InsertAfter(parent.NeighborAnchor, child.NeighborNode);
        }

        protected void Link(UFDNode<T> childRoot, UFDNode<T> parentRoot)
        {
            this.LinkAsFirstChild(childRoot, parentRoot);
            //DFS list is cyclic
            DListNodeExtensions.InsertAfter(parentRoot.DFSNode, childRoot.DFSNode, childRoot.DFSNode.Prev);
            DListNodeExtensions.InsertAfter(parentRoot.NonLeafNode, childRoot.NonLeafNode);
        }

        #endregion
    }
}
