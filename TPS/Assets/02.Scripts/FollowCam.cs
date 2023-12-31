using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    //따라가야 할 대상을 연결할 변수
    public Transform targetTr;

    //Main Camera 자신의 Transform 컴포넌트
    private Transform camTr;

    //따라갈 대상으로부터 떨어질 거리
    [Range(2.0f, 20.0f)]
    public float distance = 10.0f;

    //Y축으로 이동할 높이
    [Range(0.0f, 10.0f)]
    public float height = 2.0f;

    //반응속도
    public float damping = 10.0f;

    //카메라 LookAt의 Offset 값
    public float targetOffset = 2.0f;

    //SmoothDamp에서 사용할 변수
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        //Main Camera 자신의 Transform 컴포넌트를 추출
        camTr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //추적해야 할 대상의 뒤쪽으로 distance만큼 이동
        //높이를 height만큼 이동
        Vector3 pos = targetTr.position + (-targetTr.forward * distance) + (Vector3.up * height);

        //구면 선형보간 함수를 사용해 부드럽게 위치를 변경
        //camTr.position = Vector3.Slerp(camTr.position, pos, Time.deltaTime * damping); //시작 좌표, 목표 위치, 시간 t

        //SmoothDamp을 이용한 위치 보간
        camTr.position = Vector3.SmoothDamp(camTr.position, pos, ref velocity, damping); //시작 좌표, 목표 위치, 현재 속도, 목표 위치까지 걸리는 시간

        //Camera를 피벗 좌표를 향해 회전
        camTr.LookAt(targetTr.position + (targetTr.up * targetOffset));

    }
}
