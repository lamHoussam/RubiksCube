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
        [SerializeField] private int m_minScrambleMoves, m_maxScrambleMoves;
        public AudioClip m_rotationSound;
        public AudioSource m_audioSource { get ; private set ; }
        private bool m_startScramble;
        private Queue<Side> moves;
        [SerializeField] private GameObject cubePrefab;
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
            if(m_startScramble && !m_rotationLocked){
                ApplyScramble();
            }            
            if(Input.GetKeyDown(KeyCode.Space) && !m_rotationLocked){

                m_currentSide.Rotate(!Input.GetKey(KeyCode.LeftShift), m_speed);
            } 
        }

        /// <summary> Selects the side to rotate (needs to be called before rotating) </summary>
        /// <param name="fColor"> Color of side to select </param>
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

        /// <summary> Selects the side to rotate (needs to be called before rotating) </summary>
        /// <param name="side"> Side to select </param>
        public void SelectSide(Side side){
            //if(m_rotationLocked) return;

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

        /// <summary> Initialises all needed components to scramble cube </summary>
        public void Scramble(){
            if(m_startScramble) return;
            
            moves = new Queue<Side>();

            int numberOfMoves = UnityEngine.Random.Range(m_minScrambleMoves, m_maxScrambleMoves);
            int ind;
            for(int i = 0; i < numberOfMoves; i++){
                ind = UnityEngine.Random.Range(0, m_sides.Length);
                moves.Enqueue(m_sides[ind]);
            }
            
            m_startScramble = true;

            Debug.Log(moves);
        }

        /// <summary> Reset cube to initial state </summary>
        public void Reset(){
            
            // Instantiate(cubePrefab, transform.position, transform.rotation);

            // Destroy(this);

            DeSelectSide();

            m_startScramble = false;
            m_rotationLocked = false;
            
            foreach(var side in m_sides){
                foreach(var piece in side.m_center.m_piecesBeside){
                    piece.Reset();
                }
            }
        }

        /// <summary> Apply scramble through time </summary>
        private void ApplyScramble() {
            if(!m_rotationLocked){
                Side moveSide = moves.Dequeue();
                SelectSide(moveSide);

                m_currentSide.Rotate(UnityEngine.Random.Range(0, 2) == 0, m_speed);

                m_startScramble = moves.Count > 0;
            }

            
        }
    }

}

