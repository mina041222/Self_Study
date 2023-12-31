using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //컴포넌트 캐시 처리할 변수
    private Transform tr;
    //이동 속도 변수(public으로 선언되어 인스펙터 뷰에 노출됨
    public float moveSpeed = 10.0f;
    //회전 속도 변수
    public float turnSpeed = 80.0f;

    // Start is called before the first frame update
    void Start()
    {
        //transform 컴포넌트를 추출해 변수에 대입
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

        //전후 좌우 이동 백터 게싼
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        //translate 함수를 사용한 이동 로직
        tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);

        //Vector3.up 축을 기준으로 turnSpeed만큼 속도로 회전
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);

        
    }
}
