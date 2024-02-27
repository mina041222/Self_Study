using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    //������Ʈ ĳ�� ó���� ����
    private Transform tr;
    // Animation ������Ʈ�� ������ ����
    private Animation anim;

    //�̵� �ӵ� ����(public���� ����Ǿ� �ν����� �信 �����
    public float moveSpeed = 10.0f;
    //ȸ�� �ӵ� ����
    public float turnSpeed = 80.0f;

    //�ʱ� ����
    private readonly float initHp = 100.0f;
    //���� ����
    public float currHp;
    //HpBar ������ ����
    private Image hpBar;

    //��������Ʈ ����
    public delegate void PlayerDieHandler();
    //�̺�Ʈ ����
    public static event PlayerDieHandler OnPlayerDie;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        //Hpbar����
        hpBar = GameObject.FindGameObjectWithTag("HP_BAR")?.GetComponent<Image>();
        //HP �ʱ�ȭ
        currHp = initHp;
        DisplayHealth();

        //transform ������Ʈ�� ������ ������ ����
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();

        //�ִϸ��̼� ����
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

        //���� �¿� �̵� ���� ���
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        //translate �Լ��� ����� �̵� ����
        tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);

        //Vector3.up ���� �������� turnSpeed��ŭ �ӵ��� ȸ��
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);

        //���ΰ� ĳ������ �ִϸ��̼� ����
        PlayerAnim(h, v);
    }

    void PlayerAnim(float h, float v)
    {
        //Ű���� �Է°��� �������� ������ �ִϸ��̼� ����

        if (v >= 0.1f)
        {
            anim.CrossFade("RunF", 0.25f);      //���� �ִϸ��̼� ����
        }
        else if (v <= -0.1f)
        {
            anim.CrossFade("RunB", 0.25f);      //���� �ִϸ��̼� ����
        }
        else if (h >= 0.1f)
        {
            anim.CrossFade("RunR", 0.25f);      //������ �̵� �ִϸ��̼� ����
        }
        else if (h <= -0.1f)
        {
            anim.CrossFade("RunL", 0.25f);      //���� �̵� �ִϸ��̼� ����
        }
        else
        {
            anim.CrossFade("Idle", 0.25f);      //������ Idle �ִϸ��̼� ����
        }
    }
    
    //�浹�� Collider�� ������ PUNCH�̸� Player�� HP����
    void OnTriggerEnter(Collider coll)
    {
        //�浹�� Collider�� ������ PUNCH�̸� player�� HP����
        if (currHp >= 0.0f && coll.CompareTag("PUNCH"))
        {
            currHp -= 10.0f;
            DisplayHealth();

            Debug.Log($"Player hp = {currHp / initHp}");

            //Player�� ������ 0���ϸ� ��� ó��
            if (currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    //Player�� ���ó��
    void PlayerDie()
    {
        Debug.Log("Player Die !");

        //Monsterä�׸� ���� ��� ���� ������Ʈ�� ã�ƿ�
        //GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");

        //��� ������ OnPlayerDie �Լ��� ���������� ȣ��
        //foreach(GameObject monster in monsters)
        //{
        //    monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        //}

        //���ΰ� ��� �̺�Ʈ ȣ��(�߻�)
        OnPlayerDie();

        GameObject.Find("GameMgr").GetComponent<GameManager>().IsGameOver = true;
    }

    void DisplayHealth()
    {
        hpBar.fillAmount = currHp / initHp;
    }
}
