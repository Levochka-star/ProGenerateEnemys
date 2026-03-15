using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Scripts
{
    public class SpawnEnemy : Spawner<Enemy>
    {
        [SerializeField] private Transform _poinTarget;
        [SerializeField] private float _timeSpawn = 2f;

        private Coroutine _coroutine;

        private void Start()
        {
            Work();
        }

        public void Stop()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        private void Work()
        {
            _coroutine = StartCoroutine(WaitDelayDestroy(_timeSpawn));
        }

        public Vector3 GenerateRandomVector3()
        {
            var nextTransform = transform.position;

            nextTransform.x = (UnityEngine.Random.Range(transform.position.x - transform.
            localScale.x * 0.5f, transform.position.x + transform.localScale.x * 0.5f));

            nextTransform.y = (UnityEngine.Random.Range(transform.position.y - transform.
            localScale.y * 0.5f, transform.position.y + transform.localScale.y * 0.5f));

            nextTransform.z = (UnityEngine.Random.Range(transform.position.z - transform.
            localScale.z * 0.5f, transform.position.z + transform.localScale.z * 0.5f));

            return nextTransform;
        }

        private IEnumerator WaitDelayDestroy(float delay)
        {
            yield return new WaitForSeconds(delay);

            Enemy enemy = Spawn();

            enemy.SetTarget(_poinTarget);
            enemy.transform.position = GenerateRandomVector3();

            Work();
        }
    }
}