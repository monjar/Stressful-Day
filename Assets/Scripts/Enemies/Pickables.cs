using Character;
using UnityEngine;

namespace Enemies
{
    public class Pickables : MonoBehaviour
    {
        public bool isIncreasing;
        public float stressAmount;

        public bool isDestroyedAfterUse;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var stressPower = isIncreasing ? Mathf.Abs(stressAmount) : -Mathf.Abs(stressAmount);
            other.GetComponent<CharacterManager>().GetHitWithItem(stressPower);
            if (isDestroyedAfterUse)
                Destroy(gameObject);
        }
    }
}