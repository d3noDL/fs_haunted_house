using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

public class s_player : MonoBehaviour
{
    #region Variables
    
    public bool isActive;
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private GameObject cam, ui;
    [SerializeField] private CharacterController cc;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject[] pointers;

    private float rotationX;
    private LayerMask layerMask;
    

    #endregion
    
    void Start()
    {
        ui.SetActive(true);
        ui.GetComponent<s_ui>().Fader(true);
        
        cc = GetComponent<CharacterController>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        if (isActive)
        {
            HandleMovement();
            HandleLook();
            HandleInput();
            HandleAnimation();
        }
    }

    #region FunctionsAndCoroutines
    
    public void HandleMovement()
    {
        var moveZ = Input.GetAxisRaw("Vertical");
        var moveX = Input.GetAxisRaw("Horizontal");
        
        Vector3 moveDirection =
            (transform.TransformDirection(Vector3.forward) * moveZ ) + (transform.TransformDirection(Vector3.right) * moveX );
        
        cc.Move(moveDirection.normalized * movementSpeed * Time.deltaTime);
    }

    public void HandleLook()
    {
        var lookY = Input.GetAxis("Mouse Y");
        var lookX = Input.GetAxis("Mouse X");
        
        rotationX += -lookY * lookSensitivity;
        rotationX = Mathf.Clamp(rotationX, -45, 45);
        cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, lookX * lookSensitivity, 0);
        
    }

    public void HandleInput()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1, LayerMask.GetMask("Default")))
        {
            switch (hit.transform.tag)
            {
                case "Door":
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("It's a door!");
                        StartCoroutine(ChangeRoom(
                            hit.transform.GetComponent<s_door>().connectedDoor.GetComponent<s_door>().spawn.transform
                                .position,
                            hit.transform.GetComponent<s_door>().connectedDoor.GetComponent<s_door>().spawn.transform
                                .rotation, hit.transform.gameObject));
                    }
                    ui.GetComponent<s_ui>().SetPointer("Interact");
                    break;
                        

                case "GrandfatherClock":
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("It keeps on ticking, it's driving me crazy");
                    }
                    ui.GetComponent<s_ui>().SetPointer("Check");
                    break;
                    
                case "LightSwitch":
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("It's a lightswitch!");
                        hit.transform.GetComponent<s_lightswitch>().Toggle();
                        hit.transform.GetComponent<AudioSource>().Play();
                    }
                    ui.GetComponent<s_ui>().SetPointer("Interact");
                    break;
                    
                default:
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("It's something");
                    }
                    ui.GetComponent<s_ui>().SetPointer("Default");
                    break;
                
            }
        }
        else
        {
            ui.GetComponent<s_ui>().SetPointer("Default");
        }
    }

    public void HandleAnimation()
    {
        if (cc.velocity.magnitude > 0.1f)
        {
            GetComponentInChildren<Animation>().Play();
        }
        else
        {
            GetComponentInChildren<Animation>().Stop();
        }
    }

    IEnumerator ChangeRoom(Vector3 pos, Quaternion rot, GameObject door)
    {
        if (door.GetComponent<s_door>().isLocked == false)
        {
            isActive = false;
            door.GetComponent<s_door>().Open();
            ui.GetComponent<s_ui>().Fader(false);
            yield return new WaitForSeconds(1);
            transform.position = pos;
            transform.rotation = rot;
            yield return new WaitForSeconds(1);
            door.GetComponent<s_door>().Close();
            ui.GetComponent<s_ui>().Fader(true);
            isActive = true;
            yield break;
        }
        else
        {
            door.GetComponent<s_door>().Locked();
            yield break;
        }
        
    }

    
    
    
    #endregion
}
