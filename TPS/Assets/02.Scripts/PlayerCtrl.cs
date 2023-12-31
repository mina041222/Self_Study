using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //������Ʈ ĳ�� ó���� ����
    private Transform tr;
    //�̵� �ӵ� ����(public���� ����Ǿ� �ν����� �信 �����
    public float moveSpeed = 10.0f;
    //ȸ�� �ӵ� ����
    public float turnSpeed = 80.0f;

    // Start is called before the first frame update
    void Start()
    {
        //transform ������Ʈ�� ������ ������ ����
        tr = GetComponent<Transform>();

        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");  // -1.0f ~ 0.0f ~ +1.0f
        float v = Input.GetAxis("Vertical");  // -1.0f ~ 0.0f ~ +1.0f
        float r = Input.GetAxis("Mouse X");

        //Debug.Log("h =" + h);
        //Debug.Log("v =" + v);

        //���� �¿� �̵� ���� �Խ�
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        //translate �Լ��� ����� �̵� ����
        tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);

        //Vector3.up ���� �������� turnSpeed��ŭ �ӵ��� ȸ��
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);

        
    }
}
