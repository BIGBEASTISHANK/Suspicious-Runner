using UnityEngine;

public class camera : MonoBehaviour
{
    // VARIABLES
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float xRotation;
    [SerializeField] private float yRotation;
    [SerializeField] private float zRotation;
    [SerializeField] private Transform player;

    // REFFFERNCE
    //////////////////////
    // RUN ONCE AT START
    void Start()
    {
        // Make sure mouse is locked in the game.
        Cursor.lockState = CursorLockMode.Locked;

        // Get player body.
        player = transform.parent;
    }

    void Update()
    {
        // VERIABLE
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Getting where to move mouse.
        xRotation -= mouseY;
        // Stop mouse rotation to get beyond one point.
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        // Rotating player for rotating side by side.
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);

        // Rotating camera up and down.
        player.Rotate(Vector3.up * mouseX);

    }
}