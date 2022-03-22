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
        public virtual void Awake(){
            m_cube = gameObject.GetComponentInParent<Cube>();
            SetFaces();
        }
        protected abstract void SetFaces();
    }

}
