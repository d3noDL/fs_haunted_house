using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField] private AudioClip crackGlass, bodyThump, flatLine;
    public AudioClip heartBeat;


    private float rotationX;
    private LayerMask layerMask;
    private bool invOpen;

    public GameObject inv, trigger31, trigger32, secretDoor, demonBoss;
    public s_bed moveBed;
    public bool hurt = false;
    public float health = 0;
    public float deatherer = 0;
    public string currentSector;


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
        if (!isActive)
        {
            return;
        }
        HandleMovement();
        HandleLook();
        HandleInput();
        HandleAnimation();
        HandleHealth();
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
        rotationX = Mathf.Clamp(rotationX, -60, 60);
        cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, lookX * lookSensitivity, 0);
        
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!invOpen)
            {
                inv.SetActive(true);
                invOpen = true;
            }
            else
            {
                inv.SetActive(false);
                invOpen = false;
            }

            
        }
        
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 2f, LayerMask.GetMask("Default")))
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
                        
                case "BedroomDoor":
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (S_MAIN.i.hasBedroomKey)
                        {
                            StartCoroutine(ChangeRoom(
                                hit.transform.GetComponent<s_door>().connectedDoor.GetComponent<s_door>().spawn.transform
                                    .position,
                                hit.transform.GetComponent<s_door>().connectedDoor.GetComponent<s_door>().spawn.transform
                                    .rotation, hit.transform.gameObject));
                        }
                        else
                        {
                            ui.GetComponent<e_dialogue>().Talk("doorLocked");
                            S_MAIN.i.audioSource.PlayOneShot(S_MAIN.i.lockedDoor);
                        }
                    }
                    ui.GetComponent<s_ui>().SetPointer("Interact");
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

                case "Breaker":
                    if (Input.GetMouseButtonDown(0))
                    {
                        GameObject.Find("Breaker").GetComponent<e_breaker>().ToggleBreaker();
                    }
                    ui.GetComponent<s_ui>().SetPointer("Interact");
                    break;
                
                case "Twin":
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (!S_MAIN.i.hasNesController && !S_MAIN.i.hasMap)
                        {
                            ui.GetComponent<e_dialogue>().Talk("twinControllerLost");
                        }
                        else if (S_MAIN.i.hasNesController && !S_MAIN.i.hasMap)
                        {
                            ui.GetComponent<e_dialogue>().Talk("twinControllerFound");
                            S_MAIN.i.InventoryManager("i_map");
                            secretDoor.SetActive(true);
                            S_MAIN.i.RemoveFromInv("i_nes");
                        }
                        else if (S_MAIN.i.releaseMother && !S_MAIN.i.releaseDemon && !S_MAIN.i.hasNesController)
                        {
                            ui.GetComponent<e_dialogue>().Talk("twinGiveForDemon");
                            S_MAIN.i.InventoryManager("i_nes");
                        }
                        else if (S_MAIN.i.releaseDemon && !S_MAIN.i.releaseTwin)
                        {
                            ui.GetComponent<e_dialogue>().Talk("demonGiveController");
                            S_MAIN.i.releaseTwin = true;
                        }



                    }
                    ui.GetComponent<s_ui>().SetPointer("Talk");
                    break;
                
                case "DemonBoss":
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (S_MAIN.i.hasNesController)
                        {
                            ui.GetComponent<e_dialogue>().Talk("twinDemonReleased");
                            demonBoss.GetComponent<s_bossDemon>().Release();
                        }
                    }
                    ui.GetComponent<s_ui>().SetPointer("Interact");
                    break;
                
                case "Wardrobe":
                    if (Input.GetMouseButtonDown(0))
                    {
                        ui.GetComponent<e_dialogue>().Talk("tooHeavy");
                    }
                    ui.GetComponent<s_ui>().SetPointer("Check");
                    break;
                
                case "BedNotMove":
                    if (Input.GetMouseButtonDown(0))
                    {
                        ui.GetComponent<e_dialogue>().Talk("bedNotMove");
                    }
                    ui.GetComponent<s_ui>().SetPointer("Check");
                    break;
                
                case "BedMove":
                    if (Input.GetMouseButtonDown(0))
                    {
                        moveBed.MoveBed();
                        S_MAIN.i.audioSource.PlayOneShot(S_MAIN.i.bedMove);
                    }
                    ui.GetComponent<s_ui>().SetPointer("Interact");
                    break;
                
                case "Mom":
                    if (Input.GetMouseButtonDown(0))
                    {
                        ui.GetComponent<e_dialogue>().Talk("motherFirst");
                    }
                    ui.GetComponent<s_ui>().SetPointer("Talk");
                    break;
                
                case "Remains":
                    if (Input.GetMouseButtonDown(0))
                    {
                        ui.GetComponent<e_dialogue>().Talk("remains");
                    }
                    ui.GetComponent<s_ui>().SetPointer("Check");
                    break;
                    
                case "DoorPikovit":
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (!S_MAIN.i.hasPikovit)
                        {
                            ui.GetComponent<e_dialogue>().Talk("twinPikovitLost");
                        }
                        else
                        {
                            ui.GetComponent<e_dialogue>().Talk("twinPikovitFound");
                            trigger31.SetActive(false);
                            trigger32.SetActive(false);
                            S_MAIN.i.RemoveFromInv("i_pikovit");
                        }
                        
                    }
                    ui.GetComponent<s_ui>().SetPointer("Talk");
                    break;
                
                case "EntranceDoor":
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (!S_MAIN.i.hasKey)
                        {
                            ui.GetComponent<e_dialogue>().Talk("doorLocked");
                            S_MAIN.i.audioSource.PlayOneShot(S_MAIN.i.lockedDoor);
                        }
                        else if (S_MAIN.i.hasKey)
                        {
                            if (S_MAIN.i.releaseMother && S_MAIN.i.releaseDemon && S_MAIN.i.releaseTwin)
                            {
                                SceneManager.LoadScene("TrueEnding");
                            }
                            else if (S_MAIN.i.releaseMother && S_MAIN.i.releaseDemon && !S_MAIN.i.releaseTwin)
                            {
                                SceneManager.LoadScene("GoodEnding");
                            }
                            else if (S_MAIN.i.releaseMother && !S_MAIN.i.releaseDemon && !S_MAIN.i.releaseTwin)
                            {
                                SceneManager.LoadScene("BadEnding");
                            }
                            else if (!S_MAIN.i.releaseMother && !S_MAIN.i.releaseDemon && !S_MAIN.i.releaseTwin)
                            {
                                SceneManager.LoadScene("BadEnding");
                            }
                        }
                        
                    }
                    ui.GetComponent<s_ui>().SetPointer("Interact");
                    break;

                case "KeyItem":
                    if (Input.GetMouseButtonDown(0))
                    {
                        ui.GetComponent<e_dialogue>().Talk("item" + hit.transform.name);
                        S_MAIN.i.InventoryManager(hit.transform.name);
                        hit.transform.gameObject.SetActive(false);
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
            GetComponentInChildren<Animator>().SetBool("isMoving", true);
        }
        else
        {
            GetComponentInChildren<Animator>().SetBool("isMoving", false);
        }
    }

    public void HandleHealth()
    {
        if (hurt)
        {
            health += Time.deltaTime / 4;
        }
        else
        {
            health -= Time.deltaTime;
        }
        
        ui.GetComponent<s_ui>().deatherer.color = new Color(1, 1, 1, health);
        cam.GetComponent<Camera>().fieldOfView = 60 - (health * 20);
        
        if (health < 0)
        {
            health = 0;
        }

        if (health >= 1)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator ChangeRoom(Vector3 pos, Quaternion rot, GameObject door)
    {
        if (door.GetComponent<s_door>().isLocked == false)
        {
            GetComponentInChildren<Animator>().SetBool("isMoving", false);
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
            Debug.Log("The door is locked!");
            yield break;
        }
        
    }

    IEnumerator Death()
    {
        ui.GetComponent<s_ui>().crackImage.SetActive(true);
        S_MAIN.i.StopMusicI();
        isActive = false;
        audioSource.PlayOneShot(crackGlass);
        audioSource.PlayOneShot(flatLine);
        yield return new WaitForSeconds(1.5f);
        audioSource.PlayOneShot(bodyThump);
        yield return new WaitForSeconds(2);
        ui.GetComponent<s_ui>().Fader(false);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Menu");

    }

    private void OnTriggerEnter(Collider other)
    {
        currentSector = other.name;
        if (other.CompareTag("Demon"))
        {
            hurt = true;
            audioSource.clip = heartBeat;
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Demon"))
        {
            hurt = false;
            audioSource.Stop();
        }
    }

    #endregion
}
