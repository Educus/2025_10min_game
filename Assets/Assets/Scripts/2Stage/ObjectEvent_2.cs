using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectEvent_2 : ObjectEvent
{
    [SerializeField] PlayerEvent playerEvent;

    public IEnumerator IEPlate() // 판 애니메이션
    {
        anim.SetBool("OnAnim", true);

        yield return null;
    }
    [SerializeField] GameObject onObj;
    public void OnObj()
    {
        onObj.SetActive(true);
    }
    public IEnumerator IEOnChangeScreen()
    {
        if (playerEvent.eventPlayer) yield break;

        TransOnScreen transOnScreen = GetComponent<TransOnScreen>();

        transOnScreen.OnScreen();

        yield return new WaitForSeconds(0.5f);

        transOnScreen.OffScreen();
        
    }
}
