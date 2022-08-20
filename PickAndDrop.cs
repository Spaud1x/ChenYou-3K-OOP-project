using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAndDrop : MonoBehaviour
{
    public GunScipt gunScript;
    public Rigidbody rb;
    public BoxCollider coll;

    [Header("Object positions")]
    public Transform player;
    public Transform originalGunPos;
    public Transform gunEquipped;
    public Transform gunContainer;
    public Transform fpsCam;

    [Header("Value input")]
    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;



    private void Start()
    {
        //Setup
        if (!equipped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.enabled = true;
        }

        if (equipped)
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            coll.enabled = false;
            slotFull = true;
        }

    }

    private void Update()
    {

        // Check if player is in range and "F" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.F) && !slotFull)
        {
            PickUp();
        }

        //Drop if equipped and "G" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.G))
        {
            Drop();
        }

    }

    void SelectWeapon()
    {
        foreach (Transform weapon in transform) //referring to the guns under weaponholder
        {
            if (equipped == true)
                weapon.gameObject.SetActive(true);

        }
    }


    void PickUp()
    {
        coll.enabled = false; //disable collider when equipped if not gun will move arnd
        rb.isKinematic = true; //disable rigidbody when equipped if not gun will move arnd


        equipped = true;
        slotFull = true;

        SelectWeapon();

        //Make weapon a child of camera and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = gunEquipped.position;
        transform.localRotation = gunEquipped.rotation;
        transform.localScale = gunEquipped.localScale;

        gunScript.enabled = true;

    }

    void Drop()
    {
        coll.enabled = true;
        rb.isKinematic = false;

        equipped = false;
        slotFull = false;

        //Set parent to null
        gunContainer.position = originalGunPos.position;
        transform.SetParent(null);



        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //Add force
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        gunScript.enabled = false;
    }

}
