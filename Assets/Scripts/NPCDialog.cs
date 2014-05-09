using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCDialog : MonoBehaviour {

	private struct QuestDialog {
		int questID;
		int objectiveID;
		bool checkObjective;
		bool completed;
		string dialog;
		public int QuestID { get {	return this.questID; } }
		public int ObjectiveID { get { return this.objectiveID; } }
		public bool CheckObjective { get { return this.checkObjective; } }
		public bool Completed { get { return this.completed; } }
		public string Dialog { get { return this.dialog; } }

		private QuestDialog(int questID, int objectiveID, bool checkObjective, bool completed, string dialog) {
			this.questID = questID;
			this.objectiveID = objectiveID;
			this.checkObjective = checkObjective;
			this.completed = completed;
			this.dialog = dialog;
		}

		public QuestDialog(int questID, int objectiveID, bool completed, string dialog) : this(questID, objectiveID, true, completed, dialog) {}

		public QuestDialog(int questID, bool completed, string dialog) : this(questID, 0, false, completed, dialog) {}
	}

	private List<QuestDialog> dialogs;

	void Start() {
		dialogs = new List<QuestDialog>();
		//dialogs.Add(new QuestDialog(0, 0, false, false, ""));
	}

	void OnTriggerEnter2D(Collider2D other) {
		//TODO check for player. call dialog method
	}

	void CheckDialogOptions() {
		bool dialogConditionsMet = false;
		if (dialogs.Count > 0) {
			foreach (QuestDialog qd in dialogs) {
				if (qd.CheckObjective) {
					dialogConditionsMet = PCQuestLog.QueryLog(qd.QuestID, qd.ObjectiveID, qd.Completed);
				} else {
					dialogConditionsMet = PCQuestLog.QueryLog(qd.QuestID, qd.Completed);
				}

				if (dialogConditionsMet) {
					//display qd.dialog
					//FIXME remove from dialogs
					break;
				}
			}
		}
	}
}
