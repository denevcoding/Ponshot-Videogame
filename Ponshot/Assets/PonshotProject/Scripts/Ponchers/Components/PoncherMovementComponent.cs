using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using PathologicalGames;

//Handles Poncher movement, utilising the CharacterMotor class
[RequireComponent(typeof(PoncherMotor))]
public class PoncherMovementComponent : MonoBehaviour
{
    //Components
    private PoncherMotor poncherMotor;
    private Animator animatorPoncher;
    private Rigidbody rigidBodiePoncher;
    private CapsuleCollider colliderPoncher;

    public Transform floorChecks;

    //Movement
    [HideInInspector] public float accel;   
    [HideInInspector] public float airAccel;
    [HideInInspector] public float decel;
    [HideInInspector] public float airDecel;

    [Range(0f, 5f)]
    [HideInInspector] public float rotateSpeed, airRotateSpeed; //how fast to rotate on the ground, how fast to rotate in the air
    [HideInInspector] public float maxSpeed;                    //maximum speed of movement in X/Z axis
    [HideInInspector] public float slopeLimit, slideAmount;     //maximum angle of slopes you can walk on, how fast to slide down slopes you can't
    [HideInInspector] public float movingPlatformFriction;      //you'll need to tweak this to get the player to stay on moving platforms properly


    //Jumping is going to be on other component
    [HideInInspector] public Vector3 jumpForce;       //normal jump force
    [HideInInspector] public Vector3 secondJumpForce; //the force of a 2nd consecutive jump
    [HideInInspector] public Vector3 thirdJumpForce;  //the force of a 3rd consecutive jump
    [HideInInspector] public float jumpDelay;         //how fast you need to jump after hitting the ground, to do the next type of jump
    [HideInInspector] public float jumpLeniancy;       //how early before hitting the ground you can press jump, and still have it work


    [HideInInspector]
    public int onEnemyBounce;
    private bool landSoundUnaVez = true;
    private int onJump;
    private bool grounded;
    private Transform[] floorCheckers;
    private Quaternion screenMovementSpace;
    private float airPressTime, groundedCount, curAccel, curDecel, curRotateSpeed, slope;
    private Vector3 direction, moveDirection, screenMovementForward, screenMovementRight, movingObjSpeed;

    //Gameplay Variables
    float h;
    float v;
    private int Joystick;
    [HideInInspector] public int propietario; // The number of the player on the game
    [HideInInspector] public InputDevice inputDevice; //The Device Attatched to thi player





    void Awake()
    {
        //Create a single floorcheck in centre of object, if none are assigned
        if (!floorChecks)
        {
            floorChecks = new GameObject().transform;
            floorChecks.name = "FloorChecks";
            floorChecks.parent = transform;
            floorChecks.position = transform.position;
            GameObject check = new GameObject();
            check.name = "Check1";
            check.transform.parent = floorChecks;
            check.transform.position = transform.position;
            Debug.LogWarning("No 'floorChecks' assigned to PlayerMove script, so a single floorcheck has been created", floorChecks);
        }


        //gets child objects of floorcheckers, and puts them in an array
        //later these are used to raycast downward and see if we are on the ground
        floorCheckers = new Transform[floorChecks.childCount];
        for (int i = 0; i < floorCheckers.Length; i++)
        {
            floorCheckers[i] = floorChecks.GetChild(i);
        }


        //:::::Getting the other components for being used from this
        poncherMotor = GetComponent<PoncherMotor>();
        rigidBodiePoncher = GetComponent<Rigidbody>();
        colliderPoncher = GetComponent<CapsuleCollider>();
        animatorPoncher = GetComponent<Animator>();

        //:::::Setting variables for movement from the Scriptable Object
        //Movement Velocities
        accel = poncherMotor.poncherInfo.accel;
        airAccel = poncherMotor.poncherInfo.airAccel;
        decel = poncherMotor.poncherInfo.decel;
        airDecel = poncherMotor.poncherInfo.airDecel;

        //Rotate Speed
        rotateSpeed = poncherMotor.poncherInfo.rotateSpeed;
        airRotateSpeed = poncherMotor.poncherInfo.rotateSpeed;
        maxSpeed = poncherMotor.poncherInfo.maxSpeed;

        //Slopes Limits
        slopeLimit = poncherMotor.poncherInfo.slopeLimit;
        slideAmount = poncherMotor.poncherInfo.slideAmount;        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputDevice = (InputManager.Devices.Count > 0) ? InputManager.Devices[0] : null;

        if (inputDevice != null)
        {

            //stops rigidbody "sleeping" if we don't move, which would stop collision detection
            rigidBodiePoncher.WakeUp();

            //adjust movement values if we're in the air or on the ground
            curAccel = (grounded) ? accel : airAccel;
            curDecel = (grounded) ? decel : airDecel;
            curRotateSpeed = (grounded) ? rotateSpeed : airRotateSpeed;

            //get movement axis relative to camera
            screenMovementForward = screenMovementSpace * Vector3.forward;
            screenMovementRight = screenMovementSpace * Vector3.right;


            h = inputDevice.LeftStickX * 2; // Horizontal Axis
            v = inputDevice.LeftStickY * 2; // Vertical Axis
        }
    }




    void CheckPreconditions() 
    {
    }

    void Cancel()
    {
    }

    void End()
    {
    }
}
