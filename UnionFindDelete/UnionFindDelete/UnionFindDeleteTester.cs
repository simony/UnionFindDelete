using System;
using System.Collections.Generic;
using System.Linq;

namespace UnionFindDelete
{
    public class UnionFindDeleteTester<T>
    {
        #region Members

        protected UnionFind<T> _unionFind = new UnionFind<T>();
        protected UnionFindDelete<T> _unionFindDelete = new UnionFindDelete<T>();

        protected Dictionary<T, UFNode<T>> _unionFindMap = new Dictionary<T, UFNode<T>>();
        protected Dictionary<T, UFDElement<T>> _unionFindDeleteMap = new Dictionary<T, UFDElement<T>>();
        protected Dictionary<UFNode<T>, UFDNode<T>> _rootMap = new Dictionary<UFNode<T>, UFDNode<T>>();

        #endregion

        #region Protected Methods

        protected UFDElement<T> GetUFD(T value)
        {
            return this._unionFindDeleteMap[value];
        }

        protected UFDElement<T> GetRootUFD(T value)
        {
            return this.GetUFD(TreeNodeExtensions.FindRoot(this.GetUFD(value).Node).Value.Value);
        }

        protected UFNode<T> GetUF(T value)
        {
            return this._unionFindMap[value];
        }

        protected UFNode<T> GetRootUF(T value)
        {
            return this.GetUF(TreeNodeExtensions.FindRoot(this.GetUF(value)).Value);
        }

        protected void ValidateValue(T value)
        {
            if (this._unionFindDeleteMap.ContainsKey(value))
            {
                return;
            }
            throw new Exception(string.Format("Invalid value", value));
        }

        protected void ValidateElementMatch(UFNode<T> ufElement, UFDElement<T> ufdElement)
        {
            if (object.ReferenceEquals(ufdElement.Node, this._rootMap[ufElement]))
            {
                return;
            }
            throw new Exception("Find mismatch between union-find and union-find-delete.");
        }

        protected void ValidateValues(UFDNode<T> node, HashSet<T> valueSet)
        {
            foreach (UFDNode<T> current in UFDNodeExtensions.EnumerateDFS(node))
            {
                T value = current.Value.Value;
                if (valueSet.Contains(value))
                {
                    valueSet.Remove(value);
                    continue;
                }
                throw new Exception(string.Format("Value {0} exists while should be delete", value));
            }
            if (0 != valueSet.Count)
            {
                throw new Exception(string.Format("Values {0} missing", valueSet.ToArray()));
            }
        }

        #endregion

        #region Public Methods

        public void Make(T value)
        {
            var ufElement = this._unionFind.Make(value);
            var ufdElement = this._unionFindDelete.Make(value);
            NodeExtensions.ValidateEquals(ufElement, ufdElement);
            UnionFindDeleteExtensions.Validate(ufdElement);
            this._unionFindMap.Add(value, ufElement);
            this._unionFindDeleteMap.Add(value, ufdElement);
            this._rootMap[ufElement] = ufdElement.Node;
        }

        public UnionFindDeleteTesterResult<T> Union(T value1, T value2)
        {
            this.ValidateValue(value1);
            this.ValidateValue(value2);
            var ufRoot1 = this.GetRootUF(value1);
            var ufRoot2 = this.GetRootUF(value2);
            var ufElement = this._unionFind.Union(ufRoot1, ufRoot2);
            var ufdRoot1 = this.GetUFD(TreeNodeExtensions.FindRoot(this.GetUFD(value1).Node).Value.Value);
            var ufdRoot2 = this.GetUFD(TreeNodeExtensions.FindRoot(this.GetUFD(value2).Node).Value.Value);
            var ufdElement = this._unionFindDelete.Union(ufdRoot1, ufdRoot2);
            this._rootMap.Remove(ufRoot1);
            this._rootMap.Remove(ufRoot2);
            this._rootMap.Add(ufElement, ufdElement.Node);
            UnionFindDeleteExtensions.Validate(ufdElement);
            return new UnionFindDeleteTesterResult<T>(ufElement, ufdElement);
        }

        public UnionFindDeleteTesterResult<T> Find(T value)
        {
            this.ValidateValue(value);
            var ufElement = this._unionFind.Find(this.GetUF(value));
            var ufdElement = this._unionFindDelete.Find(this.GetUFD(value));
            UnionFindDeleteExtensions.Validate(ufdElement);
            this.ValidateElementMatch(ufElement, ufdElement);
            return new UnionFindDeleteTesterResult<T>(ufElement, ufdElement);
        }

        public void Delete(T value)
        {
            this.ValidateValue(value);
            UFDElement<T> otherElement = null;
            var ufdElement = this.GetUFD(value);
            if (TreeNodeExtensions.IsRoot(ufdElement.Node))
            {
                if (false == UFDNodeExtensions.IsLeaf(ufdElement.Node))
                {
                    otherElement = ufdElement.Node.NeighborAnchor.Next.Value.Value;
                }
            }
            else
            {
                otherElement = ufdElement.Node.Parent.Value;
            }
            UFDNode<T> root = this.GetRootUFD(value).Node;
            HashSet<T> valueSet = new HashSet<T>(UFDNodeExtensions.EnumerateDFS(root).Select(n => n.Value.Value));
            valueSet.Remove(value);
            this._unionFindDelete.Delete(ufdElement);
            this._unionFindDeleteMap.Remove(value);
            UnionFindDeleteExtensions.Validate(ufdElement);
            if (null != otherElement)
            {
                UnionFindDeleteExtensions.Validate(otherElement);
                this.ValidateValues(root, valueSet);
            }
        }

        public bool IsSameTree(T value1, T value2)
        {
            this.ValidateValue(value1);
            this.ValidateValue(value2);
            var ufRoot1 = this.GetRootUF(value1);
            var ufRoot2 = this.GetRootUF(value2);
            return (ufRoot1.Equals(ufRoot2));
        }

        public string ToString(T value)
        {
            this.ValidateValue(value);
            return UnionFindDeleteExtensions.ToString(this.GetUFD(value).Node);
        }

        #endregion

        #region Public Properties

        public IEnumerable<T> All
        {
            get
            {
                return this._unionFindDeleteMap.Keys;
            }
        }

        #endregion
    }
}
