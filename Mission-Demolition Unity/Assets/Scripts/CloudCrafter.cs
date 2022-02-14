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
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
