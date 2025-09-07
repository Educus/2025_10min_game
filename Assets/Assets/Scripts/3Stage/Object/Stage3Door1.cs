using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Door1 : MonoBehaviour
{
    [SerializeField] GameObject window;
    [SerializeField] GameObject door;
    private Animator windowAnim;
    private Animator doorAnim;

    private void Start()
    {
        windowAnim = window.GetComponent<Animator>();
        doorAnim = door.GetComponent<Animator>();
    }
    public IEnumerator IEDoorOn()
    {
        yield return StartCoroutine(IEOnAnim(windowAnim, "OnAnim"));
        yield return StartCoroutine(IEOnAnim(doorAnim, "OnAnim"));
    }
    public IEnumerator IEDoorOff()
    { 
        yield return StartCoroutine(IEOnAnim(doorAnim, "OffAnim"));
        yield return StartCoroutine(IEOnAnim(windowAnim, "OffAnim"));
    }

    public IEnumerator IELayger()
    {
        yield return StartCoroutine(IEOnAnim(windowAnim, "OnAnim"));
    }
    public IEnumerator IELaygerOff()
    {
        gameObject.SetActive(false);
        yield return null;
    }

    private IEnumerator IEOnAnim(Animator anim, string animName)
    {
        anim.Play(animName);

        yield return null;

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        float length = stateInfo.length / anim.speed;

        yield return new WaitForSeconds(length);

        Debug.Log("애니메이션 출력 끝");
    }

}
