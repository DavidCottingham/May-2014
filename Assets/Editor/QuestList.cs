using UnityEngine;
using UnityEditor;

public static class QuestList {

	private static SerializedProperty objectives;

	private static GUIContent
		moveButtonContent = new GUIContent("\u21b4", "move down"),
		duplicateButtonContent = new GUIContent("+", "duplicate"),
		deleteButtonContent = new GUIContent("-", "delete"),
		addQuestButtonContent = new GUIContent("+ Quest", "add element"),
		addObjectiveButtonContent = new GUIContent("+ Objective", "add element"),
		saveFileButtonContent = new GUIContent("Save To File", "Save quests to file"),
		loadFileButtonContent = new GUIContent("Refresh/Load File", "Load from file"),
		idLabel = new GUIContent("ID"),
		nameLabel = new GUIContent("Name"),
		xpRewardLabel = new GUIContent("XP Reward"),
		objectivesLabel = new GUIContent("Objectives"),
		descriptionLabel = new GUIContent("Description"),
		goalLabel = new GUIContent("Goal #");

	private static GUILayoutOption
		miniButtonWidth = GUILayout.Width(25f),
		largerButtonWidth = GUILayout.Width(75f);

	public static void Show(SerializedProperty list, QuestManager qm) {
		if (!list.isArray) {
			EditorGUILayout.HelpBox(list.name + " is neither an array nor a list!", MessageType.Error);
			return;
		}

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if (list.arraySize == 0 && GUILayout.Button(addQuestButtonContent, EditorStyles.miniButton, largerButtonWidth)) {
			list.arraySize += 1;
		}
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		if (list.isExpanded) {
			EditorGUI.indentLevel += 1;
			for (int i = 0; i < list.arraySize; ++i) {
				EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new GUIContent("Quest"));
				if (list.GetArrayElementAtIndex(i).isExpanded) {
					EditorGUI.indentLevel += 1;
					//EditorGUILayout.LabelField("ID", list.GetArrayElementAtIndex(i).FindPropertyRelative("id").intValue.ToString(), EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).label);
					//EditorGUILayout.LabelField("ID", QuestID(qm, i).ToString(), EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).label);
					EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("id"), idLabel);
					list.GetArrayElementAtIndex(i).FindPropertyRelative("id").intValue = QuestID(qm, i);
					EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("name"), nameLabel);
					EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("xpReward"), xpRewardLabel);
					objectives = list.GetArrayElementAtIndex(i).FindPropertyRelative("objectives");
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.PropertyField(objectives, objectivesLabel);
					if (objectives.arraySize == 0 && GUILayout.Button(addObjectiveButtonContent, EditorStyles.miniButton, largerButtonWidth)) {
						objectives.arraySize += 1;
						if (!objectives.isExpanded) objectives.isExpanded = true;
					}
					EditorGUILayout.EndHorizontal();
					if (objectives.isExpanded) {
						EditorGUI.indentLevel += 1;
						for (int j = 0; j < objectives.arraySize; ++j) {
							//EditorGUILayout.LabelField("ID", list.GetArrayElementAtIndex(j).FindPropertyRelative("id").intValue.ToString(), EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).label);
							EditorGUILayout.PropertyField(objectives.GetArrayElementAtIndex(j).FindPropertyRelative("id"), idLabel);
							objectives.GetArrayElementAtIndex(j).FindPropertyRelative("id").intValue = ObjectiveID(qm, i, j);
							EditorGUILayout.PropertyField(objectives.GetArrayElementAtIndex(j).FindPropertyRelative("name"), nameLabel);
							EditorGUILayout.PropertyField(objectives.GetArrayElementAtIndex(j).FindPropertyRelative("description"), descriptionLabel);
							EditorGUILayout.PropertyField(objectives.GetArrayElementAtIndex(j).FindPropertyRelative("goal"), goalLabel);
							ShowButtons(objectives, j, true);
						}
						EditorGUI.indentLevel -= 1;
					}
					ShowButtons(list, i, false);
					EditorGUI.indentLevel -= 1;
				}
			}
			EditorGUI.indentLevel -= 1;
		}

		if (GUILayout.Button(saveFileButtonContent)) {
			qm.Save();
		}

		if (GUILayout.Button(loadFileButtonContent)) {
			qm.LoadOrCreate();
		}

		if (GUILayout.Button("PRINT")) {
			foreach(Quest q in qm.Quests) {
				Debug.Log(q.objectives.Count);
			}
		}
	}

	private static void ShowButtons(SerializedProperty list, int index, bool center) {
		EditorGUILayout.BeginHorizontal();
		if (center) GUILayout.FlexibleSpace();
		if (GUILayout.Button(moveButtonContent, EditorStyles.miniButtonLeft, miniButtonWidth)) {
			list.MoveArrayElement(index, index + 1);
		}
		if (GUILayout.Button(duplicateButtonContent, EditorStyles.miniButtonMid, miniButtonWidth)) {
			list.InsertArrayElementAtIndex(index);
		}
		if (GUILayout.Button(deleteButtonContent, EditorStyles.miniButtonRight, miniButtonWidth)) {
			int oldSize = list.arraySize;
			list.DeleteArrayElementAtIndex(index);
			if (list.arraySize == oldSize) {
				list.DeleteArrayElementAtIndex(index);
			}
		}
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
	}

	private static int QuestID(QuestManager qm, int index) {
		if (qm.Quests.Count <= index) return 0;
		else return index;
	}

	private static int ObjectiveID(QuestManager qm, int questIndex, int objectiveIndex) {
		if (qm.Quests.Count <= questIndex || qm.Quests[questIndex].objectives.Count <= objectiveIndex) return 0;
		else return objectiveIndex;
	}
}
