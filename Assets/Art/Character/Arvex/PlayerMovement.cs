using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 720f;
    
    // Drag Arvex_RIG into this box in the inspector
    [SerializeField] private Transform characterVisual; 

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            // Moves the parent (moves both character and camera together)
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

            // Rotates ONLY the visual rig, keeping the camera stable
            if (characterVisual != null)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                characterVisual.rotation = Quaternion.RotateTowards(characterVisual.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}

