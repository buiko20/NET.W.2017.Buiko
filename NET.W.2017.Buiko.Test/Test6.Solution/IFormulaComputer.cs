namespace Test6.Solution
{
    public interface IFormulaComputer<T>
    {
        T ComputeFormula(T current, T previous);
    }
}
