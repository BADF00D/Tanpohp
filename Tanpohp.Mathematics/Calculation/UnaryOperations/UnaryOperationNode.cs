#region usings

using System;
using Tanpohp.Annotations.Resharper;

#endregion

namespace Tanpohp.Mathematics.Calculation.UnaryOperations
{
    public abstract class UnaryOperationNode : ICalculationNode
    {
        protected ICalculationNode Node;

        protected UnaryOperationNode([NotNull] ICalculationNode node)
        {
            Node = node;
        }

        #region ICalculationNode Members

        public abstract double Evaluate();

        #endregion
    }
}