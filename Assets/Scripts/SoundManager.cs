using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public static AudioClip death;
    public static AudioClip error;
    public static AudioClip jump;
    public static AudioClip select;
    public static AudioClip victory;

    private static AudioSource audioSrc;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = (PlayerPrefs.GetInt("muted") == 0) ? 1:0;

        death = Resources.Load<AudioClip>("death");
        error = Resources.Load<AudioClip>("error");
        jump = Resources.Load<AudioClip>("jump");
        select = Resources.Load<AudioClip>("select");
        victory = Resources.Load<AudioClip>("victory");

        audioSrc = GetComponents<AudioSource>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            AudioListener.volume = !(AudioListener.volume == 1) ? 1:0;
            PlayerPrefs.SetInt("muted", !(AudioListener.volume == 1) ? 1:0);
            PlayerPrefs.Save();
        }
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "death":
                audioSrc.PlayOneShot(death, 0.7f);
                break;
            case "error":
                audioSrc.PlayOneShot(error, 0.4f);
                break;
            case "jump":
                audioSrc.PlayOneShot(jump, 0.3f);
                break;
            case "select":
                audioSrc.PlayOneShot(select, 0.3f);
                break;
            case "victory":
                audioSrc.PlayOneShot(victory, 0.7f);
                break;
        }
    }
}
