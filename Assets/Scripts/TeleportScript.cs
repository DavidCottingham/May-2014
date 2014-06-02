using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class TeleportScript : MonoBehaviour {

    [SerializeField] private Vector3 toLocation;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            other.transform.position = toLocation;
        }
    }
}
