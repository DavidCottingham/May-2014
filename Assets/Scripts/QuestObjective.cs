using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class QuestObjective {

	[XmlAttribute("ID")]
	public int ID { get; private set; }
	public string Name { get; private set; }
	public string Description { get; private set; }
	public int Goal { get; private set; }
	public int CurrentCount { get; private set; }
	public bool Completed { get; private set; }
	
	public QuestObjective (int id, string name, string desc, int goalAmount) {
		this.ID = id;
		this.Name = name;
		this.Description = desc;
		this.Goal = goalAmount;
		this.Completed = false;
	}
	
	public QuestObjective (int id, string name, string desc) : this(id, name, desc, 0) {}

	//need default constructor for serialization
	public QuestObjective() : this(-1, "null", "null") {}
	
	public bool IncrementObjective() {
		if (++CurrentCount >= Goal) { Completed = true; }
		return Completed;
	}
}