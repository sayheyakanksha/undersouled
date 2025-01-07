using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]

// Allows to store reusable data for each kitchen object
// No hard coding
public class KitchenObjectSO : ScriptableObject {

    // Stores the prefab reference to be spawned in the game
    public Transform prefab;
    // Stores the UI sprite to represent the object in menus or HUDs
    public Sprite sprite;
    // Stores the name of the object
    public string objectName;

}
