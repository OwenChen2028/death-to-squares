using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    private SpriteRenderer sr;

    public Sprite lockedSprite;
    public Sprite unlockedSprite;
    public Sprite completedSprite;

    public bool unlocked;
    public bool completed;
    public bool trophy;
    public bool hundred;
    public int sceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("scene" + sceneIndex) == 1)
        {
            unlocked = true;
            completed = true;
        }
    }

    void LateUpdate()
    {
        if (!hundred)
        {
            if (transform.parent != null && transform.parent.GetComponent<ButtonScript>() != null && transform.parent.GetComponent<ButtonScript>().completed)
            {
                unlocked = true;
            }
        }
        else if (PlayerPrefs.GetInt("scene23") == 1 && PlayerPrefs.GetInt("scene12") == 1 && PlayerPrefs.GetInt("scene14") == 1 && PlayerPrefs.GetInt("scene16") == 1 && PlayerPrefs.GetInt("scene19") == 1 && PlayerPrefs.GetInt("scene24") == 1)
        {
            unlocked = true;
        }

        if (completed)
        {
            sr.sprite = completedSprite;
        }
        else if (unlocked)
        {
            sr.sprite = unlockedSprite;
        }
        else
        {
            sr.sprite = lockedSprite;
        }
    }

    void OnMouseDown()
    {
        if (unlocked)
        {
            SceneManager.LoadScene(sceneIndex);
            SoundManager.PlaySound("select");
        }
        else
        {
            SoundManager.PlaySound("error");
        }
    }
}
