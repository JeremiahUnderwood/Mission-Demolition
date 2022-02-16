/**
 * 
 * Created By Jeremiah Underwood
 * 
 * Last Edited: N/A
 * Last Edited By: N/A
 * 
 * Description: Slinshot behavior
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    static private Slingshot S;

    public GameObject prefabProjectile;
    public float velocityMultiplier = 8f;

    [Header("Set Dynamically")]
    [SerializeField] private GameObject launchPoint;
    [SerializeField] private Vector3 LaunchPos;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Rigidbody projectileRB;
    [SerializeField] private bool aimingMode = false;

    static public Vector3 Launch_Pos
    {
        get
        {
            if (S == null) return Vector3.zero;
            return S.LaunchPos;
        }
    }

    private void Awake()                             //initialise some stuff
    {
        S = this;

        Transform LaunchPointTrans = transform.Find("LaunchPoint");
        launchPoint = LaunchPointTrans.gameObject;                      //complicated way of getting reference to launch point
        launchPoint.SetActive(false);

        LaunchPos = LaunchPointTrans.position;
    }

    private void OnMouseEnter()                        //code for when mouse is hovering over the slingshot
    {
        //print("Slingshot: OnMouseEnter");              //just to flex I used print and debug
        launchPoint.SetActive(true);                       //show visual queue for when hovering over slingshot
    }
    private void OnMouseExit()
    {
        //Debug.Log("Slingshot: OnMouseExit");
        launchPoint.SetActive(false);
    }

    void Update()                                          // Update is called once per frame
    {
        if (aimingMode)            //get mouse position
        {

            Vector3 mousePos2D = Input.mousePosition;
            mousePos2D.z = -Camera.main.transform.position.z;
            Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

            Vector3 mouseDelta = mousePos3D - LaunchPos;
            float maxMagnitude = this.GetComponent<SphereCollider>().radius;

            if (mouseDelta.magnitude > maxMagnitude)             //prevent from being stretched to far
            {
                mouseDelta.Normalize();
                mouseDelta *= maxMagnitude;
            }

            Vector3 projectilePos = LaunchPos + mouseDelta;           //move projectile to position
            projectile.transform.position = projectilePos;

            if (Input.GetMouseButtonUp(0))
            {
                aimingMode = false;
                projectileRB.isKinematic = false;
                projectileRB.velocity = -mouseDelta * velocityMultiplier;
                FollowCam.POI = projectile;
                projectile = null;
            }
        }
    }

    private void OnMouseDown()
    {
        aimingMode = true;                                               //prep projectile if mouse pressed down
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = LaunchPos;
        projectileRB = projectile.GetComponent<Rigidbody>();
        projectileRB.isKinematic = true;
    }
}
