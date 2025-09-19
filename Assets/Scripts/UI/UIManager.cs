using System;
using Character;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoSingleton<UIManager>
    {

        public Image stressBarFillImage;
        public TextMeshProUGUI timerTextMesh;

        public SplashScreen splashScreen;
        public GameObject endScreen;
        

        public override void Awake()
        {
            base.Awake();
            splashScreen.OnSplashEnd = () =>
            {
                CharacterManager.Instance.StartGame();
            };
        }

        public void SetTimer(int timeLeftInSeconds)
        {
            timerTextMesh.text = timeLeftInSeconds.ToString("00");
        }

        public void SetStressBar(float currentStress, float maxStress)
        {
            stressBarFillImage.fillAmount =   currentStress/maxStress;
        }

        public void ShowGameOver()
        {
            endScreen.gameObject.SetActive(true);
        }
    }
}