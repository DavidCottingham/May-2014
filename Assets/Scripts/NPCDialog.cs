using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCDialog : MonoBehaviour {

	private struct QuestDialog {
		public int QuestID { get; private set; }
		public int ObjectiveID { get; private set; }
		public bool CheckObjective { get; private set; }
		public bool Completed { get; private set; }
		public string Dialog { get; private set; }

		private QuestDialog(int questID, int objectiveID, bool checkObjective, bool completed, string dialog) {
			this.QuestID = questID;
			this.ObjectiveID = objectiveID;
			this.CheckObjective = checkObjective;
			this.Completed = completed;
			this.Dialog = dialog;
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
