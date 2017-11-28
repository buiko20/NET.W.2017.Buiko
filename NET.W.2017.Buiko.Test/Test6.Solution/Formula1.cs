namespace Test6.Solution
{
    public class Formula1 : IFormulaComputer<int>
    {
        public int ComputeFormula(int current, int previous) =>
            current + previous;
    }
}
