using UnityEngine;


public class UnitMovevemtCanceler : MonoBehaviour
{
    private const int MAX_AT_ONE_POINT_TIMES = 30;
    private const float MOVEMENT_ERROR = 0.0000001f;

    [SerializeField] private UnitMove _unitMove;
    [SerializeField] private UnitStop _unitStop;

    private Vector3 _previousPosition;
    private int _atOnePointTimes = 0;

    private void Update()
    {
        if (_unitMove.IsMoving)
        {
            float eps = (_unitMove.transform.position - _previousPosition).sqrMagnitude;
            if ((_unitMove.transform.position - _previousPosition).sqrMagnitude < MOVEMENT_ERROR)
                _atOnePointTimes++;
            else
                _atOnePointTimes = 0;

            if (_atOnePointTimes > MAX_AT_ONE_POINT_TIMES)
                _unitStop.CancellationTokenSource.Cancel();
            _previousPosition = _unitMove.transform.position;
        }
    }
}
