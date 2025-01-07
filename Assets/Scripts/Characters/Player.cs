using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {

    //For singleston 
    public static Player Instance { get; set; }

    public event EventHandler OnPickedSomething;

    public event EventHandler<OnSelectedCounterChangedEventsArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventsArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 5f; // Player moving speed, field is private - only modify within this class but SerializeField for editable field in editor. 
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private bool isWalking; // For Player Animator parametere check
    private bool hitDetected;       // Track if a collision was detected

    private RaycastHit raycastHit;  // Store collision result for reuse
    private Vector3 lastInteractDirection;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one player");
        }
        Instance = this;
    }


    private void Start() {
        // player interacts with a counter, the GameInput_OnInteractAction() method is triggered
        gameInput.OnInteractAction += GameInput_OnInteractAction;     // listening to our event for input press E

        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e) {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null) {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
       if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    private void Update() {
        //HandleMovement();
        PerformCollisionDetectionAndMovement();
        HandleInteractions();
    }

    // Tell animator what the player is doing - iswalking or not? 
    public bool IsWalking() {
        return isWalking;
    }

    // Perform movement logic and detect collisions
    private void PerformCollisionDetectionAndMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float capsuleRadius = 0.7f;
        float capsuleHeight = 2f;
        float moveDistance = moveSpeed * Time.deltaTime;

        Vector3 capsuleStart = transform.position;
        Vector3 capsuleEnd = transform.position + Vector3.up * capsuleHeight;

        // Perform the CapsuleCast to detect obstacles
        hitDetected = Physics.CapsuleCast(
            capsuleStart,
            capsuleEnd,
            capsuleRadius,
            moveDir,
            out raycastHit,
            moveDistance,
            countersLayerMask
        );

        // Handle movement if no obstacles are detected
        if (!hitDetected) {
            transform.position += moveDir * moveDistance;
        }

        // Update walking state for animator
        isWalking = moveDir != Vector3.zero;
        RotateTowardsDirection(moveDir);
    }

    // Detect interactions with counters
    private void HandleInteractions() {
        if (hitDetected && raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {
            // Set the selected counter if it's different from the current one
            if (baseCounter != selectedCounter) {
                SetSelectedCounter(baseCounter);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    // Smoothly rotate the player towards the movement direction
    private void RotateTowardsDirection(Vector3 direction) {
        if (direction != Vector3.zero) {
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * rotateSpeed);
        }
    }

    // tells the SelectedCounterVisual script which counter is currently selected
    private void SetSelectedCounter(BaseCounter selectedCounter) {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventsArgs {
            selectedCounter = selectedCounter
        });

    }

    public Transform GetKitchenObjectFolllowTransform() {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null) {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
