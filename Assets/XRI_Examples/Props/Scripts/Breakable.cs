using System;
using UnityEngine.Events;

namespace UnityEngine.XR.Content.Interaction
{
    /// <summary>
    /// Detects a collision with a tagged collider, replacing this object with a 'broken' version
    /// </summary>
    public class Breakable : MonoBehaviour
    {
        [Serializable] public class BreakEvent : UnityEvent<GameObject, GameObject> { }

        public Transform Parent;
        public MataCerdos mataCerdos;
        
        [SerializeField] [Tooltip("The 'broken' version of this object.")]
        GameObject m_BrokenVersion;

        [SerializeField]
        [Tooltip("The tag a collider must have to cause this object to break.")]
        string m_ColliderTag = "Destroyer";

        [SerializeField]
        [Tooltip("Events to fire when a matching object collides and break this object. " +
            "The first parameter is the colliding object, the second parameter is the 'broken' version.")]
        BreakEvent m_OnBreak = new BreakEvent();

        bool m_Destroyed = false;

        /// <summary>
        /// Events to fire when a matching object collides and break this object.
        /// The first parameter is the colliding object, the second parameter is the 'broken' version.
        /// </summary>
        public BreakEvent onBreak => m_OnBreak;

        void OnCollisionEnter(Collision collision)
        {
            if (m_Destroyed)
                return;

            if (collision.gameObject.tag.Equals(m_ColliderTag, System.StringComparison.InvariantCultureIgnoreCase))
            {
                
                m_Destroyed = true;
                var brokenVersion = Instantiate(m_BrokenVersion, transform.position, transform.rotation, Parent);
                brokenVersion.GetComponent<Unbreakable>().Parent = Parent;
                brokenVersion.GetComponent<Unbreakable>().mataCerdos = mataCerdos;
                m_OnBreak.Invoke(collision.gameObject, brokenVersion);
                mataCerdos.AddScore();
                Destroy(gameObject);
            }
        }
    }
}
