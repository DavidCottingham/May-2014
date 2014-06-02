using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QIScript), true)]
//[CanEditMultipleObjects]
public class QuestInteractionEditor : Editor {

    /*
    [SerializeField] private int questID;
    [SerializeField] private bool useObjective;
    [SerializeField] private int objectiveID;
    [SerializeField] private bool checkObjectiveCompleted;
    [SerializeField] private bool completeThis;
    [SerializeField] private int objectiveToComplete;
    [SerializeField] private bool startQuest;
    [SerializeField] private int startQuestID;
    [SerializeField] private bool enableObject;
    [SerializeField] private GameObject objectToEnable;
     */

    SerializedProperty qi;
    bool useObj, completeThis, start, enable;

    GUIContent completeQuest = new GUIContent("Complete Quest"),
        completeObjective = new GUIContent("Update Objective"),
        prereqID = new GUIContent("Prerequisite Quest"),
        checkComplete = new GUIContent("Check Completed"),
        objectLabel = new GUIContent("Object"),
        startID = new GUIContent("Quest ID"),
        objectiveID = new GUIContent("Objective ID");

    public override void OnInspectorGUI() {
        serializedObject.Update();
        qi = serializedObject.FindProperty("questInteraction");
        EditorGUILayout.PropertyField(qi.FindPropertyRelative("questID"), prereqID);
        EditorGUILayout.PropertyField(qi.FindPropertyRelative("useObjective"));
        useObj = qi.FindPropertyRelative("useObjective").boolValue;
        if (useObj) {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(qi.FindPropertyRelative("objectiveID"));
            EditorGUILayout.PropertyField(qi.FindPropertyRelative("checkObjectiveCompleted"), checkComplete);
            EditorGUILayout.PropertyField(qi.FindPropertyRelative("completeThis"), completeObjective);
            completeThis = qi.FindPropertyRelative("completeThis").boolValue;
            if (completeThis) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(qi.FindPropertyRelative("objectiveToComplete"), objectiveID);
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
        } else {
            EditorGUILayout.PropertyField(qi.FindPropertyRelative("completeThis"), completeQuest);
        }
        EditorGUILayout.PropertyField(qi.FindPropertyRelative("startQuest"));
        start = qi.FindPropertyRelative("startQuest").boolValue;
        if (start) {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(qi.FindPropertyRelative("startQuestID"), startID);
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.PropertyField(qi.FindPropertyRelative("enableObject"));
        enable = qi.FindPropertyRelative("enableObject").boolValue;
        if (enable) {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(qi.FindPropertyRelative("objectToEnable"), objectLabel);
            EditorGUI.indentLevel--;
        }        

        try {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("questDialog"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultDialog"));
        } catch (System.NullReferenceException) { }

        serializedObject.ApplyModifiedProperties();
    }
}
