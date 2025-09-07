using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource effectSource;
    [SerializeField] private AudioSource scenerySource;

    [SerializeField] private Image image;
    [SerializeField] private Sprite[] sprite;
    private void Start()
    {
        Scenery("Title_Sound");
    }
    public void Scenery(string soundName)
    {
        scenerySource.clip = Resources.Load<AudioClip>(soundName);

        scenerySource.loop = true;
        scenerySource.Play();
    }

    public void Sound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
        
    }

    public void StopSound()
    {
        effectSource.Stop();
    }

    public AudioClip GetAudioClip(string soundName)
    {
        return Resources.Load<AudioClip>(soundName);
    }

    public void ChangeSound()
    {
        switch (effectSource.volume * 10)
        {
            case 0:
                effectSource.volume = 0.3f;
                scenerySource.volume = 0.2f;
                image.sprite = sprite[1];
                break;
            case 3:
                effectSource.volume = 0.6f;
                scenerySource.volume = 0.5f;
                image.sprite = sprite[2];
                break;
            case 6:
                effectSource.volume = 1f;
                scenerySource.volume = 0.8f;
                image.sprite = sprite[3];
                break;
            case 10:
                effectSource.volume = 0f;
                scenerySource.volume = 0f;
                image.sprite = sprite[0];
                break;

            default:
                break;
        }
    }
}
