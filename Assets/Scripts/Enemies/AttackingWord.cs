using System;
using Character;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class AttackingWord : MonoBehaviour
    {
        private bool _isShooting = false;
        private Transform _target;
        private Vector3 _targetInitPos;
        private bool _isDodgeable;
        public float unDodgeableBreakDistance = 1;

        private float _stressPower;

        public void ShootTo(Transform target, bool isDodgeable, float stressPower)
        {
            _stressPower = stressPower;
            _target = target;
            _isShooting = true;
            _isDodgeable = isDodgeable;
            currentSpeed = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            currentSpeed = currentSpeed.normalized * speedMag;
            _targetInitPos = target.position;
        }

        public float smoothTime = 10;
        public float speedMag = 5f;
        private Vector2 currentSpeed;

        public float inertiaTimeExtend = 1.5f;

        public float inertiaTime = 1;

        private void Update()
        {
            if (!_isShooting)
                return;
            var targetPos = _isDodgeable ? _targetInitPos : _target.position;
            var targetSpeed = (targetPos - transform.position).normalized * speedMag;

            if (_isDodgeable)
            {
                if (Vector3.Distance(targetPos, transform.position) < unDodgeableBreakDistance)
                {
                    _isShooting = false;
                    Destroy(gameObject);
                }
            }

            if (inertiaTime <= 0f)
            {
                if (inertiaTimeExtend <= 0f)
                    currentSpeed = targetSpeed;
                else
                    currentSpeed = Vector3.Lerp(currentSpeed, targetSpeed, Time.deltaTime * smoothTime).normalized *
                                   speedMag;
            }

            inertiaTime -= Time.deltaTime;
            inertiaTimeExtend -= Time.deltaTime;
            var movementDelta = currentSpeed * Time.deltaTime;
            transform.position += new Vector3(movementDelta.x, movementDelta.y, 0);
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            _isShooting = false;
            other.GetComponent<CharacterManager>().GetHitWithItem(_stressPower);
            Destroy(gameObject);
        }
    }
}