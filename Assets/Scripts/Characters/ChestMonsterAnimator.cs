using UnityEngine;

public class ChestMonsterAnimator : MonoBehaviour {

    private Animator animator;

    private enum MonsterState {
        WakeUp = 0,
        Idle = 1,
        Attack = 2
    }

    private MonsterState currentState;

    private float stateChangeInterval = 2.0f; // Time in seconds between state changes
    private float timer = 0.0f;

    private void Start() {
        animator = GetComponent<Animator>();

        currentState = MonsterState.WakeUp;
        animator.SetFloat("State", 0.0f);
    }

    private void Update() {
        timer += Time.deltaTime;

        // Change state at regular intervals
        if (timer >= stateChangeInterval) {
            timer = 0.0f; // Reset timer
            CycleMonsterState(); // Move to the next state
        }
    }

    private void CycleMonsterState() {
        // Cycle through states 0 → 1 → 2 → 0
        currentState = (MonsterState)(((int)currentState + 1) % 3);

        animator.SetFloat("State", (float)currentState); // Update Animator parameter

        Debug.Log($"State updated to: {currentState} ({(float)currentState})");
    }
}
