using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

[System.Serializable]
public class QuestObjective {
	//using fields instead of auto-props so can serialize the fields to inspector
	/*[SerializeField] private int id;
	[XmlAttribute("ID")] public int ID { get { return id; } }
	[SerializeField] private string name;
	public string Name { get { return name; } }
	[SerializeField] private string description;
	public string Description { get { return description; } }
	[SerializeField] private int goal;
	public int Goal { get { return goal; } }*/
	public int CurrentCount { get; private set; }
	public bool Completed { get; private set; }
	public int id;
	public string name;
	public string description;
	public int goal;
	
	public QuestObjective (int id, string name, string desc, int goalAmount) {
		this.id = id;
		this.name = name;
		this.description = desc;
		this.goal = goalAmount;
		this.Completed = false;
	}
	
	public QuestObjective (int id, string name, string desc) : this(id, name, desc, 0) {}

	//need default constructor for XML serialization
	public QuestObjective() : this(-1, "null", "null") {}
	
	public bool IncrementObjective() {
		if (++CurrentCount >= goal) { Completed = true; }
		return Completed;
	}
}