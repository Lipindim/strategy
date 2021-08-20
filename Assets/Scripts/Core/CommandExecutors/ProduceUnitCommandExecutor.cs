using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;


public class ProduceUnitCommandExecutor : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
{
    public IObservable<GameObject> LastProducedUnitObservable => _lastProducedUnit;
    private ReactiveProperty<GameObject> _lastProducedUnit = new ReactiveProperty<GameObject>();

    public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;

    public ProduceUnitCommand ProduceUnitCommand => _produceUnitCommand;

    [SerializeField] private ProduceUnitCommand _produceUnitCommand;
    [SerializeField] private Transform _unitsParent;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _maximumUnitsInQueue = 6;

    private ReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();

    private int _factionId;

    private void Start()
    {
        _factionId = GetComponent<IFactionMember>().FactionId;
    }

    private void Update()
    {
        if (_queue.Count == 0)
        {
            return;
        }

        var innerTask = (UnitProductionTask)_queue[0];
        innerTask.TimeLeft -= Time.deltaTime;
        if (innerTask.TimeLeft <= 0)
        {
            removeTaskAtIndex(0);
            _lastProducedUnit.Value = Instantiate(innerTask.UnitPrefab, _spawnPoint.position, Quaternion.identity, _unitsParent);
            var factionMember = _lastProducedUnit.Value.GetComponent<IFactionMember>();
            factionMember.SetFaction(_factionId);
        }
    }

    public void Cancel(int index) => removeTaskAtIndex(index);

    private void removeTaskAtIndex(int index)
    {
        for (int i = index; i < _queue.Count - 1; i++)
    		{
            _queue[i] = _queue[i + 1];
        }
        _queue.RemoveAt(_queue.Count - 1);
    }

    public override Task ExecuteSpecificCommand(IProduceUnitCommand command)
    {
        _queue.Add(new UnitProductionTask(command.ProductionTime, command.Icon, command.UnitPrefab, command.UnitName));
        return Task.CompletedTask;
    }
}