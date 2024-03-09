using System;
using Core;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoSingleton<UIManager>
    {

        public Image stressBarFillImage;

        public TextMeshProUGUI timerTextMesh;
        private void Start()
        {
        }

        public void SetTimer(int timeLeftInSeconds)
        {
            timerTextMesh.text = timeLeftInSeconds.ToString("00");
        }

        public void SetStressBar(float currentStress, float maxStress)
        {
            stressBarFillImage.fillAmount =   currentStress/maxStress;
        }
    }
}