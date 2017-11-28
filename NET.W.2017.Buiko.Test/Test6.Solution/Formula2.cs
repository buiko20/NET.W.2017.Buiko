﻿namespace Test6.Solution
{
    public class Formula2 : IFormulaComputer<int>
    {
        public int ComputeFormula(int current, int previous) =>
            (6 * current) - (8 * previous);
    }
}
