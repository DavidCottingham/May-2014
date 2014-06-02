using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenTextScript : MonoBehaviour {

    private static Queue<string> messages;
    private static int logLength = 4;
    private int lineSpace = 18;

	void Start() {
        messages = new Queue<string>();
	}

    void OnGUI() {
        //int loopCount = logLength;
        int loopCount = 0;
        foreach (string msg in messages) {
            GUI.Label(new Rect(2, (Screen.height - logLength * lineSpace) + (loopCount++ * lineSpace), 1000, 22), msg); //use --loopCount if showing log in reverse direction
        }
    }

    public static void Display(string message) {
        if (messages.Count >= logLength) {
            while (messages.Count >= logLength) {
                messages.Dequeue();
            }            
        }
        messages.Enqueue(message);
    }
}
