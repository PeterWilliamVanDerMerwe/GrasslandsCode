using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour, IInteractable
{
    /// <summary>
    /// This interaction will apply an upwards velocity to the game object interacting with it allowing that game object to
    /// get to higher areas if needed.
    /// </summary>
    public void Interact()
    {
        Vector3 movement = transform.up * 4.0f;
        PlayerController.playerRigidBody.velocity = movement;
    }
}
