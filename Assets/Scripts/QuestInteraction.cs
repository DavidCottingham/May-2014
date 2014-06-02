using UnityEngine;
using System.Collections;

[System.Serializable]
public class QuestInteraction {

    [SerializeField] private int questID;
    public int QuestID { get { return questID; } }
    [SerializeField] private bool useObjective;
    public bool UseObjective { get { return useObjective; } }
    [SerializeField] private int objectiveID;
    public int ObjectiveID { get { return objectiveID; } }
    [SerializeField] private bool checkObjectiveCompleted;
    public bool CheckObjectiveCompeleted { get { return checkObjectiveCompleted; } }
    [SerializeField] private bool completeThis;
    public bool CompleteThis { get { return completeThis; } }
    [SerializeField] private int objectiveToComplete;
    public int ObjectiveToComplete { get { return objectiveToComplete; } }
    [SerializeField] private bool startQuest;
    public bool StartQuest { get { return startQuest; } }
    [SerializeField] private int startQuestID;
    public int StartQuestID { get { return startQuestID; } }
    [SerializeField] private bool enableObject;
    public bool EnableObject { get { return enableObject; } }
    [SerializeField] private GameObject objectToEnable;
    public GameObject ObjectToEnable { get { return objectToEnable; } }
}