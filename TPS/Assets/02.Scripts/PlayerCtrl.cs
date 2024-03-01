using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    //컴포넌트 캐시 처리할 변수
    private Transform tr;
    // Animation 컴포넌트를 저장할 변수
    private Animation anim;

    //이동 속도 변수(public으로 선언되어 인스펙터 뷰에 노출됨
    public float moveSpeed = 10.0f;
    //회전 속도 변수
    public float turnSpeed = 80.0f;

    //초기 생명값
    private readonly float initHp = 100.0f;
    //현재 생명값
    public float currHp;
    //HpBar 연결한 변수
    private Image hpBar;

    //델리게이트 선언
    public delegate void PlayerDieHandler();
    //이벤트 선언
    public static event PlayerDieHandler OnPlayerDie;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        //Hpbar연결
        hpBar = GameObject.FindGameObjectWithTag("HP_BAR")?.GetComponent<Image>();
        //HP 초기화
        currHp = initHp;
        DisplayHealth();

        //transform 컴포넌트를 추출해 변수에 대입
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();

        //애니메이션 실행
        anim.Play("Idle");

        turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.3f);
        turnSpeed = 80.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");  // -1.0f ~ 0.0f ~ +1.0f
        float v = Input.GetAxis("Vertical");  // -1.0f ~ 0.0f ~ +1.0f
        float r = Input.GetAxis("Mouse X");

        //Debug.Log("h =" + h);
        //Debug.Log("v =" + v);

        //전후 좌우 이동 백터 계산
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        //translate 함수를 사용한 이동 로직
        tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);

        //Vector3.up 축을 기준으로 turnSpeed만큼 속도로 회전
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);

        //주인공 캐릭터의 애니메이션 설정
        PlayerAnim(h, v);
    }

    void PlayerAnim(float h, float v)
    {
        //키보드 입력값을 기준으로 동작할 애니메이션 수행

        if (v >= 0.1f)
        {
            anim.CrossFade("RunF", 0.25f);      //전진 애니메이션 실행
        }
        else if (v <= -0.1f)
        {
            anim.CrossFade("RunB", 0.25f);      //후진 애니메이션 실행
        }
        else if (h >= 0.1f)
        {
            anim.CrossFade("RunR", 0.25f);      //오른쪽 이동 애니메이션 실행
        }
        else if (h <= -0.1f)
        {
            anim.CrossFade("RunL", 0.25f);      //왼쪽 이동 애니메이션 실행
        }
        else
        {
            anim.CrossFade("Idle", 0.25f);      //정지시 Idle 애니메이션 실행
        }
    }
    
    //충돌한 Collider가 몬스터의 PUNCH이면 Player의 HP차감
    void OnTriggerEnter(Collider coll)
    {
        //충돌한 Collider가 몬스터의 PUNCH이면 player의 HP차감
        if (currHp >= 0.0f && coll.CompareTag("PUNCH"))
        {
            currHp -= 10.0f;
            DisplayHealth();

            Debug.Log($"Player hp = {currHp / initHp}");

            //Player의 생명이 0이하면 사망 처리
            if (currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    //Player의 사망처리
    void PlayerDie()
    {
        Debug.Log("Player Die !");

        //Monster채그를 가진 모든 게임 오브젝트를 찾아옴
        //GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");

        //모든 몬스터의 OnPlayerDie 함수를 순차적으로 호출
        //foreach(GameObject monster in monsters)
        //{
        //    monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        //}

        //주인공 사망 이벤트 호출(발생)
        OnPlayerDie();

        //GameObject.Find("GameMgr").GetComponent<GameManager>().IsGameOver = true;
        GameManager.instance.IsGameOver = true;
    }

    void DisplayHealth()
    {
        hpBar.fillAmount = currHp / initHp;
    }
}
