using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Discard items
public class TrashCounter : BaseCounter {

    public static event EventHandler OnAnyObjectTrashed;

    new public static void ResetStaticData() {
        OnAnyObjectTrashed = null;
    }


    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            // Throw the trash
            //Debug.Log("again");
            player.GetKitchenObject().DestroySelf();

            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}