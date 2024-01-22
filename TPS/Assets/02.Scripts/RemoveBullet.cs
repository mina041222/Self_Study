using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{

    //충돌이 시작할 떄 발생한는 이벤트
    void OnCollisionEnter(Collision coll)
    {
        //충돌한 게임 오브젝트 태그값 비교
        if (coll.collider.CompareTag("BULLET"))
        {

            //충돌한 게임 오브젝트 삭제
            Destroy(coll.gameObject);
        }
    }
}
