using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Methods for interacting with kitchen objects
public interface IKitchenObjectParent {

    public Transform GetKitchenObjectFolllowTransform();

    public void SetKitchenObject(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();

    public void ClearKitchenObject();

    public bool HasKitchenObject();
}
