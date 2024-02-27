using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //���Ͱ� ������ ��ġ�� ������ List Ÿ�� ����
    public List<Transform> points = new List<Transform>();

    //���� �������� ������ ����
    public GameObject monster;

    //������ ���� ����
    public float createTime = 3.0f;

    //������ ���� ���θ� ������ �ɹ� ����
    private bool isGameOver;
    
    //������ ���� ���θ� ������ ������Ƽ
    public bool IsGameOver
    {
        get { return isGameOver;}
        set
        {
            isGameOver = value;
            if(isGameOver)
            {
                CancelInvoke("CreateMonster");
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //SpawnPointGroup ���� ������Ʈ�� Transform ������Ʈ ����
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;

        //SpawmPointGroup ������ �ִ� ��� ���ϵ� ���� ������Ʈ�� Trasnform ������Ʈ ����
        foreach(Transform point in spawnPointGroup)
        {
            points.Add(point);
        }

        //������ �ð� �������� �Լ��� ȣ��
        InvokeRepeating("CreateMonster", 2.0f, createTime);
        
    }

    void CreateMonster()
    {
        //������ �ұ�Ģ�� ���� ��ġ ����
        int idx = Random.Range(0, points.Count);
        //���� ������ ����
        Instantiate(monster, points[idx].position, points[idx].rotation);
    }
}
