using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
[CustomEditor(typeof(GuageBar), true)]
[CanEditMultipleObjects]
public class GuageBarEditor : UnityEditor.UI.ImageEditor {

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        GuageBar guageBar = this.target as GuageBar;
        if (guageBar == null) { return; }

        guageBar.type = Image.Type.Sliced;
        float oldFillAmount = guageBar.fillAmount;
        guageBar.fillAmount = EditorGUILayout.Slider(guageBar.fillAmount, 0.0f, 1.0f);
        if (oldFillAmount != guageBar.fillAmount)
        {
            EditorUtility.SetDirty(guageBar);
        }
    }
}
