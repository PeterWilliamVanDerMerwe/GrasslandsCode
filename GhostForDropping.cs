using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class GhostForDropping : MonoBehaviour
{
    [SerializeField] private Material ghostMaterial;

    private PlayerController playerController;
    private GameObject gameObjectForGhost;
    private GameObject ghostObject;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        GhostTransform();
    }

    /// <summary>
    /// This method checks for the item that the player is currently holding and executes the necessary methods
    /// in order to create a blue ghost of the same object in front of the player so that a player knows the exact location 
    /// of where an object will be dropped.
    /// </summary>
    public void InstantiateGhostObject()
    {
        gameObjectForGhost = playerController.heldItem;
        ghostObject = Instantiate(gameObjectForGhost);
        DeactivateColliders();
        SetGhostMaterial();
    }

    /// <summary>
    /// This method gets all the meshrenderer components of the ghost object and sets them to the assigned ghost material.
    /// </summary>
    private void SetGhostMaterial()
    {
        Renderer[] renderers = ghostObject.GetComponentsInChildren<MeshRenderer>();

        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;

            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = ghostMaterial;
            }
            renderer.materials = materials;
        }
    }

    /// <summary>
    /// This method turns off all colliders of the current ghost object so that the ghost object does not 
    /// interact with the world at all.
    /// </summary>
    private void DeactivateColliders()
    {
        Collider[] colliders = ghostObject.GetComponentsInChildren<Collider>();

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
    }

    /// <summary>
    /// This method is responsible for placing the ghost of the held item at the correct position by making use of the 
    /// rayCastForDropping method from the player controller.
    /// </summary>
    private void GhostTransform()
    {
        if (playerController.isHoldingItem)
        {
            RaycastHit Hit = playerController.rayCastForDropping();
            ghostObject.transform.position = new Vector3(Hit.transform.position.x, Hit.point.y, Hit.transform.position.z);
            ghostObject.transform.rotation = gameObjectForGhost.transform.rotation;
        }
        else
        {
            Destroy(ghostObject);
        }
    }
}
