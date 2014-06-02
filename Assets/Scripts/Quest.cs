using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

[System.Serializable]
public class Quest {
	//using fields instead of auto-props so can serialize the fields to inspector
	private static int questCount = 0;
	[SerializeField] private int id = questCount;
	[XmlAttribute("ID")] public int ID { get { return id; } }
	[SerializeField] private string name;
	public string Name { get { return name; } }
	[SerializeField] private string description;
	public string Description { get { return description; } }
	[SerializeField] private int xpReward;
	public int XPReward { get { return xpReward; } }
	public bool Completed { get; private set; }
	[SerializeField] private List<QuestObjective> objectives;
	public List<QuestObjective> Objectives { get { return objectives; } } //public read-only access is easiest way to interact. fairly safe since read-only. is there a better or more intuitive way though?

	public Quest(string name, List<QuestObjective> objectives, int xpReward) {
		this.id = questCount++;
		this.name = name;
		this.objectives = objectives;
		this.xpReward = xpReward;
	}

	public Quest(string name, int xpReward, params QuestObjective[] objectives) : this(name, null, xpReward) {
		this.objectives = new List<QuestObjective>();
		foreach (QuestObjective qo in objectives) {
			this.objectives.Add(qo);
		}
	}

	//need default constructor for XML serialization
	public Quest() : this("", null, 0) {}

	public bool UpdateObjective(int id) {
		foreach (QuestObjective qo in objectives) {
			if (qo.ID == id) {
				qo.IncrementObjective();
				break;
			}
		}
		return CheckQuestCompleted();
	}

	private bool CheckQuestCompleted() {
		foreach (QuestObjective qo in objectives) {
			if (!qo.Completed) {
				if (Completed) { Completed = false; }
				return Completed;
			}
		}
		Completed = true;
		return Completed;
	}

	public bool CheckObjectiveCompleted(int id) {
		foreach (QuestObjective qo in objectives) {
			if (qo.ID == id) {
				return qo.Completed;
			}
		}
		return false;
	}
}