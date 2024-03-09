using UnityEngine;

namespace Character
{
    public class CharacterManager : MonoBehaviour
    {
        public CharacterController characterController;
        public CharacterStats characterStats;


        public void GetHitWithItem(float stress)
        {
            characterStats.AddStress(stress);
        }
    }
}