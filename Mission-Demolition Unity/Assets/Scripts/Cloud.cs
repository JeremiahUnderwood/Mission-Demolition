/**
 * 
 * Created By Jeremiah Underwood
 * 
 * Last Edited: N/A
 * Last Edited By: N/A
 * 
 * Description: Cloud Creation
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    [SerializeField] private GameObject cloudSphere;
    [SerializeField] private int numberOfSpheresMin = 6;
    [SerializeField] private int numberOfSpheresMax = 10;
    [SerializeField] private Vector2 sphereScaleRangeX = new Vector2(4, 8);
    [SerializeField] private Vector2 sphereScaleRangeY = new Vector2(2, 4);
    [SerializeField] private Vector2 sphereScaleRangeZ = new Vector2(2, 4);
    [SerializeField] private Vector3 sphereOffsetScale = new Vector3(5, 2, 1);
    [SerializeField] private float scaleYmin = 2f;

    private List<GameObject> spheres;

    void Start()
    {
        spheres = new List<GameObject>();
        int num = Random.Range(numberOfSpheresMin, numberOfSpheresMax);

        for (int i = 0; i < num; i++)
        {
            GameObject sp = Instantiate<GameObject>(cloudSphere);
            spheres.Add(sp);

            Transform spTrans = sp.transform;

            spTrans.SetParent(this.transform);

            Vector3 offset = Random.insideUnitSphere;              //Random Position
            offset.x *= sphereOffsetScale.x;
            offset.y *= sphereOffsetScale.y;
            offset.z *= sphereOffsetScale.z;
            spTrans.localPosition = offset;

            Vector3 scale = Vector3.one;                        //set scale
            scale.x = Random.Range(sphereScaleRangeX.x, sphereScaleRangeX.y);
            scale.y = Random.Range(sphereScaleRangeY.x, sphereScaleRangeY.y);
            scale.z = Random.Range(sphereScaleRangeZ.x, sphereScaleRangeZ.y);

            scale.y *= 1 - (Mathf.Abs(offset.x) / sphereOffsetScale.x); //smaller spheres further from the center x
            scale.y = Mathf.Max(scale.y, scaleYmin);

            spTrans.localScale = scale;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Restart();
        }
    }

    void Restart()
    {
        foreach (GameObject yeet in spheres)
        {
            Destroy(yeet);
        }
        Start();
    }
}
