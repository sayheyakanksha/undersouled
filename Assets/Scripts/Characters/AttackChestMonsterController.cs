using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackChestMonsterController : MonoBehaviour
{
    private Animator animator;

    void Start() {
        // Get the Animator component attached to the chest monster
        animator = GetComponent<Animator>();
    }

    // Trigger detection
    void OnTriggerEnter(Collider other) {
        // Check if the entering object is tagged as "Mummy"
        if (other.CompareTag("Mummy")) {
            // Trigger the wake-up animation
            animator.SetTrigger("WakeUp");
        }
    }

    void OnTriggerExit(Collider other) {
        // Check if the exiting object is tagged as "Mummy"
        if (other.CompareTag("Mummy")) {
            // Return to idle state when mummy leaves vicinity
            animator.SetTrigger("Idle");
        }
    }
}
