using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float maxRotationSpeed = 30f;
    public float rotationAcceleration = 30f;
    public Vector2 spawnOffset;
    public float projectileForce = 5f;
    public Rigidbody projectile;

    public Transform wheelR;
    public Transform wheelL;

    float rotationSpeed = 0f;
    bool canShoot = true;
    Rigidbody proj = null;

    public AudioSource popSound;
    public GameObject ballRetrievalEffect;
        // Update is called once per frame
    void Update()
    {

        float rotSign = 0f;
        if(Input.GetKey(KeyCode.A)){
            rotSign = -1f;
            rotationSpeed += rotationAcceleration * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            rotSign = 1f;
            rotationSpeed += rotationAcceleration * Time.deltaTime;
        }
        else
        {
            rotationSpeed = 0f;
        }

        if(rotationSpeed >= maxRotationSpeed)
        {
            rotationSpeed = maxRotationSpeed;
        }

        transform.Rotate(rotSign * Vector3.up * rotationSpeed * Time.deltaTime);
        wheelL.Rotate(rotSign * transform.right * 2f*rotationSpeed * Time.deltaTime, Space.World);
        wheelR.Rotate(-rotSign * transform.right * 2f*rotationSpeed * Time.deltaTime, Space.World);

        if(!canShoot && proj == null)
        {
            canShoot = true;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(canShoot)
            {
                canShoot = false;
                proj = Instantiate(projectile, transform.position + transform.forward * spawnOffset.x + transform.up * spawnOffset.y, Quaternion.identity);
                proj.AddForce(transform.forward * projectileForce);
                popSound.Play();
            }
            else
            {
                var effect = Instantiate(ballRetrievalEffect, proj.gameObject.transform.position, ballRetrievalEffect.transform.rotation);
                Destroy(effect, 1f);
                Destroy(proj.gameObject);
            }

        }
    }
}
