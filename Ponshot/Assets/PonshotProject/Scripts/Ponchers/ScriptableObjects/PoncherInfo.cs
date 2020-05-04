using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoncherInfo", menuName = "New Poncher Info")]
public class PoncherInfo : ScriptableObject
{
    public GameObject prefabPoncher; //This cointains the reference of the Ponchers Prefab 
    public string poncherName;       // Name of the character to get an extra reference
    public string description;       // Contains Relevant Info for UI Porpuses

    public Sprite[] facesHUD;

    public PhysicMaterial poncherPhysicMaterial; //physic material for the capsule poncher rigidBodie
    public PhysicMaterial bonesPhysicMaterial; //Physic material to set bounciness of the ragdoll bones

    public GameObject[] flyableObjects; //Contains the objects that could be spawned at the impact with the ball or falls

    public Animator controller;

    public Animation[] poncherAnimations;
    //Crear forma de hacer estadisticas para cada parkourista fluidez fuego tierra etc pensar el sistema

    //Sets of Varaibles for components :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    //--Movement Variables
    public float accel = 10f;
    public float decel = 10f;

    public float airAccel = 5f;
    public float airDecel = 0f;

    [Range(0f, 5f)]
    public float rotateSpeed = 5f, airRotateSpeed = 1f;
    public float maxSpeed = 9;                              //maximum speed of movement in X/Z axis
    public float slopeLimit = 40, slideAmount = 35;         //maximum angle of slopes you can walk on, how fast to slide down slopes you can't
    public float movingPlatformFriction = 7.7f;             //you'll need to tweak this to get the player to stay on moving platforms properly

    //Jumping
    public Vector3 jumpForce = new Vector3(0, 30, 0);       //Normal jump force
    public Vector3 secondJumpForce = new Vector3(0, 14, 0); //the force of a 2nd consecutive jump
    public Vector3 thirdJumpForce = new Vector3(0, 14, 0);   //the force of a 3rd consecutive jump
    public float jumpDelay = 0.1f;                          //how fast you need to jump after hitting the ground, to do the next type of jump
    public float jumpLeniancy = 0.17f;                      //how early before hitting the ground you can press jump, and still have it work

    //Parkour Actions
    public float rollLeniancy = 0.17f;  //How early can you excecute a parkour roll on landing
    //Vaults
    //Climps

    //Ponshing Actions
    public float minShootForce;    //minForce if the shoot Botton was pressed and release instantly
    public float maxShootForce;    //Force when the player charge the maximum of the bar
    public float chargeTime;       //Amount of time that takes to get the maximum force in charging       
    public float parryForce;       //Force of the parry execute


    [Header ("Animation Smoothing")]
    //Sets of voices poncher sounds :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    public AudioClip[] jumpVoices;
    public AudioClip[] pickVoices;
    public AudioClip[] landingVoices;
    public AudioClip[] chargingVoices;
    public AudioClip[] falseShootVoices;
    public AudioClip[] celebrationVoices;
    public AudioClip[] appearVoices;
    public AudioClip[] shootingVoices;
    public AudioClip[] ponchedVoices;//Sonidos al ser golpeados o ponchados "Quejidos"
    public AudioClip[] humilliationVoices;
    //A revisar
    public AudioClip quejidoHoshikie;
    public AudioClip gemidoAterrizaje;
    public AudioClip cargarLanzamiento;

    //--Action and Body SFX
    public AudioClip[] stepSFX;
    public AudioClip[] hitSFX;
    public AudioClip[] boxHitSFX;


    //Van a cambiar
    public AudioClip DashParedStart;
    public AudioClip DashPared;
    public AudioClip DashParedEnd;


    //Sets of General poncher sounds :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    //--General SFX
    public AudioClip[] pickSFX;
    public AudioClip[] hitActionSFX;
    public AudioClip[] ballHitSFX;
    public AudioClip[] chargingSFX;

    //--Parkour SFX
    public AudioClip[] jumpSFX;
    public AudioClip[] frontFlipSFX;
    public AudioClip[] landingSFX;
    public AudioClip[] brokenBonesSFX;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
