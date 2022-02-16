/**
 * 
 * Created By Jeremiah Underwood
 * 
 * Last Edited: 2/16/2022
 * Last Edited By: N/A
 * 
 * Description: Camera Follow
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    public static GameObject POI; //point of interest, (obejct following)
    [SerializeField] private float camZ;
    [SerializeField] private float easing = 0.05f;
    [SerializeField] private Vector3 minXY = Vector3.zero;

    private void Awake()
    {
        camZ = this.transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (POI != null)
        //{
        //Vector3 destination = POI.transform.position;
        Vector3 destination;
        if (POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            destination = POI.transform.position;
            if (POI.tag == "Projectile")
            {
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null;
                }
            }
        }
            destination.x = Mathf.Max(minXY.x, destination.x);
            destination.y = Mathf.Max(minXY.y, destination.y);
            destination = Vector3.Lerp(transform.position, destination, easing);

            destination.z = camZ;
            this.transform.position = destination;

            Camera.main.orthographicSize = destination.y + 10;
        //}
    }
}
