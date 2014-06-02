using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PCQuestLog : MonoBehaviour {

	private static List<Quest> currentQuests;
    private static int doNotCheckQuest = -1;
    [SerializeField] private GameObject questManager;
    private static QuestManager qm;

	void Awake() {
		currentQuests = new List<Quest>();
        qm = questManager.GetComponent<QuestManager>() as QuestManager;
	}

    void Start() {
        StartQuest(0);  //start game with default quest
    }

	public static void StartQuest(int id) {
		Quest quest = qm.GetQuest(id);
        if (quest != null) {
			currentQuests.Add(quest);
            //ScreenTextScript.Display("Quest \"" + quest.Name + "\" started");
		}
	}

	public static void UpdateQuest(int id, int objectiveID) {
		Quest quest = GetQuestFromID(id);
		if (quest != null) {
			bool completed = quest.UpdateObjective(objectiveID);
			if (completed) {
				EndQuest(quest);
			}
		}
	}

	public static void EndQuest(int id) {
        EndQuest(GetQuestFromID(id));
	}

	public static void EndQuest(Quest quest) {
        if (quest != null) {
            currentQuests.Remove(quest);
            //ScreenTextScript.Display("Quest \"" + quest.Name + "\" completed");
        }
	}

	public static bool QueryLog(int questID, int objective, bool completed) {
        if (questID == doNotCheckQuest) {
            return true;
        }

        Quest q = GetQuestFromID(questID);
        if (q != null) { return (completed == q.CheckObjectiveCompleted(objective)); }
		return false;
	}

	public static bool QueryLog(int questID) {
        if (questID == doNotCheckQuest) { return true; }

		Quest q = GetQuestFromID(questID);
        if (q != null) { return true; }
		return false;
	}

    //DEBUG
    public static void PrintQuestLog() {
        foreach (Quest q in currentQuests) {
            print(q.ID + " : " + q.Name);
            if (q.Objectives.Count > 0) {
                foreach (QuestObjective qo in q.Objectives) {
                    print("    " + qo.ID + " " + qo.Name + " " + qo.Completed);
                }
            }
        }
    }

    public static bool Interact(QuestInteraction questInteraction) {
        bool prerequisiteCompleted = false;

        if (questInteraction.UseObjective) {
            //If choosing to check against objectives, send objective ID and whether chosen to check if it's completed
            prerequisiteCompleted = QueryLog(questInteraction.QuestID, questInteraction.ObjectiveID, questInteraction.CheckObjectiveCompeleted);
        } else {
            //If only checking if on quest
            prerequisiteCompleted = QueryLog(questInteraction.QuestID);
        }
        if (prerequisiteCompleted) {
            if (questInteraction.CompleteThis) { //chosen to complete the quest or objective
                if (questInteraction.UseObjective) { //chosen to update objective
                    //if already completed objective, don't update again but return false since don't need to interact anymore (assume no longer on that part of quest)
                    if (GetQuestFromID(questInteraction.QuestID).CheckObjectiveCompleted(questInteraction.ObjectiveToComplete)) { return false; }
                    UpdateQuest(questInteraction.QuestID, questInteraction.ObjectiveToComplete); //second objective ID used here so can check pre-req of one, but update a second
                } else { //not updating objective, instead complete quest
                    if (questInteraction.EnableObject) {
                        //if chosen to activate an object, deactivate it now. this means NPC can only activate or deactivate ONE object per script. Object tied to script, not quest!
                        questInteraction.ObjectToEnable.SetActive(false);
                    }
                    EndQuest(questInteraction.QuestID);
                }
            }
            //if chosen to start a quest ...
            if (questInteraction.StartQuest) {
                //if already on quest, return false since don't need to interact anymore
                if (PlayerIsOnQuest(questInteraction.StartQuestID)) { return false; }
                //if chosen to activate an object, do so now. this means NPC can only activate or deactivate ONE object per script. Object tied to script, not quest!
                if (questInteraction.EnableObject) {
                    questInteraction.ObjectToEnable.SetActive(true);
                }
                StartQuest(questInteraction.StartQuestID);
            }
            return true; //if not returned false already, interaction successful
        }
        return false; //return false if pre-req not complete
    }

    public static bool PlayerIsOnQuest(int id) {
        Quest q = GetQuestFromID(id);
        if (q != null) { return true; }
        return false;
    }

    private static Quest GetQuestFromID(int id) {
        foreach (Quest q in currentQuests) {
            if (q.ID == id) {
                return q;
            }
        }
        return null;
    }

    private int indent = 15;
    private int ySpace = 15;

    void OnGUI() {
        int questCount = 0;
        foreach (Quest q in currentQuests) {
            GUI.Label(new Rect(0, (questCount++ * ySpace), 1000, 22), q.Name);
            GUI.Label(new Rect(indent, (questCount++ * ySpace), 1000, 22), q.Description);
            int objCount = questCount;
            foreach (QuestObjective qo in q.Objectives) {
                GUI.Label(new Rect(indent * 2, (objCount++ * ySpace), 1000, 22), (qo.Completed ? "COMPLETED " : "") + qo.Name + (qo.Goal > 0 ? (" " + qo.CurrentCount + " of " + qo.Goal) : ""));
                if (!qo.Completed) {
                    GUI.Label(new Rect(indent * 3, (objCount++ * ySpace), 1000, 22), qo.Description);
                }
            }
        }
    }
}
