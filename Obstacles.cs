using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class Obstacles : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip PickUpSound;

    LevelManager levelManager;

    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    /// <summary>
    /// This interaction method is used for all obstacles and handles the picking up of obstacles by setting the obstacle to a child of the player,
    /// Placing the object above the head of the player with the correct orientation,
    /// Telling the player controller that the player is holding an item,
    /// Instantiating the ghost object of the held item,
    /// And playing the pick up sound of the object picked up.
    /// </summary>
    public void Interact()
    {
        GameObject parent = FindObjectOfType<PlayerController>().gameObject;
        PlayerController controller = parent.GetComponent<PlayerController>();
        GhostForDropping ghostForDropping = parent.GetComponent<GhostForDropping>();

        if (!controller.isHoldingItem && !levelManager.isLevelStarted)
        {
            transform.SetParent(parent.transform);
            transform.rotation = parent.transform.rotation;
            transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y + 1.0f, parent.transform.position.z);
            controller.isHoldingItem = true;
            controller.heldItem = this.gameObject;
            ghostForDropping.InstantiateGhostObject();
            AudioSource PickUpSoundSource = GetComponent<AudioSource>();
            PickUpSoundSource.clip = PickUpSound;
            PickUpSoundSource.Play();
        }
    }
}
