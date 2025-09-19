using System;
using UnityEngine;

namespace Character
{
    public class WinZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            CharacterManager.Instance.Win();
        }
    }
}