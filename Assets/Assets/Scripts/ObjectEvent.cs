using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEvent : MonoBehaviour
{
    public bool eventObject { get; protected set; } = false;

    protected Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public IEnumerator IEAnim() // 공통 애니메이션 트리거
    {
        anim.SetBool("OnAnim", true);

        yield return new WaitForSeconds(0.1f);

        anim.SetBool("OnAnim", false);
    }
}
