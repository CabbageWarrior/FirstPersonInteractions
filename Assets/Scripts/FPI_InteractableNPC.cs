using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using DG.Tweening;

[RequireComponent(typeof(AICharacterControl))]
public class FPI_InteractableNPC : MonoBehaviour
{
    private AICharacterControl thisAICharacterControl;
    private NavMeshAgent thisNavMeshAgent;
    private Transform previousTarget;
    private Vector3 previousEulerAngles;
    private GameObject strSpeak;

    private bool isTarget = false;

    public bool IsTarget
    {
        get
        {
            return isTarget;
        }

        set
        {
            isTarget = value;
        }
    }

    private void Start()
    {
        thisAICharacterControl = GetComponent<AICharacterControl>();
        thisNavMeshAgent = GetComponent<NavMeshAgent>();

        transform.Find("BillboardElements").Find("Name").GetComponent<TextMesh>().text = GetComponent<VIDE_Assign>().alias;

        strSpeak = transform.Find("BillboardElements").Find("StrSpeak").gameObject;
    }

    private void Update()
    {
        if (isTarget && !strSpeak.activeInHierarchy)
            strSpeak.SetActive(true);
        else if (!isTarget && strSpeak.activeInHierarchy)
            strSpeak.SetActive(false);
    }

    public void DialogueStart(Transform playerTransform)
    {
        previousTarget = thisAICharacterControl.target;
        previousEulerAngles = transform.eulerAngles;

        // Gets a vector that points from the player's position to the target's.
        Vector3 heading = playerTransform.position - transform.position;

        float distance = heading.magnitude;
        Vector3 direction = heading / distance; // This is now the normalized direction.

        GameObject TransformObject = new GameObject();
        TransformObject.transform.position = transform.position + direction;

        thisAICharacterControl.SetTarget(transform);

        transform.DOLookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z), .5f);
    }

    public void DialogueEnd()
    {
        transform.DORotate(previousEulerAngles, .5f);
        previousEulerAngles = Vector3.zero;

        thisAICharacterControl.SetTarget(previousTarget);
        previousTarget = null;
    }
}
