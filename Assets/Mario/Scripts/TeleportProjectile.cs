using Unity.VisualScripting;
using UnityEngine;

namespace Mario.Scripts
{
    public class TeleportProjectile : MonoBehaviour
    {
        private const string TeleportTag = "TeleportFloor";

        public void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag(TeleportTag)) return;
            TeleportPlayer(other.GetContact(0).point);
        }
        public void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(TeleportTag)) return;
            TeleportPlayer(other.gameObject.transform.position);
        }
        private void TeleportPlayer(Vector3 hitPosition)
        {
            if (Camera.main == null) return;

            var playerTrans = Camera.main.transform.parent.parent.parent;
            playerTrans.position = new Vector3(hitPosition.x, playerTrans.position.y, hitPosition.z);
            Destroy(this);
        }
    }
}