using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [SerializeField] Vector2 vector1;
    [SerializeField] Vector2 vector2;
    [SerializeField] float time = 1f;
    [SerializeField] bool move;
    [SerializeField] bool right;
    [SerializeField] bool behind;

    [SerializeField] string animString;

    Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) OnTest1();
        if(Input.GetKeyDown(KeyCode.W)) OnTest2();
        if(Input.GetKeyDown(KeyCode.E)) OnTest3();
        if(Input.GetKeyDown(KeyCode.R)) OnTest4();
    }

    private void OnTest1()
    {
        player.move = move;
        player.right = right;
        player.behind = behind;

        StartCoroutine(IEMove(vector1, time));
    }
    private void OnTest2()
    {
        player.move = move;
        player.right = right;
        player.behind = behind;

        StartCoroutine(IEMove(vector2, time));
    }

    private void OnTest3()
    {

        if (player != null && player.anim != null && !string.IsNullOrEmpty(animString))
        {
            player.anim.Play(animString);
        }
        else
        {
            Debug.LogWarning("Player, Animator 또는 animString이 제대로 설정되지 않았습니다.");
        }
    }

    private void OnTest4()
    {
        player.transform.position = new Vector2(0, -4);
    }



    IEnumerator IEMove(Vector2 moveOffset, float time)
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

}
