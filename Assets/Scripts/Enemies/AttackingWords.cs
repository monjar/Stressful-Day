using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Enemies
{
    public class AttackingWords : MonoBehaviour
    {
        public float stressAmount = 10f;
        public float spacing = 0.02f;
        public TextMeshPro wordPrefab;
        public string text;
        public List<AttackingWord> attackingWords = new List<AttackingWord>();
        public bool shouldUpdateWords = false;
        public Image image;
        public bool isTimed = false;
        private bool _isCheckingForHit = true;
        public bool isDodgeable;
        private Transform _characterTransform;
        public float timeDelay = 3;

        private void Start()
        {
            _characterTransform = FindObjectOfType<CharacterController>().transform;
            InitWords();
        }

        private void OnEnable()
        {
            if (isTimed)
            {
                StartCoroutine(ShootAfter(timeDelay));
            }
        }
        

        public IEnumerator ShootAfter(float delay)
        {
            yield return new WaitForSeconds(delay);
            ShootAtPlayer();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isCheckingForHit || isTimed)
                return;

            ShootAtPlayer();
        }

        private void ShootAtPlayer()
        {
            _isCheckingForHit = false;
            GetComponent<ContentSizeFitter>().enabled = false;
            GetComponent<HorizontalLayoutGroup>().enabled = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                var attackingWord = transform.GetChild(i).GetComponent<AttackingWord>();

                attackingWord.ShootTo(_characterTransform, isDodgeable, stressAmount / (float)transform.childCount);
                attackingWords.Add(attackingWord);
            }

            StartCoroutine(DisableImageAfter(1));
        }

        private IEnumerator DisableImageAfter(float delay)
        {
            yield return new WaitForSeconds(delay);

            image.enabled = false;
            
            yield return new WaitForSeconds(delay + 4);
            Destroy(transform.parent.gameObject);
        }


        private void OnValidate()
        {
            if (!Application.isPlaying && shouldUpdateWords)
                InitWords();
        }

        private void InitWords()
        {
#if UNITY_EDITOR

            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                UnityEditor.EditorApplication.delayCall += () =>
                {
                    if (!child.IsUnityNull())
                        DestroyImmediate(child.gameObject);
                };
            }


#elif UNITY_STANDALONE_WIN
                    for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
              
                    if (!child.IsUnityNull())
                        Destroy(child.gameObject);
                
            }

#endif
            var wordsSplit = text.Split(" ");

            for (int i = 0; i < wordsSplit.Length; i++)
            {
                if (wordsSplit[i].Trim().Length == 0)
                    continue;
                var wordText = Instantiate(wordPrefab, transform);

                wordText.text = wordsSplit[i];
                if (i == 0)
                {
                    wordText.text = "    " + wordText.text;
                }

                if (i == wordsSplit.Length - 1)
                {
                    wordText.text = wordText.text + "    .";
                }
            }
        }
    }
}