using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    // Limites de campo de visão
    [SerializeField] float turnSpeed = 25f;
    [SerializeField] float headUpperAngleLimit = 85f;
    [SerializeField] float headLowerAngleLimit = -80f;

    // Guinada: Horizontal
    float yaw = 0f;
    // pitch: Vertical
    float pitch = 0f;

    Quaternion bodyStartOrientation;
    Quaternion headStartorientation;
    Transform head;

    void Start()
    {
        head = GetComponentInChildren<Camera>().transform;
        bodyStartOrientation = transform.localRotation;
        headStartorientation = head.transform.localRotation;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Leitura do movimento do mouse
        var mouseHorizontal = Mouse.current.delta.x.ReadValue() * turnSpeed * Time.fixedDeltaTime;
        var mouseVertical = Mouse.current.delta.y.ReadValue() * turnSpeed * Time.fixedDeltaTime;

        // Leitura do movimento do left stick do gamepad
        var gamepad = Gamepad.current;
        float stickHorizontal = 0f;
        float stickVertical = 0f;

        if (gamepad != null)
        {
            stickHorizontal = gamepad.rightStick.x.ReadValue() * turnSpeed * Time.fixedDeltaTime;
            stickVertical = gamepad.rightStick.y.ReadValue() * turnSpeed * Time.fixedDeltaTime;
        }

        // Combinar os valores do mouse e do gamepad
        var horizontal = mouseHorizontal + stickHorizontal;
        var vertical = mouseVertical + stickVertical;

        yaw += horizontal;
        pitch -= vertical;

        pitch = Mathf.Clamp(pitch, headLowerAngleLimit, headUpperAngleLimit);

        var bodyRotaion = Quaternion.AngleAxis(yaw, Vector3.up);
        var headRotation = Quaternion.AngleAxis(pitch, Vector3.right);

        transform.localRotation = bodyRotaion * bodyStartOrientation;
        head.localRotation = headRotation * headStartorientation;
    }
}
