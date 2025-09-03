using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEvent_1 : ObjectEvent
{
    public IEnumerator IEBone() // ÇØ°ñ
    {
        Debug.Log("gorhf");

        anim.SetBool("OnAnim", true);

        yield return new WaitForSeconds(0.1f);

        anim.SetBool("OnAnim", false);
    }
}
