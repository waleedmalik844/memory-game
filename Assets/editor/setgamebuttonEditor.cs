using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(setGameButton))]
[CanEditMultipleObjects]
[System.Serializable]

public class setgamebuttonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        setGameButton myscript = target as setGameButton;

        switch ( myscript.buttontype)
        {
            case setGameButton.Ebuttontype.pairnumbutton:
             //   myscript.pairNumber = (gamesetting.EPairNumber)EditorGUILayout.EnumPopup(label: "Pair Numbers", myscript.pairNumber);
                break;
           case setGameButton.Ebuttontype.puzzelcategorybtn:
              //  myscript.puzzelcategory = (gamesetting.EpuzzelCategotries)EditorGUILayout.EnumPopup(label: "puzzel category", myscript.puzzelcategory);
                break;


        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
            

    }
    

}
