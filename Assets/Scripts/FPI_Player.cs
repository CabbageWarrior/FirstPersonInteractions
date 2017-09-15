using UnityEngine;
using System.Collections;
using VIDE_Data;
using System;

[RequireComponent(typeof(UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl))]
public class FPI_Player : MonoBehaviour
{
    //This script handles player movement and interaction with other NPC game objects

    //Reference to our diagUI script for quick access
    public exampleUI diagUI;
    public QuestChartDemo questUI;

    [Header("Raycast")]
    public GameObject raycastStart;

    private UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl thirdPersonUserControl;
    private Transform currentNPC;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        thirdPersonUserControl = GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>();
    }

    void Update()
    {
        //Only allow player to move and turn if there are no dialogs loaded
        if (!VD.isActive && !thirdPersonUserControl.CanMove)
        {
            thirdPersonUserControl.CanMove = true;
            if (currentNPC != null)
            {
                currentNPC.GetComponent<FPI_InteractableNPC>().DialogueEnd();
                currentNPC = null;
            }
        }
        else if (VD.isActive && thirdPersonUserControl.CanMove)
        {
            thirdPersonUserControl.CanMove = false;
        }
        //Interact with NPCs when hitting spacebar
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }

        //Hide/Show cursor
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.visible = !Cursor.visible;
            if (Cursor.visible)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }

    //Casts a ray to see if we hit an NPC and, if so, we interact
    void TryInteract()
    {
        RaycastHit rHit;
        
        if (Physics.Raycast(raycastStart.transform.position, raycastStart.transform.forward, out rHit, 2))
        {
            //In this example, we will try to interact with any collider the raycast finds

            //Lets grab the NPC's DialogueAssign script... if there's any
            VIDE_Assign assigned;
            if (rHit.collider.GetComponent<VIDE_Assign>() != null)
            {
                assigned = rHit.collider.GetComponent<VIDE_Assign>();
            }
            else return;


            if (!VD.isActive)
            {
                //... and use it to begin the conversation
                if (assigned.alias == "NonDialogue")
                {
                    questUI.CallQuestChart();
                }
                else
                {
                    currentNPC = assigned.transform;
                    currentNPC.GetComponent<FPI_InteractableNPC>().DialogueStart(transform);
                    diagUI.Begin(assigned);
                }
            }
            else
            {
                //If conversation already began, let's just progress through it
                if (assigned.alias == "NonDialogue")
                {
                    questUI.CallQuestChart();
                }
                else
                {
                    diagUI.CallNext();
                }
            }
        }
    }
}
