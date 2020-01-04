namespace CashFlow.Core.Interfaces
{
    public interface ICrud<T>
    {
        T Get(long ID);
        void Add(T model);
        void Remove(T model);
    }
}