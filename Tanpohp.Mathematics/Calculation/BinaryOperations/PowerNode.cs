#region usings

using System;
using Tanpohp.Annotations.Resharper;

#endregion

namespace Tanpohp.Mathematics.Calculation.BinaryOperations
{
    public class PowerNode : BinaryOperationNode
    {
        public PowerNode([NotNull] ICalculationNode @base, [NotNull] ICalculationNode exponent) : base(@base, exponent)
        {
        }

        public override double Evaluate()
        {
            return Math.Pow(Left.Evaluate(), Right.Evaluate());
        }
    }
}