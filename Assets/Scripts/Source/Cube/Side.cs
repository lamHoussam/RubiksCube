using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cube {
    public class Side : MonoBehaviour
    {
        [SerializeField] private Vector3 rotationAxis;
        public List<Piece> m_pieces { get; private set; }
        [SerializeField] private FaceColor color;
        [SerializeField] private Cube m_cube;
        public FaceColor m_color => color;

        private Vector3 m_rotationVector;
        private float m_targetAngle;
        private float m_lerpSpeed;
        private bool m_startRotation = false;
        private void Start() {
            m_cube = gameObject.GetComponentInParent<Cube>();
            m_pieces = new List<Piece>();
        }

        public void Rotate(bool reverse, float lerpSpeed) {
            m_targetAngle = reverse ? -90 : 90;
            
            m_rotationVector = transform.eulerAngles + rotationAxis * m_targetAngle;
            m_lerpSpeed = lerpSpeed;

            m_startRotation = true;
        }

        private void OnMouseDown() {
            Debug.Log($"Hit : {m_color}");
            m_cube.SelectSide(m_color);
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Piece")) return ;
            if(other.TryGetComponent<Piece>(out Piece piece)){
                foreach(var p in m_pieces){
                    if(p == piece) return ;
                }

                m_pieces.Add(piece);
            }
        }

        private void OnTriggerExit(Collider other) {
            if(other.CompareTag("Piece")) return ;
            if(other.TryGetComponent<Piece>(out Piece piece)){
                m_pieces.Remove(piece);
            }
        }
    }

}   
