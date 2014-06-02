using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class ItemScript : QIScript {

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            PCQuestLog.Interact(questInteraction);
            //Pickup/Destroy whether prereq or not?
            Destroy(gameObject);
        }
    }
}
