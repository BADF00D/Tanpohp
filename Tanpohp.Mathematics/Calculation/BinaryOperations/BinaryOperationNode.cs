#region usings

using Tanpohp.Annotations.Resharper;

#endregion

namespace Tanpohp.Mathematics.Calculation.BinaryOperations
{
    public abstract class BinaryOperationNode : ICalculationNode
    {
        protected ICalculationNode Left, Right;

        protected BinaryOperationNode([NotNull] ICalculationNode left, [NotNull] ICalculationNode right)
        {
            Left = left;
            Right = right;
        }

        #region ICalculationNode Members

        public abstract double Evaluate();

        #endregion
    }
}