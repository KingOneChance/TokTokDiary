using TMPro;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Refer to it https://m.blog.naver.com/cra2yboy/222147589437
/// </summary>
public class FontChanger : EditorWindow
{
    TMP_FontAsset selectedFont;

    [MenuItem("GameUtility/Font Changer")]
    private static void ShowWindow()
    {
        FontChanger w = GetWindow<FontChanger>(false, "UI Font Changer", true);
        w.minSize = new Vector2(200, 110);
        w.maxSize = new Vector2(200, 110);
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Font: ", selectedFont?.name);

        SelectFontButton();
        OnSelectorClosed();

        ResetFontButton();
        ChangeAllFontsButton();
    }

    private void SelectFontButton()
    {
        EditorGUILayout.Space();
        if (GUILayout.Button("Select Font"))
        {
            EditorGUIUtility.ShowObjectPicker<TMP_FontAsset>(selectedFont, true, "", GUIUtility.GetControlID(FocusType.Passive) + 100);
        }
    }

    private void OnSelectorClosed()
    {
        if (Event.current.commandName == "ObjectSelectorClosed")
        {
            if (EditorGUIUtility.GetObjectPickerObject() != null)
            {
                selectedFont = (TMP_FontAsset)EditorGUIUtility.GetObjectPickerObject();
            }
        }
    }

    private void ResetFontButton()
    {
        EditorGUILayout.Space();

        if (GUILayout.Button("Reset Font"))
        {
            selectedFont = Resources.GetBuiltinResource(typeof(TMP_FontAsset), "Maplestory Bold SDF.asset") as TMP_FontAsset;
        }
    }

    private void ChangeAllFontsButton()
    {
        EditorGUILayout.Space();

        if (GUILayout.Button("Change All Fonts In Scene"))
        {
            ChangeAllFonts();
            SceneView.lastActiveSceneView.Repaint();
            UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
        }
    }

    public void ChangeAllFonts()
    {
        var allTextObjects = Resources.FindObjectsOfTypeAll(typeof(TextMeshProUGUI));
        foreach (TextMeshProUGUI t in allTextObjects)
        {
            if (t.font != selectedFont)
            {
                t.font = selectedFont;
            }
        }
    }
}
