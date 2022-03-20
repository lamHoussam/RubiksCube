using UnityEngine;

namespace Cube{
    public enum FaceColor { Red, Orange, Blue, Green, White, Yellow }
    public class Face : MonoBehaviour
    {
        [SerializeField] private FaceColor color;
        public FaceColor faceColor => color;
    }

}
