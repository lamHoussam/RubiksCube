using UnityEditor;

namespace Cube{
    [CustomEditor(typeof(Face))]
    public class FaceEditor : Editor 
    {
        private SerializedProperty spColor;
        private void OnEnable(){
            spColor = serializedObject.FindProperty("color");
        }
        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.PropertyField(spColor);

            serializedObject.ApplyModifiedProperties();
        }

        private void OnDestroy() {
            spColor = null;
        }
    }

}
