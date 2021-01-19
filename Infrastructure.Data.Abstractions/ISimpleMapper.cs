namespace System.Data.Abstractions
{
    public interface ISimpleMapper<TIn, TOut>
    {
        TOut Map(TIn @in);
    }
}
