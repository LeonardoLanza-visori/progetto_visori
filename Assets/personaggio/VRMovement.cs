using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(CharacterController))]
public class VRMovement : MonoBehaviour
{
    [Header("Configurazione Movimento")]
    public float speed = 3.5f;
    public XRNode inputSource = XRNode.LeftHand; // Analogico Sinistro
    public float gravity = -9.81f;

    private CharacterController character;
    private Vector2 inputAxis;
    private float fallingVelocity;
    private Transform cameraTransform;

    void Start()
    {
        character = GetComponent<CharacterController>();
        
        // Cerchiamo la Main Camera per orientare il movimento secondo lo sguardo
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        // Impedisce al controller di ignorare l'altezza impostata manualmente
        character.stepOffset = 0.1f;
    }

    void Update()
    {
        // Recuperiamo l'input dell'analogico dai controller Oculus
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
         void CapsuleFollowHeadset()
{
    // Adatta l'altezza della capsula all'altezza della camera
    character.height = cameraTransform.localPosition.y;
    
    // Sposta il centro della capsula a metà dell'altezza per far toccare i piedi a terra
    Vector3 capsuleCenter = cameraTransform.localPosition;
    capsuleCenter.y = character.height / 2;
    character.center = capsuleCenter;
    }
    }

    private void FixedUpdate()
    {
        // Calcoliamo la rotazione orizzontale del visore
        Quaternion headYaw = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        
        // Calcoliamo la direzione del movimento basata sull'input e sulla rotazione della testa
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        // Applichiamo il movimento orizzontale
        character.Move(direction * speed * Time.fixedDeltaTime);

        // Gestione della gravità
        if (character.isGrounded)
        {
            fallingVelocity = 0;
        }
        else
        {
            fallingVelocity += gravity * Time.fixedDeltaTime;
        }

        // Applichiamo la gravità (Movimento verticale)
        character.Move(Vector3.up * fallingVelocity * Time.fixedDeltaTime);
    }
}