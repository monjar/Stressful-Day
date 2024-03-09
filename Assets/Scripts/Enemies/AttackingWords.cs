using System;
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

        private bool _isCheckingForHit = true;
        public bool isDodgeable;
        private Transform _characterTransform;

        private void Start()
        {
            _characterTransform = FindObjectOfType<CharacterController>().transform;
            InitWords();
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!_isCheckingForHit)
                return;
            _isCheckingForHit = false;
            GetComponent<ContentSizeFitter>().enabled = false;
            GetComponent<HorizontalLayoutGroup>().enabled = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                var attackingWord = transform.GetChild(i).GetComponent<AttackingWord>();

                attackingWord.ShootTo(_characterTransform, isDodgeable, stressAmount / (float)transform.childCount);
                attackingWords.Add(attackingWord);
            }
        }


        private void OnValidate()
        {
            if (!Application.isPlaying && shouldUpdateWords)
                InitWords();
        }

        private void InitWords()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                UnityEditor.EditorApplication.delayCall += () =>
                {
                    if (!child.IsUnityNull())
                        DestroyImmediate(child.gameObject);
                };
            }

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