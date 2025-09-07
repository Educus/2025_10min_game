using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 트랩0, 기어 1, 안경2, 핫도그3, 슬라임4, 렌치5
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
        // 두번째 무조건 당첨
        // 두번째 제외 무조건 꽝

        anim.SetBool("SetItem", true);
        anim.SetInteger("Num", num);

        yield return new WaitForSeconds(0.8f);

        playerEvent.GetItems(num);

        count++;
    }

    public void OffItem() // 모든 아이템 setActive false
    {
        anim.SetBool("SetItem", false);
    }
}
