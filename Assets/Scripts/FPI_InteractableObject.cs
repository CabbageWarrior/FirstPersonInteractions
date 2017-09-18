using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VIDE_Assign))]
public class FPI_InteractableObject : MonoBehaviour {
    protected bool isTarget = false;
    protected GameObject strSpeak;

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
    // Use this for initialization
    protected virtual void Start ()
    {
        strSpeak = transform.Find("BillboardElements").Find("StrSpeak").gameObject;

        transform.Find("BillboardElements").Find("Name").GetComponent<TextMesh>().text = GetComponent<VIDE_Assign>().alias + "-OBJ";
    }

    // Update is called once per frame
    protected virtual void Update () {
        if (isTarget && !strSpeak.activeInHierarchy)
            strSpeak.SetActive(true);
        else if (!isTarget && strSpeak.activeInHierarchy)
            strSpeak.SetActive(false);
    }
}
