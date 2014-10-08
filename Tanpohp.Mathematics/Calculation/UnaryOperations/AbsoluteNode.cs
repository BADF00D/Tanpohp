#region usings

using System;
using Tanpohp.Annotations.Resharper;

#endregion

namespace Tanpohp.Mathematics.Calculation.UnaryOperations
{
    public class AbsoluteNode : UnaryOperationNode
    {
        public AbsoluteNode([NotNull] ICalculationNode node) : base(node)
        {
        }

        public override double Evaluate()
        {
            return Math.Abs(Node.Evaluate());
        }
    }
}