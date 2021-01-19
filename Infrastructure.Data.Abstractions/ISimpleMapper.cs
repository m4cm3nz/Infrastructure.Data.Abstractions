namespace Infrastructure.Data.Abstractions
{
    public interface ISimpleMapper<TIn, TOut>
    {
        TOut Map(TIn @in);
    }
}
