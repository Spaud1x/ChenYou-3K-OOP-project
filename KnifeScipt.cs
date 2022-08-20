using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KnifeScipt : MonoBehaviour
{

    [Header("Keybinds")]
    public KeyCode inspectKey = KeyCode.Y;

    [Header("Gun properties")]
    //for gun properties
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    //for reloading and ammo
    public int maxAmmo = 10;
    private int currentAmmo;
    public float slicingTime = 1f;
    private bool isSlicing = false;

    //for inspecting
    public float inspectingTime = 1f;
    private bool isInspecting = false;

    

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
        isSlicing = false;
        animator.SetBool("Slicing", false);
        isInspecting = false;
        animator.SetBool("Inspecting", false);
    }



    void Update()
    {
        if (isInspecting)
            return;

        if (isSlicing)
            return;

        if (Input.GetKey(inspectKey))
        {
            StartCoroutine(Inspect());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {

            nextTimeToFire = Time.time + 1f / fireRate;
            Slice();
            StartCoroutine(Knife());
            return;

           

        }
      
        UpdateAmmoText();
    }

    IEnumerator Inspect()
    {
        isInspecting = true;
        animator.SetBool("Inspecting", true);

        yield return new WaitForSeconds(inspectingTime - .25f);
        animator.SetBool("Inspecting", false);
        yield return new WaitForSeconds(inspectingTime - .25f);

        isInspecting = false;
    }

    IEnumerator Knife()
    {
        
        isSlicing = true;
        FindObjectOfType<AudioManager>().Play("Knife sound"); //call for knife sound
        Debug.Log("Shing Shing Kaching...");

        animator.SetBool("Slicing", true);

        yield return new WaitForSeconds(slicingTime - .25f);
        animator.SetBool("Slicing", false);
        yield return new WaitForSeconds(.25f);

        isSlicing = false;

    }



    void Slice()
    {
        

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            //check if target is actually target
            if (target != null)
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
