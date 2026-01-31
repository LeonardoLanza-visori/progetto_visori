using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(CharacterController))]
public class VRMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public XRNode inputSource = XRNode.LeftHand; // Usa la mano sinistra per il movimento
    public float gravity = -9.81f;

    private CharacterController character;
    private Vector2 inputAxis;
    private float fallingVelocity;
    private Transform cameraTransform;

    void Start()
    {
        character = GetComponent<CharacterController>();
        // Troviamo la telecamera per capire in che direzione muoverci
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Recuperiamo l'input dall'analogico dell'Oculus
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    private void FixedUpdate()
    {
        // Calcoliamo la direzione basandoci su dove guarda il visore
        Quaternion headYaw = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        // Applichiamo movimento e gravità
        character.Move(direction * speed * Time.fixedDeltaTime);

        if (character.isGrounded)
            fallingVelocity = 0;
        else
            fallingVelocity += gravity * Time.fixedDeltaTime;

        character.Move(Vector3.up * fallingVelocity * Time.fixedDeltaTime);
    }
}