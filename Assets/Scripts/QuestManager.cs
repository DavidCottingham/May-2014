using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public class QuestManager : MonoBehaviour {

	[SerializeField] private List<Quest> quests = new List<Quest>();
	public List<Quest> Quests { get { return quests; } }
	private XmlSerializer serializer = new XmlSerializer(typeof(List<Quest>));
	//non-running location?
	private string path = "Assets/quests.xml";
	private string playerQuestLogPath;

	void Awake() {
		//quests = new List<Quest>();
		//serializer = new XmlSerializer(typeof(List<Quest>));
		path = Path.Combine(Application.dataPath, "quests.xml");
		playerQuestLogPath = Path.Combine(Application.persistentDataPath, "quests.xml");
	}

	void Start() {
		//LoadOrCreate();
	}

	public void Save() {
		//using statement ensure correct handling of IDisposable objects
		using (FileStream stream = new FileStream(path, FileMode.Create)) {
			serializer.Serialize(stream, quests);
		}
		print("Saving file to " + path);
	}
	
	public void Save(Quest quest) {
		quests.Add(quest);
		Save();
	}
	
	public void AddQuest(Quest quest) {
		quests.Add(quest);
	}

	public void LoadOrCreate() {
		if (File.Exists(path)) {
			try {
				//using statement ensure correct handling of IDisposable objects
				using (FileStream stream = new FileStream(path, FileMode.Open)) {
					quests = serializer.Deserialize(stream) as List<Quest>;
				}
				print("Loading file from " + path);
			} catch(System.Exception exc) {
				Debug.LogException(exc);
			}
		} else {
			//DEBUG_OUT
			//Debug.LogError("Quests Not Found! Creating empty Quests file");
			Debug.LogError("Quests Not Found!");
			//Save();
		}
	}
}
