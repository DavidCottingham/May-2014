using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PCQuestLog : MonoBehaviour {

	private static List<Quest> currentQuests;
	//public static List<Quest> CurrentQuests { get { return currentQuests; } }

	void Awake() {
		currentQuests = new List<Quest>();
	}

	public static void StartQuest(int id) {
		foreach (Quest quest in currentQuests) {
			if (quest.ID == id) {
				currentQuests.Add(quest);
				print("Quest " + quest.Name + " started"); //DEBUG
				break;
			}
		}
	}

	public static void UpdateQuest(int id, int objective) {
		foreach (Quest quest in currentQuests) {
			if (quest.ID == id) {
				bool completed = quest.UpdateObjective(objective);
				if (completed) {
					EndQuest(quest);
				}
				break;
			}
		}
	}

	public static void EndQuest(int id) {
		foreach (Quest quest in currentQuests) {
			if (quest.ID == id) {
				EndQuest(quest);
				break;
			}
		}
	}

	public static void EndQuest(Quest quest) {
		currentQuests.Remove(quest);
	}

	public static bool QueryLog(int questID, int objective, bool completed) {
		foreach (Quest q in currentQuests) {
			if (q.ID == questID) {
				return (completed == q.CheckObjective(objective));
			}
		}
		return false;
	}

	public static bool QueryLog(int questID, bool completed) {
		foreach (Quest q in currentQuests) {
			if (q.ID == questID) {
				return (completed == q.Completed);
			}
		}
		return false;
	}
}
