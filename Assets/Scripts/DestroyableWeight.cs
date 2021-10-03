using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWeight : Weight
{
    


    public virtual void KillWeight(Vector3 projectilePos)
    {
        transform.parent = null;
        GetComponent<Collider>().isTrigger = true;
        var rb = gameObject.AddComponent<Rigidbody>();

        Vector3 outwardsDir = transform.position - projectilePos;
        outwardsDir = outwardsDir.normalized;
        rb.AddForce(outwardsDir * 6f + Vector3.up * 12f, ForceMode.VelocityChange);
        rb.angularVelocity = Random.insideUnitSphere * 10f;

        Destroy(gameObject, 5.0f);
        Destroy(this);
    }
}
