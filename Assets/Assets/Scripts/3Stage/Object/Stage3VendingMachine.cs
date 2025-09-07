using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Ʈ��0, ��� 1, �Ȱ�2, �ֵ���3, ������4, ��ġ5
public class Stage3VendingMachine : MonoBehaviour
{
    [SerializeField] PlayerEvent_3 playerEvent;
    private Animator anim;
    private int count = 0;
    private int totalObj = 6;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public IEnumerator IEMachine()
    {
        int num;

        if (count == 1)
        {
            num = 0;
        }
        else
        {
            num = Random.Range(1, totalObj);
        }
        // �ι�° ������ ��÷
        // �ι�° ���� ������ ��

        anim.SetBool("SetItem", true);
        anim.SetInteger("Num", num);

        yield return new WaitForSeconds(0.8f);

        playerEvent.GetItems(num);

        count++;
    }

    public void OffItem() // ��� ������ setActive false
    {
        anim.SetBool("SetItem", false);
    }
}
