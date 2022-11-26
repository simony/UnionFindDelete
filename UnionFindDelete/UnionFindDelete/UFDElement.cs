namespace UnionFindDelete
{
    public class UFDElement<T> : INode<T>
    {
        #region Constructors

        public UFDElement(T value)
        {
            this.Value = value;
            this.Node = new UFDNode<T>(this);
        }

        #endregion

        #region Public Properties

        public UFDNode<T> Node { get; set; }
        public T Value { get; set; }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return string.Format("{0}({1}, {2})", base.ToString(), this.Node.GetHashCode(), this.Value);
        }

        #endregion
    }
}
