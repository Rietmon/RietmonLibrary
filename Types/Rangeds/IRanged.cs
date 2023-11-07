namespace DamnLibrary.Types.Rangeds
{
    public interface IRanged<T>
    {
        public T MinimalValue { get; set; }

        public T MaximalValue { get; set; }

        public T RandomValue { get; }
    }
}