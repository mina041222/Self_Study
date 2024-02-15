using UnityEngine;

public class DelegateDemo : MonoBehaviour
{
    //��������Ʈ ����
    delegate float SumHandler(float a, float b);
    //��������Ʈ Ÿ���� ���� ����
    SumHandler sumHandler;

    //���� ������ �ϴ� �Լ�
    float Sum(float a, float b)
    {
        return a + b;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //��������Ʈ ������ �Լ�(�޼���) ����(�Ҵ�)
        sumHandler = Sum;
        //��������Ʈ ����
        float sum = sumHandler(10.0f, 5.0f);
        //����� ���
        Debug.Log($"Sum = {sum}");
    }

}
