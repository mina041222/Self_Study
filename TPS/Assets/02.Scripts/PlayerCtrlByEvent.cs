using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrlByEvent : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction attackAction;

    private Animator anim;
    private Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        //Move �׼� ���� �� Ÿ�� ����
        moveAction = new InputAction("Move", InputActionType.Value);

        //Move �׼��� ���� ���ε� ���� ���� 
        moveAction.AddCompositeBinding("2DVector")
        .With("Up", "<Keyboard>/w")
        .With("Down", "<Keyboard>/d")
        .With("Left", "<Keyboard>/a")
        .With("Right", "<Keyboard>/s");

        //Move �׼��� performed, canceled �̺�Ʈ ����
        moveAction.performed += ctx =>
        {
            Vector2 dir = ctx.ReadValue<Vector2>();
            moveDir = new Vector3(dir.x, 0, dir.y);
            //Warrior_Run �ִϸ��̼� ����
            anim.SetFloat("Movement", dir.magnitude);
        };

        moveAction.canceled += ctx =>
        {
            moveDir = Vector3.zero;
            anim.SetFloat("Movement", 0.0f);
        };

        //Move �׼��� Ȱ��ȭ
        moveAction.Enable();

        //Attack �׼� ����
        attackAction = new InputAction("Attack",
                                        InputActionType.Button,
                                        "<Keyboard>/space");
        //Attack �׼��� performed �̺�Ʈ ����
        attackAction.performed += ctx =>
        {
            anim.SetTrigger("Attack");
        };
        //Attack �׼��� Ȱ��ȭ
        attackAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if(moveDir != Vector3.zero)
        {
            //���� �������� ȸ��
            transform.rotation = Quaternion.LookRotation(moveDir);
            //ȸ���� �� ���� �������� �̵�
            transform.Translate(Vector3.forward * Time.deltaTime * 4.0f);
        }
    }
}
