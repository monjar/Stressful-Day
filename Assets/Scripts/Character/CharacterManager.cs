using System;
using UI;
using UnityEngine;

namespace Character
{
    public class CharacterManager : MonoBehaviour
    {
        public CharacterController characterController;
        public CharacterStats characterStats;


        public float timeLeft = 90;

        public float stressPerSecond = 3f;

        public void GetHitWithItem(float stress)
        {
            characterStats.AddStress(stress);
            if (characterStats.CurrentStress < 0)
                EndGameByStress();
            else
                UIManager.Instance.SetStressBar(characterStats.CurrentStress, characterStats.maxStress);
        }

        private bool _isGameGoing = true;


        private void EndGame()
        {
            _isGameGoing = false;
        }

        private void EndGameByStress()
        {
            Debug.Log("YOU LOST!");
        }

        private void EndGameByTimer()
        {
            Debug.Log("YOU MADE IT!");
            EndGame();
        }

        private void Update()
        {
            if (!_isGameGoing)
                return;
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {
                EndGameByTimer();
            }

            UIManager.Instance.SetTimer((int)timeLeft);
            GetHitWithItem(stressPerSecond * Time.deltaTime);
        }
    }
}