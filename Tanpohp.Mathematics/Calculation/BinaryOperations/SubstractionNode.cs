using Tanpohp.Annotations.Resharper;

namespace Tanpohp.Mathematics.Calculation.BinaryOperations
{
    public class SubstractionNode : BinaryOperationNode
    {
        public SubstractionNode([NotNull] ICalculationNode left, [NotNull] ICalculationNode right)
            : base(left, right)
        {
        }

        public override double Evaluate()
        {
            return Left.Evaluate() - Right.Evaluate();
        }
    }
}