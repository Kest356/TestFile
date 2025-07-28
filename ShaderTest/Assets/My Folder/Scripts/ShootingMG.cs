using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class ShootingMG : MonoBehaviour {

    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public AudioSource aud;
    public GameObject eft;
    private float nextFire;

    void Update ()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            aud.Play();
            Instantiate(eft, shotSpawn.position, shotSpawn.rotation);
        }
    }
}
