using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private float moveSpeed = 5f;

	void Update() {
		if (Input.GetButton("Up")) {
			gameObject.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
		} 
		if (Input.GetButton("Down")) {
			gameObject.transform.Translate(-Vector3.up * moveSpeed * Time.deltaTime);
		}
		if (Input.GetButton("Left")) {
			gameObject.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
		}
		if (Input.GetButton("Right")) {
			gameObject.transform.Translate(-Vector3.left * moveSpeed * Time.deltaTime);
		}

        if (Input.GetButtonDown("Quest Log")) {
            PCQuestLog.PrintQuestLog();
        }
        if (Input.GetButtonDown("Quit")) {
            Application.Quit();
        }
	}
}
