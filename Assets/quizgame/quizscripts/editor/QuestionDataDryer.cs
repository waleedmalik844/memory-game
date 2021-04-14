using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(Question))]
[CanEditMultipleObjects]
[System.Serializable]
public class QuestionDataDryer : Editor
{

    private Question QuestionInstance => target as Question;
    private ReorderableList questionlist;

    private void OnEnable()
    {
        InitialzeRecordableList(ref questionlist, propertyname: "questionList", listlabel: "Questions list");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        questionlist.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    private void InitialzeRecordableList(ref ReorderableList list, string propertyname, string listlabel)
    {
        list = new ReorderableList(serializedObject, elements: serializedObject.FindProperty(propertyname)
            ,draggable: true, displayHeader: true, displayAddButton: true, displayRemoveButton: true);

        list.onAddCallback = reordableList => QuestionInstance.AddQuestion();

        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, listlabel);

        };
        var l = list ;
        list.drawElementCallback = (Rect rect, int index, bool isActivate, bool isFocused) =>
        {
            var element = l.serializedProperty.GetArrayElementAtIndex(index);

            rect.y += 2;
            EditorGUI.PropertyField(position: new Rect(rect.x, rect.y, width: 300, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("question"), GUIContent.none);

            EditorGUI.PropertyField(position: new Rect(rect.x + 310, rect.y, width: 300, EditorGUIUtility.singleLineHeight),
               element.FindPropertyRelative("isTrue"), GUIContent.none);
        };
    }
}
