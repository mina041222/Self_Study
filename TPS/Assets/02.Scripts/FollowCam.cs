using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    //���󰡾� �� ����� ������ ����
    public Transform targetTr;

    //Main Camera �ڽ��� Transform ������Ʈ
    private Transform camTr;

    //���� ������κ��� ������ �Ÿ�
    [Range(2.0f, 20.0f)]
    public float distance = 10.0f;

    //Y������ �̵��� ����
    [Range(0.0f, 10.0f)]
    public float height = 2.0f;

    //�����ӵ�
    public float damping = 10.0f;

    //ī�޶� LookAt�� Offset ��
    public float targetOffset = 2.0f;

    //SmoothDamp���� ����� ����
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        //Main Camera �ڽ��� Transform ������Ʈ�� ����
        camTr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //�����ؾ� �� ����� �������� distance��ŭ �̵�
        //���̸� height��ŭ �̵�
        Vector3 pos = targetTr.position + (-targetTr.forward * distance) + (Vector3.up * height);

        //���� �������� �Լ��� ����� �ε巴�� ��ġ�� ����
        //camTr.position = Vector3.Slerp(camTr.position, pos, Time.deltaTime * damping); //���� ��ǥ, ��ǥ ��ġ, �ð� t

        //SmoothDamp�� �̿��� ��ġ ����
        camTr.position = Vector3.SmoothDamp(camTr.position, pos, ref velocity, damping); //���� ��ǥ, ��ǥ ��ġ, ���� �ӵ�, ��ǥ ��ġ���� �ɸ��� �ð�

        //Camera�� �ǹ� ��ǥ�� ���� ȸ��
        camTr.LookAt(targetTr.position + (targetTr.up * targetOffset));

    }
}
