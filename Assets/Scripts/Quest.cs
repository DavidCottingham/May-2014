using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

[System.Serializable]
public class Quest {
	//using fields instead of auto-props so can serialize the fields to inspector
	//empty setter on properties needed for XML serialization
	[XmlAttribute("ID")] public int id = 0;
	public string name;
	public string description;
	public int xpReward;
	public bool Completed { get; private set; }
	public List<QuestObjective> objectives;

	public Quest(int id, string name, List<QuestObjective> objectives, int xpReward) {
		this.id = id;
		this.name = name;
		this.objectives = objectives;
		this.xpReward = xpReward;
	}

	/*public Quest(int id, string name, int xpReward, params QuestObjective[] objectives) : this(id, name, null, xpReward) {
		this.objectives = new List<QuestObjective>();
		foreach (QuestObjective qo in objectives) {
			this.objectives.Add(qo);
		}
	}*/

	//need default constructor for XML serialization
	public Quest() {}

	public bool UpdateObjective(int id) {
		foreach (QuestObjective qo in objectives) {
			if (qo.id == id) {
				qo.IncrementObjective();
				break;
			}
		}
		return CheckCompleted();
	}

	private bool CheckCompleted() {
		foreach (QuestObjective qo in objectives) {
			if (!qo.Completed) {
				if (Completed) { Completed = false; }
				return Completed;
			}
		}
		Completed = true;
		return Completed;
	}

	public bool CheckObjective(int id) {
		foreach (QuestObjective qo in objectives) {
			if (qo.id == id) {
				return qo.Completed;
			}
		}
		return false;
	}
}