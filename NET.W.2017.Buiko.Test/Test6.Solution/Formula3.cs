namespace Test6.Solution
{
    public class Formula3 : IFormulaComputer<double>
    {
        public double ComputeFormula(double current, double previous) =>
            current + (previous / current);
    }
}
