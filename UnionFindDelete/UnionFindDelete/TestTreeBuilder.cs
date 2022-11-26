using System;

namespace UnionFindDelete
{
    public class TestTreeBuilder
    {
        #region Constants

        public static readonly int MIN_AMOUNT_OF_LEAVES =
            UnionFindDelete<int>.FULL_TREE_MIN_AMOUNT_OF_CHILDREN + 1;

        #endregion

        #region Members

        protected int _nextValue = 0;
        protected UnionFindDeleteTester<int> _tester = null;

        #endregion

        #region Constructors

        public TestTreeBuilder(UnionFindDeleteTester<int> tester)
        {
            this._tester = tester;
        }

        #endregion

        #region Protected Methods

        protected int AllocateNextValue()
        {
            int nextValue = this._nextValue;
            this._nextValue++;
            return nextValue;
        }

        #endregion

        #region Public Properties

        public int NextValue
        {
            get
            {
                return this._nextValue;
            }
        }

        #endregion

        #region Public Methods

        public int Build1(int leafCount)
        {
            int rootValue = this.AllocateNextValue();
            this._tester.Make(rootValue);
            for (int i = 0; i < leafCount; i++)
            {
                int nextValue = this.AllocateNextValue();
                this._tester.Make(nextValue);
                this._tester.Union(nextValue, rootValue);
            }
            return rootValue;
        }

        public int Build2(int leafCount, int lelvel2Count)
        {
            int rootValue = this.Build1(MIN_AMOUNT_OF_LEAVES);
            for (int i = 0; i < lelvel2Count; i++)
            {
                int nextValue = this.Build1(Math.Max(leafCount, MIN_AMOUNT_OF_LEAVES));
                this._tester.Union(nextValue, rootValue);
            }
            return rootValue;
        }

        public int Build3(int leafCount, int lelvel2Count, int level3Count)
        {
            int rootValue = this.Build2(0, 1);
            for (int i = 0; i < lelvel2Count; i++)
            {
                int nextValue = this.Build2(leafCount, level3Count);
                this._tester.Union(nextValue, rootValue);
            }
            return rootValue;
        }

        public int Build(int count, int depth)
        {
            int rootValue = this.Build1(MIN_AMOUNT_OF_LEAVES);
            if (0 < depth)
            {
                for (int i = 0; i < count; i++)
                {
                    int nextValue = this.Build(count, depth - 1);
                    this._tester.Union(nextValue, rootValue);
                }
            }
            return rootValue;
        }

        #endregion
    }
}
