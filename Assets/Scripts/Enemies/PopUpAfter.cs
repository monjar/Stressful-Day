using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemies
{
    public class PopUpAfter : MonoBehaviour
    {
        public List<PopupEntry> list;

        private float timeNow = 0;


        private void Update()
        {
            timeNow += Time.deltaTime;

            list.ForEach(item =>
            {
                if (!item.GameObject.IsUnityNull() && item.delay < timeNow)
                    item.GameObject.SetActive(true);
            });
        }
    }

    [Serializable]
    public struct PopupEntry
    {
        public GameObject GameObject;
        public float delay;
    }
}