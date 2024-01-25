using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    //����ũ ��ƼŬ �������� ������ ����
    public GameObject sparkEffect;

    //�浹�� ������ �� �߻��Ѵ� �̺�Ʈ
    void OnCollisionEnter(Collision coll)
    {
        //�浹�� ���� ������Ʈ �±װ� ��
        if (coll.collider.CompareTag("BULLET"))
        {
            //ù��° �浹 ������ ���� ����
            ContactPoint cp = coll.GetContact(0);
            //�浹�� �Ѿ��� ���� ���͸� ���ʹϾ� Ÿ������ ��ȯ
            Quaternion rot = Quaternion.LookRotation(-cp.normal);

            //����ũ ��ƼŬ�� �������� ����
            GameObject spark = Instantiate (sparkEffect, cp.point, rot);
            //�����ð��� ������ ����Ŭ ��ƼŬ�� ����
            Destroy(spark, 0.5f);
            //�浹�� ���� ������Ʈ ����
            Destroy(coll.gameObject);
        }
    }
}
