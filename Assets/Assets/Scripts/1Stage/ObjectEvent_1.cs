using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEvent_1 : ObjectEvent
{
    [SerializeField] private PlayerEvent_1 playerEvent_1;
    [SerializeField] private GameObject[] boneDescription;
    private int boneNum = 0;
    public IEnumerator IEBone() // ÇØ°ñ
    {
        eventObject = true;

        if (!playerEvent_1.onLight)
        {
            eventObject = false;
            yield break;
        }

        anim.SetBool("OnAnim", true);

        if(boneNum >= boneDescription.Length) boneNum = 0;

        boneDescription[boneNum].SetActive(true);
        yield return new WaitForSeconds(1f);
        boneDescription[boneNum].SetActive(false);
        boneNum++;

        anim.SetBool("OnAnim", false);

        eventObject = false;
    }
}
