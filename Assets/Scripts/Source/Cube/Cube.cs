using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cube
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private Transform m_centers, m_corners, m_edges;
        [SerializeField] private Side[] m_sides;
        [SerializeField] private Side m_currentSide;
        private void Start() {
            m_sides = gameObject.GetComponentsInChildren<Side>();
            m_currentSide = m_sides[0];
        }
        private void Update() {
            if(Input.GetKey(KeyCode.Space)){
                m_currentSide.Rotate(false, 2f);
            } 
            if(Input.GetKeyUp(KeyCode.Space))
                DeSelectSide();
        }

        public void SelectSide(FaceColor fColor){
            foreach(var side in m_sides){
                if(side.m_color == fColor){
                    m_currentSide = side;

                    break ;
                }
            }

            foreach(var piece in m_currentSide.m_pieces){
                piece.transform.SetParent(m_currentSide.transform);
            }
        }

        public void DeSelectSide(){
            foreach(var piece in m_currentSide.m_pieces){
                piece.transform.SetParent(this.transform);
            }
        }

        
    }

}