using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cube{
    public class Center : Piece
    {
        [SerializeField] private FaceColor m_centerColor;
        public override void Awake() {
            base.Awake();
        }
        protected override void GetFaces(){
            Face[] f = this.gameObject.GetComponentsInChildren<Face>();
            
            m_faces = new List<Face>();
            foreach(var face in f) m_faces.Add(face);
        }
        

    }

}
