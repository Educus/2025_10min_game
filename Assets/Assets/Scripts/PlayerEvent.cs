using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvent : MonoBehaviour
{
    protected Player player;
    protected bool getItem = false;
    public bool eventPlayer { get; protected set; } = false;

    protected virtual void Start()
    {
        player = GetComponent<Player>();
    }

    protected IEnumerator IEMove(Vector2 moveOffset, float time)
    {
        Vector3 start = player.transform.position;
        Vector3 target = moveOffset;

        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / time;
            player.transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }
    }
    protected IEnumerator IEObjMove(GameObject obj, Vector2 moveOffset, float time)
    {
        Vector3 start = obj.transform.position;
        Vector3 target = obj.transform.position + new Vector3(moveOffset.x, moveOffset.y);

        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / time;
            obj.transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }
    }

    protected IEnumerator IEAnim(string animName)
    {
        player.anim.Play(animName);

        yield return null;

        AnimatorStateInfo stateInfo = player.anim.GetCurrentAnimatorStateInfo(0);
        float length = stateInfo.length / player.anim.speed;

        yield return new WaitForSeconds(length);

        Debug.Log("애니메이션 출력 끝");
    }
}
