using UnityEngine;
using System.Collections;

public class QuestObjective {
	
	private int id;
	public int ID { get { return id; } }
	private string name;
	public string Name { get { return name; } }
	private string desc;
	public string Description { get { return desc; } }
	private int goalAmount;
	public int Goal { get { return goalAmount; } }
	private int currAmount;
	public int CurrentCount { get { return currAmount; } }
	private bool completed = false;
	public bool Completed { get { return completed; } }
	
	public QuestObjective (int id, string name, string desc, int goalAmount) {
		this.id = 0;
		this.name = name;
		this.desc = desc;
		this.goalAmount = goalAmount;
	}
	
	public QuestObjective (int id, string name, string desc) : this(id, name, desc, 0) {}
	
	public bool IncrementObjective() {
		if (++currAmount >= goalAmount) { completed = true; }
		return completed;
	}
}