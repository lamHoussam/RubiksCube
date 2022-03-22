using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cube{
    public class Center : Piece
    {
        [SerializeField] private FaceColor m_centerColor;
        public List<Piece> m_piecesBeside { get; private set; }
        public override void Awake() {
            base.Awake();

            m_piecesBeside = new List<Piece>();
            m_piecesBeside.Add(this);
        }
        protected override void SetFaces(){
            Face[] f = this.gameObject.GetComponentsInChildren<Face>();
            
            m_faces = new List<Face>();
            foreach(var face in f) m_faces.Add(face);
        }
        
        private void OnTriggerEnter(Collider other) {
            
            if(other.TryGetComponent<Piece>(out Piece piece)){
                foreach(var p in m_piecesBeside){
                    if(p == piece) return ;
                }

                m_piecesBeside.Add(piece);
            }
        }

        private void OnTriggerExit(Collider other) {

            if(other.TryGetComponent<Piece>(out Piece piece)){
                m_piecesBeside.Remove(piece);
            }
        }

    }

}
