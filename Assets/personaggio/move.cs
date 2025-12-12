using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 100f;
    public float gravity = -9.81f;

    private float xRotation = 0f;
    private CharacterController controller;
    private Camera cam;

    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // --- Mouse Look ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;    
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // --- Movimento ---
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // --- Fix per salire gli scalini ---
        controller.stepOffset = controller.isGrounded ? 0.4f : 0f;

        // --- Gravità ---
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
