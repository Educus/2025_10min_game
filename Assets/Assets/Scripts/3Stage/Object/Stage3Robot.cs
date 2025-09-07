using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Robot : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sprite;

    private Vector3 startPos = new Vector2(0.1f, -0.5f);
    private Vector3 endPos = new Vector2(1f, -1.5f);
    private void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(transform.localPosition.x >= 0.285)
        {
            sprite.sortingOrder = 3;
        }
        else
        {
            sprite.sortingOrder = -1;
        }
    }

    public IEnumerator IEOnRobot()
    {
        yield return StartCoroutine(IEMove(startPos, endPos, 2f));
    }

    public IEnumerator IEOffRobot()
    {
        yield return StartCoroutine(IEMove(endPos, startPos, 2f));
    }

    public IEnumerator IEDeadRobot()
    {
        yield return StartCoroutine(IEAnim("3Stage_Robot_Elec"));
    }

    private IEnumerator IEMove(Vector3 startPos, Vector3 targetPos,float time)
    {
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / time;
            transform.localPosition = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }
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
