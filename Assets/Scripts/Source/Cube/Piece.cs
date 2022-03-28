using System.Collections.Generic;
using UnityEngine;

namespace Cube{

    public abstract class Piece : MonoBehaviour
    {
        [System.Serializable]
        public class Position{
            public float x{ get; set; }
            public float y{ get; set; }
        }
        public List<Face> m_faces{ get; protected set; }
        public Cube m_cube { get; private set; }
        public Vector3 m_initialPosition { get; private set; }
        public Vector3 m_initialRotation { get; private set; }
        public Transform m_initialParent { get; private set; }
        private Rigidbody rb;
        public virtual void Awake(){

            m_initialParent = transform.parent;
            rb = GetComponent<Rigidbody>();
            m_cube = gameObject.GetComponentInParent<Cube>();
            SetFaces();

            m_initialPosition = transform.position;
            m_initialRotation = transform.eulerAngles;
        }

        /// <summary> Get all faces of a piece </summary>
        protected void SetFaces(){
            Face[] f = this.gameObject.GetComponentsInChildren<Face>();
            
            m_faces = new List<Face>();
            foreach(var face in f) m_faces.Add(face);
        }

        /// <summary> Reset piece's position </summary>
        public void Reset(){

            transform.SetParent(m_initialParent);

            transform.SetPositionAndRotation(m_initialPosition, Quaternion.Euler(m_initialRotation));

            // rb.MovePosition(m_initialPosition);
            // rb.MoveRotation(Quaternion.Euler(m_initialRotation));
        }
    }

}
