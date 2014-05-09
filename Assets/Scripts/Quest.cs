using System.Collections;
using System.Collections.Generic;

public class Quest {

	private int id;
	public int ID { get { return id; } }
	private string name;
	public string Name { get { return name; } }
	private List<QuestObjective> objectives;
	private int xpReward;
	public int XPReward { get { return xpReward; } }
	private bool completed;
	public bool Completed { get { return completed; } }

	public Quest(int id, string name, List<QuestObjective> objectives, int xpReward) {
		this.id = id;
		this.name = name;
		this.objectives = objectives;
		this.xpReward = xpReward;
	}

	public Quest(int id, string name, int xpReward, params QuestObjective[] objectives) : this(id, name, null, xpReward) {
		this.objectives = new List<QuestObjective>();
		foreach (QuestObjective qo in objectives) {
			this.objectives.Add(qo);
		}
	}

	public bool UpdateObjective(int id) {
		foreach (QuestObjective qo in objectives) {
			if (qo.ID == id) {
				qo.IncrementObjective();
				break;
			}
		}
		return CheckCompleted();
	}

	private bool CheckCompleted() {
		foreach (QuestObjective qo in objectives) {
			if (!qo.Completed) {
				if (completed) { completed = false; }
				return completed;
			}
		}
		completed = true;
		return completed;
	}

	public bool CheckObjective(int id) {
		foreach (QuestObjective qo in objectives) {
			if (qo.ID == id) {
				return qo.Completed;
			}
		}
		return false;
	}
}