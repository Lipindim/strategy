public interface IDeferredCommand : ICommand
{
    public bool Defer { get; }
}

