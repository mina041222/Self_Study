using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    //�������� ������ �ؽ��� �迭
    public Texture[] textures;
    //������ �ִ� Mesh Renderer ������Ʈ�� ������ ����
    private new MeshRenderer renderer;

    //������Ʈ�� ������ ����
    private Transform tr;
    private Rigidbody rb;

    //�Ѿ� ���� Ƚ���� ������ų ����
    private int hitCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        //������ �ִ� MeshRenderer ������Ʈ�� ����
        renderer = GetComponentInChildren<MeshRenderer>();

        //���� �߻�
        int idx = Random.Range(0, textures.Length);
        //�ؽ��� ����
        renderer.material.mainTexture = textures[idx];
    }
    
    //�浹�� �߻��ϴ� �ݹ� �Լ�
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            //�Ѿ� ���� Ƚ���� ���� ��Ű�� 3ȸ �̻��̸� ���� ó��
            if (++hitCount == 3)
            {
                ExpBarrel();
            }

        }
    }

    void ExpBarrel()
    {
        //Rigidbody ������Ʈ�� mass�� 1.0���� ������ ���Ը� ������ ��
        rb.mass = 1.0f;
        //���� �ڱ�ġ�� ���� ����
        rb.AddForce(Vector3.up * 1500.0f);

        //3�� �Ŀ� �巳�� ����
        Destroy(gameObject, 3.0f);
    }
}
