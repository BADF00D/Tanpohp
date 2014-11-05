using Tanpohp.Annotations.Resharper;

namespace Tanpohp.Mathematics.Calculation.BinaryOperations
{
    public class MultiplicationNode : BinaryOperationNode
    {
        public MultiplicationNode([NotNull]ICalculationNode left, [NotNull]ICalculationNode right) : base(left, right)
        {
        }

        public override double Evaluate()
        {
            return Left.Evaluate()*Right.Evaluate();
        }
    }
}