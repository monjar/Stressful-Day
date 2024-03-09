using System;
using UnityEngine;

namespace Character
{
    [Serializable]
    public class CharacterStats
    {
        public float maxStress;
        [SerializeField] private float _stressMeter;


        public float CurrentStress => _stressMeter;
        private void Start()
        {
            _stressMeter = 0;
        }

        public void AddStress(float stressToAdd)
        {
            _stressMeter += stressToAdd;
            if (_stressMeter > maxStress)
            {
                Debug.Log("STRESSING!!");
            }

        }
    }
}