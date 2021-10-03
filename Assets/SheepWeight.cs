using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepWeight : DestroyableWeight
{

    Rigidbody body;
    bool running = false;
    bool rotating = false;
    float rotationLeft = 180f;

    public float runningSpeed = 2f;
    public float rotationSpeed = 180f;
    public GameObject model = default;
    public float bobbingAmount = 1f;
    public float bobbingFrequency = 3f;
    public AudioSource runSound;
    public AudioSource baaSound;
    public AudioClip[] baaClips;


    public void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if(running || rotating)
        {
            model.transform.localPosition = new Vector3(0f, Mathf.Sin(Time.time * bobbingFrequency) * bobbingAmount, 0f);
            body.isKinematic = false;
        }
        else{
            model.transform.localPosition = Vector3.zero;
            body.isKinematic = true;
        }
        if(running)
        {
            transform.Translate(transform.forward*runningSpeed*Time.deltaTime, Space.World);
        }
        if(rotating)
        {
            float rotAmount = rotationSpeed* Time.deltaTime;
            rotationLeft -= rotAmount;
            transform.Rotate(Vector3.up * rotAmount);

            if(rotationLeft<0f)
            {
                transform.Rotate(Vector3.up * -rotationLeft);
                rotationLeft = 180f;
                rotating = false;
                runSound.Stop();
            }
        }

        

        if(body.velocity.sqrMagnitude > 1f)
        {
            transform.parent = null;
            GetComponent<Collider>().isTrigger = true;
            LevelManager.Instance.HitSheep(transform.position);
            Destroy(gameObject, 5.0f);
            Destroy(this);
        }
    }

    public override void KillWeight(Vector3 projectilePos)
    {
        runSound.Play();
        baaSound.clip = baaClips[Random.Range(0, baaClips.Length)];
        baaSound.pitch = Random.Range(0.8f, 1.2f);
        baaSound.Play();
        running = true;
        Debug.Log("hit");
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Wall"))
        {
            running = false;
            rotating = true;
            rotationLeft = 180f;
        }
    }
}
