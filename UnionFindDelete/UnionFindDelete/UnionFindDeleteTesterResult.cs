namespace UnionFindDelete
{
    public class UnionFindDeleteTesterResult<T>
    {
        #region Constructors

        public UnionFindDeleteTesterResult(UFNode<T> ufElement, UFDElement<T> ufdElement)
        {
            this.Id = ufdElement.Node.GetHashCode();
            this.UFValue = ufElement.Value;
            this.UFDValue = ufdElement.Value;
        }

        #endregion

        #region Public Properties

        public int Id { get; set; }
        public T UFValue { get; set; }
        public T UFDValue { get; set; }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", this.Id, this.UFValue, this.UFDValue);
        }

        #endregion
    }
}
