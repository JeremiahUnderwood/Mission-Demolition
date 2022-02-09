using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    public static GameObject POI; //point of interest, (obejct following)
    [SerializeField] private float camZ;

    private void Awake()
    {
        camZ = this.transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (POI != null)
        {
            Vector3 destination = POI.transform.position;

            destination.z = camZ;
            this.transform.position = destination;
        }
    }
}
