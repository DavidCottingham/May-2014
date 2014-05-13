using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(QuestManager))]
public class QuestAddEditor : Editor {

	QuestManager qm;
	//Quest quest;

	private int questID;
	private string questName;
	private int questReward;

	void Start() {}

	public override void OnInspectorGUI() {
		qm = (QuestManager) target;

		EditorGUILayout.LabelField("Quests");
		QuestInput();
		if (GUILayout.Button("OK")) {
			qm.AddQuest(new Quest(questID, questName, null, questReward));
			QuestInput();
		}
		if (GUILayout.Button("Save to File")) {
			qm.Save();
		}
	}

	private void QuestInput() {
		//EditorGUI.BeginChangeCheck();
		questID = EditorGUILayout.IntField("Quest ID", questID);
		questName = EditorGUILayout.TextField("Name", questName);
		questReward = EditorGUILayout.IntField("XP Reward", questReward);
		//EditorGUI.EndChangeCheck();
	}
}
