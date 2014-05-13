using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public class Quest {

	[XmlAttribute("ID")]
	public int ID { get; private set; }
	public string Name { get; private set; }
	public List<QuestObjective> Objectives { get; private set; }
	public int XPReward { get; private set; }
	public bool Completed { get; private set; }

	public Quest(int id, string name, List<QuestObjective> objectives, int xpReward) {
		this.ID = id;
		this.Name = name;
		this.Objectives = objectives;
		this.XPReward = xpReward;
	}

	public Quest(int id, string name, int xpReward, params QuestObjective[] objectives) : this(id, name, null, xpReward) {
		this.Objectives = new List<QuestObjective>();
		foreach (QuestObjective qo in objectives) {
			this.Objectives.Add(qo);
		}
	}

	//need default constructor for serialization
	public Quest() : this(-1, "null", null, 0) {}

	public bool UpdateObjective(int id) {
		foreach (QuestObjective qo in Objectives) {
			if (qo.ID == id) {
				qo.IncrementObjective();
				break;
			}
		}
		return CheckCompleted();
	}

	private bool CheckCompleted() {
		foreach (QuestObjective qo in Objectives) {
			if (!qo.Completed) {
				if (Completed) { Completed = false; }
				return Completed;
			}
		}
		Completed = true;
		return Completed;
	}

	public bool CheckObjective(int id) {
		foreach (QuestObjective qo in Objectives) {
			if (qo.ID == id) {
				return qo.Completed;
			}
		}
		return false;
	}
}