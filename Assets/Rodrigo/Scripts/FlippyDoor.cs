namespace UnityEngine.XR.Content.Interaction
{

    public class FlippyDoor : MonoBehaviour
    {
        [SerializeField]
        Transform m_Transform;

        int m_Count;

        void Update()
        {

            // Rotación de la puerta
            var eulerAngles = m_Transform.eulerAngles;
            var desiredAngle = m_Count > 0 ? 90f : 0f;
            eulerAngles.x = Mathf.LerpAngle(eulerAngles.x, desiredAngle, Time.deltaTime * 4f);
            m_Transform.eulerAngles = eulerAngles;
        }

        void OnTriggerEnter(Collider other)
        {
            m_Count++;
        }

        void OnTriggerExit(Collider other)
        {
            m_Count--;
        }
    }
}
