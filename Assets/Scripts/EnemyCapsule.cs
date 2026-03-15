using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyCapsule: MonoBehaviour, IPoolable<EnemyCapsule>
    {
        [SerializeField] private float _moveSpeed = 10f;

        private Transform _target;

        private float _speed;

        public event Action<EnemyCapsule> ReadyToDestroy;

        private Coroutine _coroutine;

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void OnEnable()
        {
            _speed = _moveSpeed * Time.deltaTime;
        }

        public void Stop()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<EnemyReachedDestinationCapsule>() != null)
            {
                _coroutine = StartCoroutine(WaitDelayDestroy(UnityEngine.Random.Range(0.2f, 0.8f)));
            }
        }

        private IEnumerator WaitDelayDestroy(float delay)
        {
            yield return new WaitForSeconds(delay);

            ReadyToDestroy?.Invoke(this);
        }
    }
}