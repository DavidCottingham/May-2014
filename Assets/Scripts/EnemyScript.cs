using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class EnemyScript : QIScript {

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            PCQuestLog.Interact(questInteraction);
        }
        Destroy(gameObject);
    }
}
