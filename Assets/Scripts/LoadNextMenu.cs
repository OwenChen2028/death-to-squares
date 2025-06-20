using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextMenu : MonoBehaviour
{
    public int sceneIndex;
    public bool trophy;
    public bool start;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0) || (trophy && Input.GetKeyDown(KeyCode.Q)))
        {
            SceneManager.LoadScene(sceneIndex);
            SoundManager.PlaySound("select");
        }
        if (start && Input.GetKeyDown(KeyCode.Q))
        {
            //Application.Quit();
        }
    }
}
