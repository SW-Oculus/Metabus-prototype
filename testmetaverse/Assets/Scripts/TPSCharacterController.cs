using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCharacterController : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;

    Animator animator;
    Rigidbody rigid;

    bool isJumpingUp; // ���� ���� ��
    //bool isJumpingDown; // �����ϰ� �Ʒ��� �������� ��
    bool isOnGround; // �÷��̾ �� ���� ���ִ��� üũ

    void Awake()
    {
        animator = characterBody.GetComponent<Animator>();
        rigid = characterBody.GetComponent<Rigidbody>();
        isOnGround = true;
    }

    void Update()
    {
        Move();
        InteractObj();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void LateUpdate()
    {
        LookAround();
    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        // ī�޶� ���� ���� (���콺�� �����̴ٺ��� ȭ���� ���������� ���� �߻�)
        float x = camAngle.x - mouseDelta.y;
        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMoving = moveInput.magnitude != 0;
        animator.SetBool("isMoving", isMoving);
        if (isMoving)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * 5f;
        }
    }

    private void InteractObj()
    {
        if (Input.GetMouseButton(0))
            animator.SetBool("isInteracting", true);
        else
            animator.SetBool("isInteracting", false);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            rigid.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            isOnGround = false;
            Debug.Log("Jumping");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            Debug.Log("On the Ground");
            isOnGround = true;
        }
    }
}

