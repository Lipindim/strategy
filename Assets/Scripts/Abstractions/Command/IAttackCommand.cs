public interface IAttackCommand : ICommand
{
    public ISelectable Target { get; }
}

