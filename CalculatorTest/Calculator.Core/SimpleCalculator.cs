namespace Calculator.Core
{
    public class SimpleCalculator : ISimpleCalculator
    {
        // checked is used for arithmatic overflow checking
        public int Add(int start, int amount)
        {
            checked
            {
                return start + amount;
            }
        }

        public int Subtract(int start, int amount)
        {
            checked
            {
                return start - amount;
            }
        }
    }
}
