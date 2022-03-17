using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FreezeGun : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float timeToDisappear = 5f;
    public Rigidbody bullet;
    public Rigidbody bullet2;
    public Transform barrel;
    public GameObject flash;
    public float flashToDissappear = 1.0f;
    public float ftimeToDisappear = 0.01f;
    public bool isShooting;


    public float chargeTime = 3.0f;
    public float charge = 0.0f;

    public Camera fpsCam;
    public float range = 100f;

    public NavMeshAgent agent;


    public int maxAmmo = 10;
    public int currentAmmo;



    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();



        GunShoot();




        if (Input.GetButton("Fire2") || (Input.GetButtonDown("Fire1")))
        {
            isShooting = true;

        }
        else
        {
            isShooting = false;
        }

    }


    public void Fire()
    {
        if (Input.GetButtonDown("Fire1") && !isShooting)
        {
            Rigidbody bulletClone = (Rigidbody)Instantiate(bullet, barrel.position, transform.rotation);
            bulletClone.AddForce(transform.forward * bulletSpeed);
            StartCoroutine(DisappearCoroutine(bulletClone.gameObject));
            GameObject flashClone = (GameObject)Instantiate(flash, barrel.position, transform.rotation);
            StartCoroutine(DisappearflashCoroutine(flashClone.gameObject));
        }
    }

    void GunShoot()
    {
        if (Input.GetButton("Fire2"))
        {
            charge += Time.deltaTime;

            if (Input.GetButtonUp("Fire2"))
            {
                charge = 0;

            }
            if (charge >= chargeTime && !isShooting)
            {
                if (currentAmmo <= 0)
                {
                    return;
                }
                currentAmmo--;
                Rigidbody bulletClone = (Rigidbody)Instantiate(bullet2, barrel.position, transform.rotation);
                bulletClone.AddForce(transform.forward * bulletSpeed);
                charge = 0;
                StartCoroutine(DisappearCoroutine(bulletClone.gameObject));
                GameObject flashClone = (GameObject)Instantiate(flash, barrel.position, transform.rotation);
                StartCoroutine(DisappearflashCoroutine(flashClone.gameObject));
            }
        }
    }

    private IEnumerator DisappearCoroutine(GameObject bulletToDisappear)
    {
        yield return new WaitForSeconds(timeToDisappear);
        Destroy(bulletToDisappear);
    }

    private IEnumerator DisappearflashCoroutine(GameObject flashoDissappear)
    {
        yield return new WaitForSeconds(ftimeToDisappear);
        Destroy(flashoDissappear);
    }
}