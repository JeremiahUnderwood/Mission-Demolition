/**
 * 
 * Created By Jeremiah Underwood
 * 
 * Last Edited: 2/24/2022
 * Last Edited By: N/A
 * 
 * Description: Game Manager
 * 
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode   //technically just a intiger, but usefully named
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{

    static private MissionDemolition S;

    public Text uitLevel; //UI for level text
    public Text uitShots;  //UI for shots fired
    public Text uitButton;  //UI for button
    public Vector3 CastlePos;  //Position of the castle
    public GameObject[] castles;  //Array of castle objects

    [Header("Set Dynamically")]

    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;   //castle currently in use
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";  //camera mode

    // Start is called before the first frame update
    void Start()
    {
        S = this; //singleton
        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        if (castle != null)
        {
            Destroy(castle);
        }

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos)                                           //Destroy old projectiles
        {
            Destroy(pTemp);
        }

        castle = Instantiate<GameObject>(castles[level]); //make new castle
        castle.transform.position = CastlePos;
        shotsTaken = 0;
        
        SwitchView("Show Both");         //reset the camera
        ProjectileLine.S.Clear();

        Goal.goalMet = false;  //reset goal

        UpdateGUI();
        mode = GameMode.playing;
    }

    void UpdateGUI()
    {
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();

        if ((mode == GameMode.playing) && Goal.goalMet)     //check if level end
        {
            mode = GameMode.levelEnd;
            SwitchView("Show Both");
            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    public void SwitchView(string eView = "")
    {
        if(eView == "")
        {
            eView = uitButton.text;
        }
        showing = eView;
        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                uitButton.text = "Show Castle";
                break;
            case "Show Castle":
                FollowCam.POI = S.castle;
                uitButton.text = "Show Both";
                break;
            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
        }
    }

    public static void ShotFired()
    {
        S.shotsTaken++;
    }

}
