using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PistolScript : MonoBehaviour
{

    [Header("Keybinds")]
    public KeyCode reloadKey = KeyCode.R;

    [Header("Gun properties")]
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    public Rigidbody rb;

    [Header("Reloading and ammo")]
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool pistolIsReloading = false;


    [Header("Animations and effects")]
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffectGO;
    public GameObject bloodEffectGO;
    public Text ammoDisplay;
    public Animator animator;
    public TrailRenderer bulletTrail;


    private float nextTimeToFire = 0f;




    void Start()
    {
        UpdateAmmoText();
        currentAmmo = maxAmmo;

    }


    void OnEnable()
    {
        pistolIsReloading = false;
        animator.SetBool("PistolReloading", false);
    }

    void Update()
    {
        if (pistolIsReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(PistolReload());
            return;
        }

        if (Input.GetKey(reloadKey))
        {
            StartCoroutine(PistolReload());
            return;
        }


        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        UpdateAmmoText();
    }


    IEnumerator PistolReload()
    {
        //reload when no more bullets

        pistolIsReloading = true;
        FindObjectOfType<AudioManager>().Play("AKM Rifle gun reload sound"); //call for gun reload sound
        Debug.Log("Reloading...");

        animator.SetBool("PistolReloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("PistolReloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        pistolIsReloading = false;
        
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
            TrailRenderer trail = Instantiate(bulletTrail, fpsCam.transform.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));
            Destroy(trail.gameObject, 1f);

            Debug.Log(hit.transform.name);

            MainTarget target = hit.transform.GetComponent<MainTarget>();
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            TargetPractice rangeTarget = hit.transform.GetComponent<TargetPractice>(); //lobby targets

            //check if target is actually target
            if (target != null)
            {
                target.Hit(damage);

            }
            //check if enemy is actually enemy
            else if (enemy != null)
            {
                enemy.Hit(damage);
                hit.rigidbody.AddForce(hit.normal * impactForce);

                GameObject bloodGO = Instantiate(bloodEffectGO, hit.point, Quaternion.LookRotation(hit.normal)); //blood effect
                Destroy(bloodGO, 1f); //prevents clutter in hierarchy by destroying unncessary game objects

            }
            //check if targets at target practice is actually target
            else if (rangeTarget != null)
            {
                rangeTarget.Hit(damage);
            }

            GameObject impactGO = Instantiate(impactEffectGO, hit.point, Quaternion.LookRotation(hit.normal)); //bullet effect
            Destroy(impactGO, 1f);    //prevents clutter in hierarchy by destroying unncessary game objects


        }
    }

    //creating bullet trail
    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
            time += Time.deltaTime / Trail.time;

            yield return null;
        }
        Trail.transform.position = Hit.point;


    }


    private void UpdateAmmoText()
    {
        ammoDisplay.text = $"{currentAmmo}/{maxAmmo}";
    }



}
