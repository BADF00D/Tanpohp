using Tanpohp.Annotations.Resharper;

namespace Tanpohp.Mathematics.Calculation.BinaryOperations
{
    public class DiversionNode : BinaryOperationNode
    {
        public DiversionNode([NotNull] ICalculationNode left, [NotNull] ICalculationNode right) : base(left, right)
        {
        }

        public override double Evaluate()
        {
            return Left.Evaluate()/Right.Evaluate();
        }
    }
}