using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSprite : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject frontObj;
    [SerializeField] GameObject[] sideObj;
    void Update()
    {
        ActiveObj();
        SideObj();
    }

    private void ActiveObj()
    {
        if (player.objBreak == true)
        {
            frontObj.SetActive(false);
            sideObj[0].SetActive(false);
        }
        else if (player.move == true && player.behind == true)
        {
            frontObj.SetActive(false);
            sideObj[0].SetActive(false);
        }
        else if (player.move == true && player.behind == false)
        {
            frontObj.SetActive(false);
            sideObj[0].SetActive(true);
        }
        else
        {
            frontObj.SetActive(true);
            sideObj[0].SetActive(false);
        }
    }

    private void SideObj()
    {
        sideObj[0].transform.localPosition = new Vector3(Mathf.Abs(sideObj[0].transform.localPosition.x) * (player.right ? 1 : -1), sideObj[0].transform.localPosition.y);
    }
}
