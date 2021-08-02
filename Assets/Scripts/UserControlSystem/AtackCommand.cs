public class AttackCommand : IAttackCommand
{
    public ISelectable Target { get; }

    public AttackCommand(ISelectable target)
    {
        Target = target;
    }
}