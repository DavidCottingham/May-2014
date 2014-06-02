using UnityEngine;
using System.Collections;

public class GameEnderScript : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Application.Quit();
        }
    }
}
