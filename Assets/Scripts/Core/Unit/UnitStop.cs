using System.Threading;
using System.Threading.Tasks;


public class UnitStop : CommandExecutorBase<IStopCommand>
{
	public CancellationTokenSource CancellationTokenSource { get; set; }

	public override Task ExecuteSpecificCommand(IStopCommand command)
	{
		CancellationTokenSource?.Cancel();
		return Task.CompletedTask;
	}
}

