/**
 * 
 * Created By Jeremiah Underwood
 * 
 * Last Edited: N/A
 * Last Edited By: N/A
 * 
 * Description: Cloud spawning
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{

    [SerializeField] private int numberOfClouds = 40;
    [SerializeField] private GameObject cloudPrefab;
    [SerializeField] private Vector3 cloudPositionMin = new Vector3(-50, -5, 10);
    [SerializeField] private Vector3 cloudPositionMax = new Vector3(150, 100, 10);
    [SerializeField] float cloudScaleMin = 1;
    [SerializeField] float cloudScaleMax = 3;
    [SerializeField] float cloudSpeedMultiplier = 0.5f;

    private GameObject[] cloudInstances;


    private void Awake()
    {
        cloudInstances = new GameObject[numberOfClouds];
        GameObject anchor = GameObject.Find("CloudAnchor");
        GameObject cloud;

        for (int i = 0; i < numberOfClouds; i++)
        {
            cloud = Instantiate<GameObject>(cloudPrefab);

            Vector3 cPos = Vector3.zero;                                         //positioncloud
            cPos.x = Random.Range(cloudPositionMin.x, cloudPositionMax.x);
            cPos.y = Random.Range(cloudPositionMin.y, cloudPositionMax.y);

            float scaleU = Random.value;                                          //Scale clouds
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);

            cPos.y = Mathf.Lerp(cloudPositionMin.y, cPos.y, scaleU);

            cPos.z = 100 - 90 * scaleU;                                        //make smaller clouds further away, technically might not actually matter due to orthographic camera

            cloud.transform.position = cPos;
            cloud.transform.SetParent(anchor.transform);
            cloudInstances[i] = cloud;


        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject cloud in cloudInstances)
        {
            float scaleVal = cloud.transform.localScale.x;      //put these values in variables for ease of access
            Vector3 cPos = cloud.transform.position;

            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMultiplier;      //scale to move closer clouds faster, and scales by time.deltaTime

            if (cPos.x <= cloudPositionMin.x)         //keeps cloud from moving infinitely off the screen
            {
                cPos.x = cloudPositionMax.x;
            }

            cloud.transform.position = cPos;
        }
    }
}
