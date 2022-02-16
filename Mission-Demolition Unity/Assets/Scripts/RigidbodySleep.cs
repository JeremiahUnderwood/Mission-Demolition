/**
 * 
 * Created By Jeremiah Underwood
 * 
 * Last Edited: N/A
 * Last Edited By: N/A
 * 
 * Description: Keeps Castle from moving first frame and some other stuff prolly IDK yet
 * 
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodySleep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.Sleep();
    }
}
