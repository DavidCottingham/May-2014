using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuestManager))]
public class QuestAddEditor : Editor {

	QuestManager qm;

	public override void OnInspectorGUI() {
		qm = (QuestManager) target;
		serializedObject.Update();
		QuestList.Show(serializedObject.FindProperty("quests"), qm);
		serializedObject.ApplyModifiedProperties();
	}
}