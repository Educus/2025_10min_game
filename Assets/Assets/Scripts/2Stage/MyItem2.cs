using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyItem2 : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] PlayerEvent_2 playerEvent;

    // [SerializeField] GameObject[] arm;
    // [SerializeField] GameObject[] flyArm;
    // [SerializeField] GameObject[] strongArm;
    [SerializeField] GameObject[] seed;
    [SerializeField] GameObject[] nipper;
    void Start()
    {
        AllOff();
    }
    void Update()
    {
        if (playerEvent.getSeed)
        {
            HandleItem(seed);
        }
        else if (playerEvent.getNipper)
        {
            HandleItem(nipper);
        }
        else
        {
            AllOff();
        }
    }

    private void HandleItem(GameObject[] item)
    {
        if (player.move && !player.behind)
        {
            /*
            // Arm 전환
            if (playerEvent.num == 1)
            {
                flyArm[0].SetActive(false);
                flyArm[1].SetActive(true);
                strongArm[0].SetActive(false);
                strongArm[1].SetActive(false);
                arm[0].SetActive(false);
                arm[1].SetActive(false);
            }
            else if (playerEvent.num == 2)
            {
                flyArm[0].SetActive(false);
                flyArm[1].SetActive(false);
                strongArm[0].SetActive(false);
                strongArm[1].SetActive(true);
                arm[0].SetActive(false);
                arm[1].SetActive(false);
            }
            else
            {
                flyArm[0].SetActive(false);
                flyArm[1].SetActive(false);
                strongArm[0].SetActive(false);
                strongArm[1].SetActive(false);
                arm[0].SetActive(false);
                arm[1].SetActive(true);
            }
            */

            // 아이템 전환
            item[0].SetActive(false);
            item[1].SetActive(true);
            
            // 방향에 따라 플립 & 위치 조정
            bool flip = player.right;
            /*
            flyArm[1].GetComponent<SpriteRenderer>().flipX = flip;
            strongArm[1].GetComponent<SpriteRenderer>().flipX = flip;
            arm[1].GetComponent<SpriteRenderer>().flipX = flip;
            */

            float x = Mathf.Abs(item[1].transform.localPosition.x);
            if (!flip) x = -x;

            item[1].transform.localPosition = new Vector3(x,item[1].transform.localPosition.y);
        }
        else if (!player.move)
        {
            /*
            // Arm 복구
            if (playerEvent.num == 1)
            {
                flyArm[0].SetActive(true);
                flyArm[1].SetActive(false);
                strongArm[0].SetActive(false);
                strongArm[1].SetActive(false);
                arm[0].SetActive(false);
                arm[1].SetActive(false);
            }
            else if (playerEvent.num == 1)
            {
                flyArm[0].SetActive(false);
                flyArm[1].SetActive(false);
                strongArm[0].SetActive(true);
                strongArm[1].SetActive(false);
                arm[0].SetActive(true);
                arm[1].SetActive(false);
            }
            else
            {
                flyArm[0].SetActive(false);
                flyArm[1].SetActive(false);
                strongArm[0].SetActive(false);
                strongArm[1].SetActive(false);
                arm[0].SetActive(true);
                arm[1].SetActive(false);
            }
            */
            // 아이템 복구
            item[0].SetActive(true);
            item[1].SetActive(false);
        }
        else
        {
            AllOff();
        }
    }

    private void AllOff()
    {
        /*
        arm[0].SetActive(false);
        arm[1].SetActive(false);
        strongArm[0].SetActive(false);
        strongArm[1].SetActive(false);
        flyArm[0].SetActive(false);
        flyArm[1].SetActive(false);
        */
        seed[0].SetActive(false);
        seed[1].SetActive(false);
        nipper[0].SetActive(false);
        nipper[1].SetActive(false);
    }

    public void UseNipper()
    {
        StartCoroutine(IENipper());
    }

    private IEnumerator IENipper()
    {
        // 흔들기(왕복)
        for (int i = 0; i < 3; i++)
        {
            yield return RotateZ(15);
            yield return RotateZ(-15);
        }

        // 원래 각도로 되돌리기
        yield return RotateZ(0);
    }
    private IEnumerator RotateZ(float targetAngle)
    {
        Quaternion startRot = nipper[1].transform.rotation;
        Quaternion endRot = Quaternion.Euler(0, 0, targetAngle);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / 0.25f;
            nipper[1].transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }
    }
}
