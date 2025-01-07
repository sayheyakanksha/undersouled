using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawn and pick up new object
public class ContainerCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            // Player is not carrying anything

            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            //Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            //kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        }

    }
}   