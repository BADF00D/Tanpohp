namespace Tanpohp.Mathematics.Calculation
{
    public class ValueNode : ICalculationNode
    {
        private readonly double _value;

        public ValueNode(double value)
        {
            _value = value;
        }

        public double Evaluate()
        {
            return _value;
        }
    }
}