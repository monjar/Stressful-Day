using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SplashScreen : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public Image image;
        public List<Sprite> pictures;
        public float pictureIntervals;

        public bool skipSplash;
        public Action OnSplashEnd;

        private void Start()
        {
            if (skipSplash)
            {
                
                image.enabled = false;
                OnSplashEnd?.Invoke();
                return;
            }
            image.enabled = true;
            StartCoroutine(ChangePicturesWithDelay(pictureIntervals));
        }

        public IEnumerator ChangePicturesWithDelay(float delay)
        {
            int pictureIndex = 0;
            while (pictureIndex < pictures.Count)
            {
                image.sprite = pictures[pictureIndex++];
                _animator.SetTrigger("FadeToBlack");
                yield return new WaitForSecondsRealtime(delay);
            }

            image.enabled = false;
            OnSplashEnd?.Invoke();
        }
    }
}