using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    #region reference
    public GunSystem gunSystem;
    public Transform defaultPosition;
    public MouseLook mouseLook;
    #endregion

    #region variables
    public float dropForwardForce, dropUpwardForce;
    public bool equipped;
    #endregion

    private void Start()
    {
        //Setup
        if (!equipped)
        {
            gunSystem.enabled = false;
            mouseLook.slotFull = false;
        }
        if (equipped)
        {
            gunSystem.enabled = true;
            mouseLook.slotFull = true;
        }
    }

    #region input

    public void OnDropPressed()
    {
        if (equipped)
        {
            Drop();
        }
    }
    #endregion

    #region equip / drop
    public void PickUp(GameObject rayHittedGameObject)
    {
        //set bools
        equipped = true;
        mouseLook.slotFull = true;
        // MouseLook.isPickingUp = false;

        //Remove rigidbody and BoxCollider
        Destroy(rayHittedGameObject.GetComponent<Rigidbody>());
        rayHittedGameObject.GetComponent<Collider>().enabled = false;

        // set that weapon as weapon swing and gun system
        InputManager.gunSystem = rayHittedGameObject.GetComponent<GunSystem>();
        MouseLook.weaponSwing = rayHittedGameObject.GetComponent<WeaponSwing>();
        GunSystem.weaponIsActive = true;
        GunSystem.turnOffCanvas = false;
        rayHittedGameObject.GetComponent<GunSystem>().enabled = true;
        rayHittedGameObject.GetComponent<WeaponSwing>().enabled = true;

        //Make weapon a child and move it to default position
        Vector3 scale = rayHittedGameObject.transform.localScale;
        rayHittedGameObject.transform.SetParent(defaultPosition, false);
        rayHittedGameObject.transform.localPosition = Vector3.zero;
        rayHittedGameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        rayHittedGameObject.transform.localScale = scale;

    }

    private void Drop()
    {
        Debug.Log("dropinn");
        // isDropping = false;
        Vector3 scale = transform.localScale;
        equipped = false;
        mouseLook.slotFull = false;
        transform.SetParent(null);
        transform.localScale = scale;

        // Disable script
        gameObject.GetComponent<GunSystem>().enabled = false;
        gameObject.GetComponent<WeaponSwing>().enabled = false;
        InputManager.gunSystem = null;
        InputManager.pickUpController = null;
        MouseLook.weaponSwing = null;
        GunSystem.turnOffCanvas = true;
        GunSystem.weaponIsActive = false;

        // Make Rigidbody and BoxCollider
        gameObject.AddComponent<Rigidbody>();
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        transform.gameObject.GetComponent<Collider>().isTrigger = false;
        transform.gameObject.GetComponent<Collider>().enabled = true;

        // AddForce
        rb.AddForce(mouseLook.playerCamera.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(mouseLook.playerCamera.up * dropUpwardForce, ForceMode.Impulse);
        // Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);
    }
    #endregion

}