using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyItem3 : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] PlayerEvent_3 playerEvent;

    [SerializeField] private GameObject[] objFront;
    [SerializeField] private GameObject[] objSide;
    [SerializeField] private GameObject[] objGold;
    void Update()
    {
        if (player.behind)
        {
            AllOff();
            return;
        }


        Debug.Log(player.anim.GetBool("Grap"));
        if (player.anim.GetBool("Grap"))
        {
            int num = playerEvent.itemNum;
            float x;

            if (!player.move)
            {
                if (num == 9)
                {
                    objGold[0].SetActive(true);
                    objGold[1].SetActive(false);
                }
                else
                {
                    for (int i = 0; i < objFront.Length; i++)
                    {
                        if (i == num)
                            objFront[i].SetActive(true);
                        else
                            objFront[i].SetActive(false);

                        objSide[i].SetActive(false);
                    }
                }
            }
            else
            {
                if (num == 9)
                {
                    objGold[0].SetActive(false);
                    objGold[1].SetActive(true);
                    objGold[1].GetComponent<SpriteRenderer>().flipX = player.right;

                    x = Mathf.Abs(objGold[1].transform.localPosition.x);
                    if (!player.right) x = -x;
                        objGold[1].transform.localPosition = new Vector3(x, objGold[1].transform.localPosition.y);
                }
                else
                {
                    for (int i = 0; i < objFront.Length; i++)
                    {
                        if (i == num)
                            objSide[i].SetActive(true);
                        else
                            objSide[i].SetActive(false);

                        objFront[i].SetActive(false);
                    }

                    objSide[num].GetComponent<SpriteRenderer>().flipX = player.right;
                    x = Mathf.Abs(objSide[num].transform.localPosition.x);
                    if (!player.right) x = -x;
                    objSide[num].transform.localPosition = new Vector3(x, objSide[num].transform.localPosition.y);
                }
            }

        }
        else
        {
            AllOff();
        }
    }

    void AllOff()
    {
        for (int i = 0; i < objFront.Length; i++)
        {
            objFront[i].SetActive(false);
            objSide[i].SetActive(false);
        }

        objGold[0].SetActive(false);
        objGold[1].SetActive(false);
    }
}
