using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SpriteAnimation))]
public class SpriteAnimationEditor : Editor
{
    public double time;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BaseSpriteAnimation baseAnimator = (BaseSpriteAnimation)target;

        if (GUILayout.Button("Play"))
        {
            baseAnimator.Play(baseAnimator.Loop);
        }
        baseAnimator.UpdateFrame((float)( EditorApplication.timeSinceStartup - time)*3.5f);
        EditorUtility.SetDirty(baseAnimator);

        UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
        time = EditorApplication.timeSinceStartup;
    }


}

[CustomEditor(typeof(ImageUISpriteAnimation))]
public class ImageUISpriteAnimationEditor : SpriteAnimationEditor
{
}