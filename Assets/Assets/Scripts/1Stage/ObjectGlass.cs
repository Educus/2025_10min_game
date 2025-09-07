using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGlass : MonoBehaviour
{
    [SerializeField] GameObject glass;
    [SerializeField] GameObject[] ladder;
    private Animator anim;

    void Start()
    {
        anim = glass.GetComponent<Animator>();
    }

    public void BreakGlass()
    {
        anim.SetBool("OnAnim", true);
        // ladder[0].GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
        // ladder[1].SetActive(true);
    }

      
}
