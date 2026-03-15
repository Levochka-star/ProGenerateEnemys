using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEnd : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private float _speedPlatform = 15f;

    private bool _isPointEnd = false;

    private void Start()
    {
        transform.position = _startPosition.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float speed = _speedPlatform * Time.deltaTime;

        if (_isPointEnd == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPosition.position, speed);
        }

        if (_isPointEnd == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _startPosition.position, speed);
        }

        if (_isPointEnd == false && transform.position == _endPosition.position)
        {
            _isPointEnd = true;
        }
        else if (_isPointEnd == true && transform.position == _startPosition.position)
        {
            _isPointEnd = false;
        }
    }
}
