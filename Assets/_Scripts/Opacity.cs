using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opacity : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //GetComponent<MeshRenderer>().material.color = new Color(a=0.75f);
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
    }
}
