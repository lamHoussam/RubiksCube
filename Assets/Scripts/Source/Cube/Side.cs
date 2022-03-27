using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cube {
    public class Side : MonoBehaviour
    {
        [SerializeField] private Center center;
        public Center m_center => center;
        [SerializeField] private FaceColor color;
        [SerializeField] private Cube m_cube;
        public FaceColor m_color => color;

        private Vector3 m_targetRotation, m_rotationAxis, m_initRot;
        private float m_targetAngle, m_speed;
        private float m_angle, m_coeff;
        private bool m_startRotation = false;
        
        private void Start() {
            m_cube = gameObject.GetComponentInParent<Cube>();

            m_rotationAxis = -GetComponent<BoxCollider>().center.normalized; 
        }

        private void FixedUpdate() {
            if(m_startRotation) ApplyRotation();
        }

        /// <summary> Initialise this side's rotation </summary>
        /// <param name="reverse"> If true rotate couterclockwise else clockwise </param>
        /// <param name="speed"> Speed to rotate the side at <param>
        public void Rotate(bool reverse, float speed) {
            if(m_startRotation) return;
            m_coeff = reverse ? 1 : -1;
            m_targetAngle = m_coeff * 90;

            m_targetRotation = transform.eulerAngles + m_rotationAxis * m_targetAngle;
            m_initRot = transform.eulerAngles;

            Debug.Log(transform.forward);
            m_speed = speed;

            m_startRotation = true;
            m_cube.m_rotationLocked = true;

            m_angle = 0;

        }

        /// <summary> Apply initialised rotation </summary>
        private void ApplyRotation(){
            if(m_angle < 90){
                GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(transform.eulerAngles + m_coeff * m_speed * Time.deltaTime * m_rotationAxis));
                // transform.rotation = Quaternion.AngleAxis (m_coeff * m_speed * Time.deltaTime, transform.forward) * transform.rotation;
                // transform.Rotate(transform.forward, m_coeff * m_speed * Time.deltaTime, Space.World);
                m_angle += m_coeff * m_speed * Time.deltaTime;
            }
            
            if(Mathf.Abs(m_angle) >= 90){ 
                transform.rotation = Quaternion.Euler(m_targetRotation);
                // yield return new WaitForSeconds(.2f);
                OnStopRotation(); 
            }
        }
        private void OnStopRotation(){
            // Debug.Log("End");

            // m_cube.DeSelectSide();
            m_startRotation = false;
            m_cube.m_rotationLocked = false;
        }

        private void OnMouseDown() {
            // Debug.Log($"Hit : {m_color}");
            m_cube.SelectSide(m_color);
        }        
    }

}   
