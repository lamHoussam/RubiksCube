using UnityEditor;

namespace Cube{
    [CustomEditor(typeof(Center))]
    public class CenterEditor : Editor {

        private SerializedProperty spCenterColor;
        private void OnEnable(){
            spCenterColor = serializedObject.FindProperty("m_centerColor");
        }
        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.PropertyField(spCenterColor);

            serializedObject.ApplyModifiedProperties();
        }

        private void OnDestroy() {
            spCenterColor = null;
        }
    }

}
