using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어 크기 조정
    private float y1 = -5f;
    private float y2 = -0.5f;
    private float scale1 = 1f;
    private float scale2 = 0.8f;

    private SpriteRenderer sprite;
    private Vector2 startPos;
    public Vector2 StartPos() { return startPos; }

    public Animator anim;
    
    public bool right = false;
    public bool move = false;
    public bool behind = false;
    public bool objBreak = false;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        startPos = transform.position;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        SetSize();
        SetLayer();

        SetAnim();
    }

    private void SetSize() // y축에 따라 scale 변경
    {
        float y = transform.position.y;
        float t = Mathf.InverseLerp(y1, y2, y);
        float scale = Mathf.Lerp(scale1, scale2, t);
        transform.localScale = Vector3.one * scale;
    }
    private void SetLayer() // y축에따라 layer 변경
    {
        float y = transform.position.y;
        int order = Mathf.RoundToInt(-y);
        sprite.sortingOrder = Mathf.Max(order, 2);
    }

    private void SetAnim()
    {
        sprite.flipX = right;

        anim.SetBool("Back",behind);
        anim.SetBool("Move", move);

    }
}
