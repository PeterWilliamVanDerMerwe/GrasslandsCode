using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Rigidbody playerRigidBody;

    public bool isHoldingItem;
    public GameObject heldItem;

    [SerializeField] private float playerSpeed;
    [SerializeField] private float lengthOfRay;
    [SerializeField] private float minimumStopMovement;
    [SerializeField] private float distanceToObject;

    void Start()
    {
        isHoldingItem = false;
        playerRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        PlayerInput();
        rayCastForDropping();
    }

    /// <summary>
    /// This method handles all keyboard input from the player and will execute the function related to the input provided.
    /// </summary>
    private void PlayerInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            PlayerMovement(KeyCode.D);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            PlayerMovement(KeyCode.A);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            PlayerMovement(KeyCode.S);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            PlayerMovement(KeyCode.W);
        }
        else playerRigidBody.velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.Q))
        {
            LetGoOfItem();
        }
    }

    /// <summary>
    /// This method is responsible for player movement, including speed and rotation, both of which are fixed and dependent
    /// on the key being pressed.
    /// </summary>
    /// <param name="key">The key of the keyboard being pressed</param>
    private void PlayerMovement(KeyCode key)
    {
        Vector3 movement = transform.forward * playerSpeed;

        switch (key)
        {
            case KeyCode.D:
                transform.rotation = Quaternion.Euler(0, -90, 0);
                playerRigidBody.velocity = movement;
                break;

            case KeyCode.A:

                transform.rotation = Quaternion.Euler(0, 90, 0);
                playerRigidBody.velocity = movement;
                break;

            case KeyCode.S:

                transform.rotation = Quaternion.Euler(0, 0, 0);
                playerRigidBody.velocity = movement;
                break;

            case KeyCode.W:

                transform.rotation = Quaternion.Euler(0, 180, 0);
                playerRigidBody.velocity = movement;
                break;
        }
    }

    /// <summary>
    /// This method handles plater interaction using an interface called IIinteractable.
    /// It checks whether the player is within interaction range and if the interaction key is pressed will 
    /// execute the interaction method of the object being interacted with.
    /// </summary>
    private void PlayerInteraction(bool canInteract, IInteractable interact)
    {
        if (canInteract)
        {
            if (Input.GetKey(KeyCode.E))
            {
                interact.Interact();
            }
        }
    }

    /// <summary>
    /// This method handles the players releasing of an object by setting the parent to null and placing the object at 
    /// a transform in front of the player and only if there is ground below the player in order to not allow the player to
    /// throw objects off the map.
    /// </summary>
    private void LetGoOfItem()
    {
        RaycastHit hit;
        hit = rayCastForDropping();

        if (hit.transform.gameObject != null)
        {
            foreach (Transform t in transform)
            {
                if (t.tag == "Obstacle" || t.tag == "Dog")
                {
                    t.transform.SetParent(null);
                    t.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, hit.point.y, hit.collider.gameObject.transform.position.z);
                }
            }
            isHoldingItem = false;
        }
    }

    /// <summary>
    /// This method handles the raycast that is responsible for getting the coordinates of a point one unit directly infront 
    /// of the player.
    /// </summary>
    public RaycastHit rayCastForDropping()
    {
        Vector3 startPoint = transform.position + transform.rotation * new Vector3(0, 0, 1);
        Quaternion rotation = transform.rotation;
        Vector3 direction = rotation * Vector3.down;
        RaycastHit hit;

        if (Physics.Raycast(startPoint, direction * 10f, out hit, 100f))
        {
            Debug.Log("Hit " + hit.collider.name + " at position " + hit.point);
        }
        return hit;
    }

    private void OnTriggerStay(Collider other)
    {
        var collide = other.gameObject.GetComponent<IInteractable>();
        if (collide == null) return;
        PlayerInteraction(true, collide);
    }
}
