using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeManager : MonoBehaviour
{
    [SerializeField]
    private float traumaDecayRate = 1f;
    [SerializeField]
    private Camera camera = default;

    [SerializeField]
    private float maxAngle = 10f;
    [SerializeField]
    private float maxOffset = 0.3f;
    [SerializeField]
    private float noiseRate = 10f;

    private float trauma;
    private float shakeAmount;

    private Vector3 initialPos;
    private Quaternion initialRot;

    private float rotationSeed;
    private Vector2 translationSeed;

    private bool isShaking;

    public static ScreenShakeManager Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        initialPos = camera.transform.localPosition;
        initialRot = camera.transform.localRotation;

        rotationSeed = Random.Range(0, 10000);
        translationSeed.x = Random.Range(0, 10000);
        translationSeed.y = Random.Range(0, 10000);
        
    }

    public void AddTrauma(float amount)
    {
        trauma += amount;
        trauma = Mathf.Clamp(trauma, 0.0f, 1.0f);
    }

    void Update()
    {

        if(trauma > 0.0f)
        {
            isShaking = true;
        } 

        if (isShaking)
        {
            shakeAmount = trauma * trauma;

            //last part maps 1D perlin noise from [0;1] to [-1;1]
            float angle = maxAngle * shakeAmount * (2.0f * Mathf.PerlinNoise((noiseRate * Time.time) + rotationSeed, 0.5f) - 1.0f);
            float posX = maxOffset * shakeAmount * (2.0f * Mathf.PerlinNoise((noiseRate * Time.time) + translationSeed.x, 0.5f) - 1.0f);
            float posY = maxOffset * shakeAmount * (2.0f * Mathf.PerlinNoise((noiseRate * Time.time) + translationSeed.y, 0.5f) - 1.0f);


            camera.transform.localRotation = Quaternion.Euler(initialRot.eulerAngles.x, initialRot.eulerAngles.y, initialRot.eulerAngles.z + angle);
            camera.transform.localPosition = new Vector3(initialPos.x + posX, initialPos.y + posY, initialPos.z);

            trauma -= traumaDecayRate * Time.deltaTime;

            if(trauma <= 0.0f)
            {
                trauma = 0.0f;
                isShaking = false;
                camera.transform.localRotation = initialRot;
                camera.transform.localPosition = initialPos;
            }
        }
    }

    [ContextMenu("add some trauma")]
    public void TraumaTest()
    {
        AddTrauma(1.0f);
    }
}