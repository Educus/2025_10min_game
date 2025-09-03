using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEvent_1 : MonoBehaviour
{
    public bool eventObject { get; private set; } = false;

    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    public IEnumerator IEBone() // �ذ�
    {
        Debug.Log("gorhf");

        anim.SetBool("OnAnim", true);

        yield return new WaitForSeconds(0.1f);

        anim.SetBool("OnAnim", false);
    }
    public IEnumerator IEAnim() // ���� �ִϸ��̼� Ʈ����
    {
        anim.SetBool("OnAnim", true);

        yield return new WaitForSeconds(0.1f);

        anim.SetBool("OnAnim", false); 
    }

}
