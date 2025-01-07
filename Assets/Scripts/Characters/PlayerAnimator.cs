using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    private const string IS_WALKING = "IsWalking";

    // For getting player movement for reference
    [SerializeField] private Player player;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();     
    }

    private void Update() {
        // Making sure to check player is walking every frame
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}