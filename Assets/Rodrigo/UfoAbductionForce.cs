using System.Collections.Generic;

namespace UnityEngine.XR.Content.Interaction
{

    public class UfoAbductionForce : MonoBehaviour
    {
        [SerializeField]
        float m_MinForceMagnitude;

        [SerializeField]
        float m_MaxForceMagnitude;

        [SerializeField]
        Collider m_Trigger;

        float m_ButtonValue;
        readonly List<Rigidbody> m_Rigidbodies = new List<Rigidbody>();

        void Awake()
        {
            m_Trigger.enabled = false;
        }

        void OnEnable()
        {
            m_Trigger.enabled = true;
        }

        void OnDisable()
        {
            m_Trigger.enabled = false;
            m_Rigidbodies.Clear();
        }

        void FixedUpdate()
        {
            var deltaForce = m_MaxForceMagnitude - m_MinForceMagnitude;
            var force = transform.up * (m_MinForceMagnitude + deltaForce * m_ButtonValue);
            foreach (var rigidbody in m_Rigidbodies)
                rigidbody.AddForce(force, ForceMode.Acceleration);
        }

        void OnTriggerEnter(Collider other)
        {
            var otherRigidbody = other.GetComponent<Rigidbody>();
            if (otherRigidbody != null)
                m_Rigidbodies.Add(otherRigidbody);
        }

        void OnTriggerExit(Collider other)
        {
            var otherRigidbody = other.GetComponent<Rigidbody>();
            if (otherRigidbody != null)
                m_Rigidbodies.Remove(otherRigidbody);
        }

        public void OnButtonValueChange(float value)
        {
            m_ButtonValue = value;
        }
    }
}
