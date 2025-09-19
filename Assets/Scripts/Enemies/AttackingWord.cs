using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
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
        private float unDodgeableBreakDistance = 1.05f;
        private float unDodgeableTime = 4;
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
            if (_isDodgeable)
                StartCoroutine(DestroyAfterTime());
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
            else
            {
                _targetInitPos = _target.position;
            }

            inertiaTime -= Time.deltaTime;
            inertiaTimeExtend -= Time.deltaTime;
            var movementDelta = currentSpeed * Time.deltaTime;
            transform.position += new Vector3(movementDelta.x, movementDelta.y, 0);
        }

        private IEnumerator DestroyAfterTime()
        {
            yield return new WaitForSeconds(unDodgeableTime);
            _isShooting = false;
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!_isShooting)
                return;
            _isShooting = false;
            AudioManager.Instance.PlayOneShot(AudioName.HIT);
            other.GetComponent<CharacterManager>().GetHitWithItem(_stressPower);
            Destroy(gameObject);
        }
    }
}