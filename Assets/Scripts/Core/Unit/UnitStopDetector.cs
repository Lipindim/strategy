using System;
using UnityEngine;


public class UnitStopDetector : MonoBehaviour
{
    public Action StopDetected;

    [SerializeField] private UnitMove _unitMove;
    [SerializeField] private int MAX_AT_ONE_POINT_TIMES = 30;
    [SerializeField] private float MOVEMENT_ERROR = 0.001f;


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
                StopDetected?.Invoke();
            _previousPosition = _unitMove.transform.position;
        }
    }
}
