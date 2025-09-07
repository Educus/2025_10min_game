using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiggyBank : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject goldObj;
    public bool setGold {  get; private set; } = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        goldObj.SetActive(false);
    }
    public void SetGold()
    {
        anim.SetBool("OnAnim", true);
        goldObj.SetActive(true);
        setGold = true;
    }
    public void GetGold()
    {
        anim.SetBool("OnAnim", false);
        goldObj.SetActive(false);
        setGold = false;
    }
}
