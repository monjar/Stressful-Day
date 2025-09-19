using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Core;
using Enemies;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Character
{
    public class CharacterManager : MonoSingleton<CharacterManager>
    {
        public CharacterController characterController;
        public CharacterStats characterStats;
        public List<Thoughts> thoughts;
        public bool isWinOnTime;
        public string nextLevel = "Level2";
        public float timeLeft = 90;
        public AudioName musicName;
        public float stressPerSecond = 3f;
        public float currentTime = 0;
        public bool IsGameGoing => _isGameGoing;

        private int currentThoughtIndex;

        public override void Awake()
        {
            base.Awake();
            Time.timeScale = 0;
            currentThoughtIndex = 0;
            currentTime = 0;
        }

        public void StartGame()
        {
            Time.timeScale = 1;
            AudioManager.Instance.PlayMusic(musicName);
        }

        public void GetHitWithItem(float stress)
        {
            if (!_isGameGoing)
                return;
            characterStats.AddStress(stress);
            if (characterStats.CurrentStress > characterStats.maxStress)
                EndGameByStress();
            else
                UIManager.Instance.SetStressBar(characterStats.CurrentStress, characterStats.maxStress);
        }

        private bool _isGameGoing = true;


        public void Win()
        {
            _isGameGoing = false;

            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlayOneShot(AudioName.WIN);
            SceneManager.LoadScene(nextLevel);
            
        }

        private void EndGame()
        {
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlayOneShot(AudioName.GAME_OVER);
            _isGameGoing = false;
            UIManager.Instance.ShowGameOver();
            StartCoroutine(GoToMainAfter(3));
        }

        public IEnumerator GoToMainAfter(float delay)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene("MainMenu");
        }

        private void EndGameByStress()
        {
            UIManager.Instance.SetStressBar(characterStats.maxStress, characterStats.maxStress);


            Debug.Log("YOU LOST!");
            EndGame();
        }

        private void EndGameByTimer()
        {
            Debug.Log("YOU MADE IT!");
            if (isWinOnTime)
                Win();
            else
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

            currentTime += Time.deltaTime;
            if (currentThoughtIndex < thoughts.Count && thoughts[currentThoughtIndex].delay < currentTime)
            {
                thoughts[currentThoughtIndex].attackingWords.gameObject.SetActive(true);
                currentThoughtIndex++;
            }
        }
    }

    [Serializable]
    public struct Thoughts
    {
        public AttackingWords attackingWords;
        public float delay;
    }
}