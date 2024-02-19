using UnityEngine;

namespace Mario.Scripts
{
    public class TeleportProjectile : MonoBehaviour
    {
        private const string TeleportTag = "TeleportFloor";

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag(TeleportTag)) return;
            TeleportPlayer(other.GetContact(0).point);
        }

        private static void TeleportPlayer(Vector3 hitPosition)
        {
            if (Camera.main == null) return;
        
            var playerTrans = Camera.main.transform.parent.parent.parent;
            playerTrans.position = hitPosition;
        }
    }
}
