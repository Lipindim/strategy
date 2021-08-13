public interface IUnitProductionTask : IIcon
{
	public string UnitName { get; }
	public float TimeLeft { get; }
	public float ProductionTime { get; }
}