
namespace AlwaysUp.Save
{
    public interface IPlayerPrefSO<T>
    {
        string Key { get; }
        T DefaultValue { get; }

        T Get();
        void Set(T value);

        void Reset();
    }
}
