using Tanpohp.Annotations.Resharper;

namespace Tanpohp.Mathematics.Calculation.UnaryOperations
{
    public class SignNode : UnaryOperationNode
    {
        public SignNode([NotNull]ICalculationNode node) : base(node)
        {
        }

        public override double Evaluate()
        {
            return -Node.Evaluate();
        }
    }
}