using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunScipt : MonoBehaviour
{

    [Header("Keybinds")]
    public KeyCode reloadKey = KeyCode.R;

    [Header("Gun properties")]
    //for gun properties
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    //for reloading and ammo
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    //for muzzleFlash
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    public Animator animator;

    public Text ammoDisplay;


    void Start()
    {
        UpdateAmmoText();
        currentAmmo = maxAmmo;
    }


    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    void Update()
    {

        if (isReloading)
            return;

        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKey(reloadKey))
        {
            StartCoroutine(Reload());
            return;
        }
            

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        UpdateAmmoText();
    }


    IEnumerator Reload()
    {
        //reload when no more bullets

        isReloading = true;
        FindObjectOfType<AudioManager>().Play("AKM Rifle gun reload sound"); //call for gun reload sound
        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;

        UpdateAmmoText();
    }


    void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("AKM Rifle gunfire sound"); //call for gunfire sound
        
        muzzleFlash.Play();

        currentAmmo--;


        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            
            //check if target is actually target
            if(target != null)
            {
                target.Hit(damage);
      
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1f);    //prevents clutter in hierarchy by destroying unncessary game objects

        }
    }

    private void UpdateAmmoText()
    {
        ammoDisplay.text = $"{currentAmmo}/{maxAmmo}";
    }



}
