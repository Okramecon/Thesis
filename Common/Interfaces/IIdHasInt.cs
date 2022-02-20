
namespace Common.Interfaces
{
    public interface IIdHasInt<T>
    {
        T Id { get; set; }
    }

    public interface IIdHasInt : IIdHasInt<int> { }

    public interface IIdHasIntNullable : IIdHasInt<int?> { }
}
