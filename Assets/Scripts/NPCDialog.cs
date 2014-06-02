using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class NPCDialog : QIScript {

    [SerializeField] private string questDialog;
    [SerializeField] private string defaultDialog;
    //[SerializeField] private QuestInteraction questInteraction;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            if (PCQuestLog.Interact(questInteraction)) {
                //say quest Dialog
                if (questDialog != "") ScreenTextScript.Display(questDialog);
            } else {
                //say default dialog
                if (defaultDialog != "") ScreenTextScript.Display(defaultDialog);
            }
        }
    }
}
