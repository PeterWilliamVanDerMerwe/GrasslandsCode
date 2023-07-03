using UnityEngine;

public class DogRotater : MonoBehaviour
{
    Transform currentDog;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentDog.rotation *= Quaternion.Euler(0, 90f, 0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Dog")
        {
            currentDog = other.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        currentDog = null;
    }
}
