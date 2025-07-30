using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private CharacterMovement playerMov;

    void Start()
    {
        playerMov = GetComponentInParent<CharacterMovement>();
    }

    void OnTriggerEnter(Collider other)
    {
        playerMov.isGrounded = true;
    }

    void OnTriggerExit(Collider other)
    {
        playerMov.isGrounded = false;
    }
}
