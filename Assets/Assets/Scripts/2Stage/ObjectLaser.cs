using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLaser : MonoBehaviour
{
    [SerializeField] GameObject objOn;
    [SerializeField] GameObject objOff;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public IEnumerator IEShotLaser()
    {
        yield return StartCoroutine(IEAnim("OnAnim"));
    }
    public void OffLaser()
    {
        anim.SetBool("OffAnim", true);
        objOn.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        objOff.SetActive(true);
    }

    protected IEnumerator IEAnim(string animName)
    {
        anim.Play(animName);

        yield return null;

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        float length = stateInfo.length / anim.speed;

        yield return new WaitForSeconds(length);
    }
}
