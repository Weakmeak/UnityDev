using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CharacterPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float hitForce = 2;
    [SerializeField] private float gravity = Physics.gravity.y;
    [SerializeField] private float turnRate = 10;
    [SerializeField] private float jumpHeight = 3;

    Vector3 velocity = Vector3.zero;

    Camera cam;
    CharacterController characterController;
    PlayInputActions actions;

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    private void Awake()
    {
        actions = new PlayInputActions();
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inpDirection = Vector3.zero;
        Vector2 axis = actions.Player.Move.ReadValue<Vector2>();
        inpDirection.x = axis.x;
        inpDirection.z = axis.y;
        inpDirection = cam.transform.TransformDirection(inpDirection);

        //transform.Translate(inpDirection * speed * Time.deltaTime);

        if(characterController.isGrounded)
        {
            velocity.x = inpDirection.x * speed;
            velocity.z = inpDirection.z * speed;

            if (actions.Player.Jump.triggered) velocity.y = Mathf.Sqrt(jumpHeight * -3 * gravity);
        } else
        {
            velocity.x = inpDirection.x * speed * 0.6f;
            velocity.z = inpDirection.z * speed * 0.6f;
            velocity.y += gravity * Time.deltaTime;

        }

        Vector3 look = inpDirection;
        look.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), turnRate * Time.deltaTime);
        characterController.Move(velocity * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * hitForce;
    }

    public void onJump(InputAction.CallbackContext callback)
    {
        if(callback.performed) Debug.Log("JoJ");
    }
}
