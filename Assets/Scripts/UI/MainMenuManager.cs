using System;
using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuManager : MonoBehaviour
    {
        private void Start()
        {
            AudioManager.Instance.PlayMusic(AudioName.MUSIC_MENU);
        }

        public GameObject creditsModal;
        public void OnStartGame()
        {
            AudioManager.Instance.PlayOneShot(AudioName.CLICK_UI);
            SceneManager.LoadScene("Level1");
        }
        
        public void OnCreditsClose()
        {
            creditsModal.SetActive(false);
            AudioManager.Instance.PlayOneShot(AudioName.CLICK_UI);
        }
        public void OnCredits()
        {
            creditsModal.SetActive(true);
            AudioManager.Instance.PlayOneShot(AudioName.CLICK_UI);
        }
        public void OnExitGame()
        {
            AudioManager.Instance.PlayOneShot(AudioName.CLICK_UI);
            Application.Quit();
            
        }
    }
}