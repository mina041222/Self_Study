using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{

    //�浹�� ������ �� �߻��Ѵ� �̺�Ʈ
    void OnCollisionEnter(Collision coll)
    {
        //�浹�� ���� ������Ʈ �±װ� ��
        if (coll.collider.CompareTag("BULLET"))
        {

            //�浹�� ���� ������Ʈ ����
            Destroy(coll.gameObject);
        }
    }
}
