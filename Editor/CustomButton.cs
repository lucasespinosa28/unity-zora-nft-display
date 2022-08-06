#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ZoraNft))]
public class CustomButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ZoraNft e = (ZoraNft)target;
        if (GUILayout.Button("Preview"))
        {
            e.PreviewNft();
        }
    }
}
#endif
