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
        public bool m_rotationLocked { get; set; }
        [SerializeField] private UnityEngine.UI.Image m_selectedPanel;
        [SerializeField] private float m_speed;
        [SerializeField] private AudioClip m_rotationSound;
        private AudioSource m_audioSource;
        private bool m_startScramble;
        private Queue<Side> moves;
        private void Start() {
            m_sides = gameObject.GetComponentsInChildren<Side>();
            m_currentSide = m_sides[0];
            
            MeshRenderer rend = m_currentSide.m_center.gameObject.GetComponentsInChildren<MeshRenderer>()[1];

            m_selectedPanel.color = rend.material.color;
                            
            m_rotationLocked = false;
            m_startScramble = false;
            m_audioSource = GetComponent<AudioSource>();

            moves = new Queue<Side>();
        }
        private void Update() {
            if(m_startScramble){
                Scramble();
            }            
            if(Input.GetKeyDown(KeyCode.Space) && !m_rotationLocked){

                m_currentSide.SetRotate(!Input.GetKey(KeyCode.LeftShift), m_speed);
                m_audioSource.PlayOneShot(m_rotationSound);
            } 
        }

        public void SelectSide(FaceColor fColor){
            if(m_rotationLocked) return;

            
            foreach(var side in m_sides){
                if(side.m_color == fColor){
                    m_currentSide = side;

                    break ;
                }
            }

            MeshRenderer rend = m_currentSide.m_center.gameObject.GetComponentsInChildren<MeshRenderer>()[1];
            m_selectedPanel.color = rend.material.color;

            foreach(var piece in m_currentSide.m_center.m_piecesBeside){
                piece.transform.SetParent(m_currentSide.transform);
            }
        }

        public void SelectSide(Side side){
            if(m_rotationLocked) return;

            m_currentSide = side;
            MeshRenderer rend = m_currentSide.m_center.gameObject.GetComponentsInChildren<MeshRenderer>()[1];
            m_selectedPanel.color = rend.material.color;

            foreach(var piece in m_currentSide.m_center.m_piecesBeside){
                piece.transform.SetParent(m_currentSide.transform);
            }
        }

        public void DeSelectSide(){
            if(m_rotationLocked) return;
            foreach(var piece in m_currentSide.m_center.m_piecesBeside){
                piece.transform.SetParent(this.transform);
            }
        }

        public void SetScramble(){
            if(m_startScramble) return;
            
            moves = new Queue<Side>();

            int numberOfMoves = UnityEngine.Random.Range(20, 21);
            int ind;
            for(int i = 0; i < numberOfMoves; i++){
                ind = UnityEngine.Random.Range(0, m_sides.Length);
                moves.Enqueue(m_sides[ind]);
            }
            
            m_startScramble = true;

            Debug.Log(moves);
        }

        public void Reset(){
            foreach(var side in m_sides){
                foreach(var piece in side.m_center.m_piecesBeside){
                    piece.Reset();
                }

                side.m_center.m_piecesBeside.Clear();
            }

            m_startScramble = false;
            m_rotationLocked = false;

        }

        public void Scramble() {
            if(m_rotationLocked) return ; 

            Side moveSide = moves.Dequeue();
            SelectSide(moveSide);

            m_currentSide.SetRotate(UnityEngine.Random.Range(0, 2) == 0, m_speed);

            m_startScramble = moves.Count > 0;
        }
    }

}

