using System;
using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        public Transform targetFollow;
        public bool followVertical = false;
        public bool followHorizontal = true;
        public float smoothTime;
        private void FixedUpdate()
        {
            var currentPos = transform.position;
            var targetPos = new Vector3(targetFollow.position.x, targetFollow.position.y, currentPos.z);
            if (!followVertical)
                targetPos.y = currentPos.y;
            if (!followHorizontal)
                targetPos.x = currentPos.x;

            transform.position = Vector3.Lerp(currentPos, targetPos, Time.fixedDeltaTime * smoothTime);
        }
    }
}