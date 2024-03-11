#pragma warning disable IDE0051

using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Animator anim;
    private new Transform transform;
    private Vector3 moveDir;

    void Start()
    {
        anim = GetComponent<Animator>();
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        if (moveDir != Vector3.zero)
        {
            //���� �������� ȸ��
            transform.rotation = Quaternion.LookRotation(moveDir);
            //ȸ���� �� ���� �������� �̵�
            transform.Translate(Vector3.forward * Time.deltaTime * 4.0f);
        }
    }

    #region SEND_MESSAGE
    void OnMove(InputValue value)
    {
        Vector2 dir = value.Get<Vector2>();

        //2���� ��ǥ�� 3���� ��ǥ�� ��ȯ
        moveDir = new Vector3(dir.x, 0, dir.y);
        anim.SetFloat("Movement", dir.magnitude);

        Debug.Log($"Move = ({dir.x},{dir.y})");
    }

    void OnAttack()
    {
        Debug.Log("Attack");
        anim.SetTrigger("Attack");
    }
    # endregion

    #region UNITY_EVENTS
    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 dir = ctx.ReadValue<Vector2>();

        //2���� ��ǥ�� 3���� ��ǥ�� ��ȯ
        moveDir = new Vector3(dir.x, 0, dir.y);

        //Warrior_Run �ִϸ��̼� ����
        anim.SetFloat("Movement", dir.magnitude);
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        Debug.Log($"ctx.phase={ctx.phase}");

        if (ctx.performed)
        {
            Debug.Log("Attack");
            anim.SetTrigger("Attack");
        }
    }
    #endregion

}