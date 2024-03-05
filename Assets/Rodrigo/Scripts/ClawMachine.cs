using System.Collections;
using Mario.Scripts;
using UnityEngine.XR.Interaction.Toolkit;

namespace UnityEngine.XR.Content.Interaction
{

    public class ClawMachine : MonoBehaviour
    {
        [SerializeField]
        Transform m_ClawTransform;

        [SerializeField]
        XRSocketInteractor m_ClawSocket;

        [SerializeField]
        UfoAbductionForce m_UfoAbductionForce;

        [SerializeField]
        float m_ClawWithoutPrizeSpeed;

        [SerializeField]
        float m_ClawWithPrizeSpeed;

        [SerializeField]
        float m_ClawAbductionSpeed;

        [SerializeField]
        Vector2 m_MinClawPosition;

        [SerializeField]
        Vector2 m_MaxClawPosition;

        [SerializeField]
        ParticleSystem m_SparkliesParticle;

        [SerializeField]
        ParticleSystem m_UfoBeamParticle;

        bool m_ButtonPressed;
        Vector2 m_JoystickValue;

        void Start()
        {
            StartCoroutine(NoPrizeState());
        }

        void UpdateClawPosition(float speed)
        {
            // Posicion gancho
            var clawPosition = m_ClawTransform.localPosition;

            // Velocidad posicion
            clawPosition += new Vector3(m_JoystickValue.x * speed * Time.deltaTime, 0f,
                m_JoystickValue.y * speed * Time.deltaTime);

            // nueva posicion
            clawPosition.x = Mathf.Clamp(clawPosition.x, m_MinClawPosition.x, m_MaxClawPosition.x);
            clawPosition.z = Mathf.Clamp(clawPosition.z, m_MinClawPosition.y, m_MaxClawPosition.y);


            m_ClawTransform.localPosition = clawPosition;
        }

        IEnumerator NoPrizeState()
        {
            // Mover el gancho si el no se presiona el boton
            while (!m_ButtonPressed)
            {
                UpdateClawPosition(m_ClawWithoutPrizeSpeed);
                yield return null;
            }

            StartCoroutine(TryGrabPrizeState());
        }

        IEnumerator TryGrabPrizeState()
        {
            // Cuando se engancha lanzar particulas y activar el socket
            m_SparkliesParticle.Play();
            m_UfoBeamParticle.Play();
            m_ClawSocket.socketActive = true;
            m_UfoAbductionForce.enabled = true;

            // si el boton se presiona y el socket no tiene seleccion se sigue moviendo el gancho
            while (m_ButtonPressed && !m_ClawSocket.hasSelection)
            {
                UpdateClawPosition(m_ClawAbductionSpeed);
                yield return null;
            }

            // Desactivar el socket y las particulas
            m_UfoAbductionForce.enabled = false;
            m_SparkliesParticle.Stop();

            //Soltar el premio
            StartCoroutine(ReleasePrizeState());
        }

        IEnumerator ReleasePrizeState()
        {
           
            while (m_ButtonPressed)
            {
                UpdateClawPosition(m_ClawWithPrizeSpeed);
                yield return null;
            }

            // Soltar el premio y desactivar el socket y las particulas
            m_ClawSocket.socketActive = false;
            m_UfoBeamParticle.Stop();

            StartCoroutine(NoPrizeState());
        }


        public void OnButtonPress()
        {
            m_ButtonPressed = true;

            // Si el premio es soltado se completa el nivel
            GlobalTimer.instance.SetLevelCompletion(6);
        }

        public void OnButtonRelease()
        {
            m_ButtonPressed = false;
        }


        public void OnJoystickValueChangeX(float x)
        {
            m_JoystickValue.x = x;
        }

       
        public void OnJoystickValueChangeY(float y)
        {
            m_JoystickValue.y = y;
        }
    }
}
