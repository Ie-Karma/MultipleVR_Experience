using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.XR.Content.Interaction
{
    /// <summary>
    /// Rewinds positional changes made this object and its children to restore it back to a 'complete' object
    /// </summary>
    public class Unbreakable : MonoBehaviour
    {
        [Serializable] public class RestoreEvent : UnityEvent<GameObject> { }
        
        public Transform Parent;
        public MataCerdos mataCerdos;

        [SerializeField]
        [Tooltip("How long to wait before rewinding the object's motion.")]
        float m_RestTime = 1.0f;

        [SerializeField]
        [Tooltip("How long to spend restoring the object.")]
        float m_RestoreTime = 2.0f;

        [SerializeField]
        [Tooltip("A 'non broken' object to replace this object with when motion rewinding is complete.")]
        GameObject m_RestoredVersion;

        [SerializeField]
        [Tooltip("Events to fire when the 'non broken' object is restored.")]
        RestoreEvent m_OnRestore = new RestoreEvent();

        bool m_Resting = true;
        float m_Timer = 0.0f;
        bool m_Restored = false;

        struct ChildPoses
        {
            internal Pose m_StartPose;
            internal Pose m_EndPose;
        }

        Dictionary<Transform, ChildPoses> m_ChildPoses = new Dictionary<Transform, ChildPoses>();
        List<Transform> m_ChildTransforms = new List<Transform>();

        /// <summary>
        /// Events to fire when the 'non broken' object is restored.
        /// </summary>
        public RestoreEvent onRestore => m_OnRestore;

        void Start()
        {
            // Go through all children
            GetComponentsInChildren(m_ChildTransforms);

            // Cache their start positions
            foreach (var child in m_ChildTransforms)
            {
                m_ChildPoses.Add(child, new ChildPoses { m_StartPose = new Pose(child.position, child.rotation) });
            }
        }

        void Update()
{
    if (m_Restored)
        return;

    // Phase 1 - wait to rewind
    // Phase 2 - (removed)
    // Phase 3 - replace object, destroy this one

    m_Timer += Time.deltaTime;

    if (m_Resting)
    {
        if (m_Timer > m_RestTime)
        {
            m_Timer = 0.0f;
            m_Resting = false;

            // The second phase code has been removed here
        }
    }
    else
    {
        var timePercent = m_Timer / m_RestoreTime;
        if (timePercent > 1.0f)
        {
            m_Restored = true;
            var restoredVersion = Instantiate(m_RestoredVersion, Parent.position, Parent.rotation, Parent);
            restoredVersion.GetComponent<Breakable>().Parent = Parent;
            restoredVersion.GetComponent<Breakable>().mataCerdos = mataCerdos;
            restoredVersion.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            m_OnRestore.Invoke(restoredVersion);
            Destroy(gameObject);
        }
    }
}
    }
}
