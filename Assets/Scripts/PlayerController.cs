using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask ground;
    public float speed;
    public float sensitivity;
    public float jumpHeight;
    public Transform cameraTransform;
    CharacterController cc;
    void Start()
    {
        cc = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    public float lastYVelocity = 0;
    void Update()
    {
        bool isGrounded = Physics.CheckSphere(transform.position, 0.08f, ground, QueryTriggerInteraction.Ignore);
        Movement(isGrounded);
        Rotation();


    }

    float camXRot = 0;
    private void Rotation()
    {
        transform.Rotate(0, Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime, 0);
        camXRot = Mathf.Clamp(camXRot - (Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime), -90, 90);
        cameraTransform.localRotation = Quaternion.Euler(camXRot, 0, 0);
    }

    private void Movement(bool isGrounded)
    {
        Vector3 move = ((Input.GetAxisRaw("Horizontal") * transform.right) + (Input.GetAxisRaw("Vertical") * transform.forward)).normalized * Time.deltaTime * speed;

        if (Input.GetButtonDown("Jump") && isGrounded)
            lastYVelocity += Mathf.Sqrt(jumpHeight * 2f * 9.81f);

        if (isGrounded && lastYVelocity < 0)
            lastYVelocity = 0f;
        else if (!isGrounded)
            lastYVelocity -= 9.81f * Time.deltaTime;
        move.y = lastYVelocity;

        cc.Move(move);
    }
}
