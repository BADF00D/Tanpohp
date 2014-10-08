#region usings

using System;
using Tanpohp.Annotations.Resharper;

#endregion

namespace Tanpohp.Mathematics.Calculation.UnaryOperations
{
    public class SquareRootNode : UnaryOperationNode
    {
        public SquareRootNode([NotNull] ICalculationNode node) : base(node)
        {
        }

        public override double Evaluate()
        {
            return Math.Sqrt(Node.Evaluate());
        }
    }
}