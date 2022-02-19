
namespace Common.Interfaces
{
    public interface IIdHas<T>
    {
        T Id { get; set; }
    }

    public interface IIdHasInt : IIdHas<int> { }

    public interface IIdHasIntNullable : IIdHas<int?> { }
}
