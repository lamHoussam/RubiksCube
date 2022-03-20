using UnityEngine;
using System.Collections.Generic;

namespace Cube{
    public class Corner : Piece
    {
        public override void Awake()
        {
            base.Awake();
        }

        protected override void GetFaces()
        {
            Face[] f = this.gameObject.GetComponentsInChildren<Face>();
            
            m_faces = new List<Face>();
            foreach(var face in f) m_faces.Add(face);
        }
    }

}
