using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] GameObject title;
    [SerializeField] GameObject prolog;
    [SerializeField] AudioClip audioSource;

    [SerializeField] GameObject[] cutScene;
    [SerializeField] GameObject blackSreen;

    private void Start()
    {
        audioSource = Resources.Load<AudioClip>("UI_Click");

        prologSound1 = Resources.Load<AudioClip>("Prolog_Sound1");
        prologSound2 = Resources.Load<AudioClip>("Prolog_Sound2");
    }
    public void StartButton()
    {
        AudioManager.Instance.Sound(audioSource);
        title.SetActive(false);
        prolog.SetActive(true);

        // ÄÆ½Å Àç»ý
        StartCoroutine(IEProlog());
    }

    public void Start_1Stage()
    {
        AudioManager.Instance.Sound(audioSource);

        SceneManager.LoadScene("1Stage");
    }


    [SerializeField] private AudioClip prologSound1;
    [SerializeField] private AudioClip prologSound2;
    public IEnumerator IEProlog()
    {
        prolog.SetActive(true);
        yield return new WaitForSeconds(1.5f);


        for (int i = 0; i < cutScene.Length; i++)
        {
            if (i == 4)
            {
                AudioManager.Instance.Sound(prologSound1);
            }

            cutScene[i].SetActive(true);

            yield return new WaitForSeconds(1.5f);
            
            if (i == 5)
            {
                AudioManager.Instance.StopSound();

            }
        }

        prolog.SetActive(false);
        blackSreen.SetActive(true);
        
        yield return new WaitForSeconds(2.5f);

        Start_1Stage();
    }

    private void OnDisable()
    {
        AudioManager.Instance.Scenery(null);
    }

    public void SoundChange()
    {
        AudioManager.Instance.Sound(audioSource);
        AudioManager.Instance.ChangeSound();
    }
}
