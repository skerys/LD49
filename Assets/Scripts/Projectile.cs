using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody body;
    public float minVelocity = 0.5f;
    public float minVelocityTime = 1.0f;

    float velocityTimer = 0f;

    public AudioSource rollSound;
    public AudioSource thunkSound;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(body.IsSleeping()){
            Destroy(this.gameObject);
        }

        if(transform.position.y < -20f)
        {
            Destroy(this.gameObject);
        }

        if(body.velocity.magnitude < minVelocity)
        {
            velocityTimer += Time.deltaTime;
            if(velocityTimer > minVelocityTime)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            velocityTimer = 0f;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        LevelManager.Instance.PlayThunk(col.impulse.magnitude / 20f);
        if(col.collider.gameObject.CompareTag("Wall")) return;
        DestroyableWeight dw = col.gameObject.GetComponent<DestroyableWeight>();
        if(dw != null)
        {
            Destroy(this.gameObject);
            dw.KillWeight(transform.position);
        }
    }

    void OnCollisionStay(Collision col)
    {
        rollSound.volume = body.velocity.magnitude / 30f;
    }

    void OnCollisionExit(Collision col)
    {
        rollSound.volume = 0f;
    }
}
