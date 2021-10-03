using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LevelManager : MonoBehaviour
{
    public Animator transition;
    public RectTransform circle;

    public static LevelManager Instance;

    int snakeCount;
    Vector2 uiOffset;
    
    Image circleImage;

    public AudioSource thunkSound;
    public AudioSource hissSound;

    public AudioSource songSound;
    public float volumeShiftSpeed;

    void Awake()
    {
        if(FindObjectOfType<LevelManager>() != this) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);

        circleImage = circle.GetComponent<Image>();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftBracket))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1, LoadSceneMode.Single);
        }
        if(Input.GetKeyDown(KeyCode.RightBracket))
        {
           StartCoroutine(LoadNextLevel());
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
           StartCoroutine(RestartLevel());
        }

        songSound.volume = Mathf.PerlinNoise(0.5f, Time.time * volumeShiftSpeed) * 0.3f;
    }

    public void NextLevel()
    {
        StartCoroutine(LoadNextLevel());
    }

    public void AddSnake()
    {
        snakeCount++;
    }

    public void RemoveSnake(Vector3 snakePos)
    {
        PlayHiss();
        snakeCount--;
        if(snakeCount == 0)
        {
            Vector2 viewportPoint = Camera.main.WorldToViewportPoint(snakePos);
            circle.anchorMin = viewportPoint;
            circle.anchorMax = viewportPoint;
            circleImage.color = new Color(0.7f, 0.7f, 1f, 1f);
            StartCoroutine(LoadNextLevel());
        }
    }

    public void HitSheep(Vector3 sheepPos)
    {
        Vector2 viewportPoint = Camera.main.WorldToViewportPoint(sheepPos);
        circle.anchorMin = viewportPoint;
        circle.anchorMax = viewportPoint;
        circleImage.color = Color.black;
        StartCoroutine(RestartLevel());
    }

    public void PlayThunk(float volume)
    {
        thunkSound.volume = volume;
        thunkSound.Play();
    }
    public void PlayHiss()
    {
        hissSound.pitch = UnityEngine.Random.Range(0.8f,1.5f);
        hissSound.Play();
    }

    
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(1.0f);
        transition.SetTrigger("EndLevel");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        snakeCount = 0;
    }

    IEnumerator RestartLevel()
    {
        transition.SetTrigger("EndLevel");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        snakeCount = 0;
    }



}
