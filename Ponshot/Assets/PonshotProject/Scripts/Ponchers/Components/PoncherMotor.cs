using UnityEngine;

//this class holds movement functions for a rigidbody character such as player, enemy, npc..
//you can then call these functions from another script, in order to move the character
[RequireComponent(typeof(Rigidbody))]
public class PoncherMotor : MonoBehaviour
{
    //All poncher info
    public PoncherInfo poncherInfo;// aquí cargo el scriptable object de donde voy a obtener todas las variables
    
    public bool sidescroller = true; //freezes Z movement if true

    //Movement Vectors
    [HideInInspector]
    public Vector3 currentSpeed;
    [HideInInspector]
    public float DistanceToTarget;

    //Components
    private Rigidbody rigidBodie;
    private Collider poncherCollider;





    //Component Functions ::::::::::::::::::::::::
    void Awake()
    {
        sidescroller = true; //for 2.5D Games
        rigidBodie = GetComponent<Rigidbody>();
        poncherCollider = this.GetComponent<Collider>();

        //Set the name of the object to the name of the Poncher
        this.gameObject.name = poncherInfo.poncherName;

        //Setting Rigid Bodie Constraints
        if (sidescroller)
            rigidBodie.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        else
            rigidBodie.constraints = RigidbodyConstraints.FreezeRotation;

        //Setting Collision Detection
        rigidBodie.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; 

       
        PhysicMaterial pMat = poncherInfo.poncherPhysicMaterial;
        pMat.name = "PoncherPhysixMat";
        poncherCollider.material = pMat;
        Debug.LogWarning("No physics material found for PoncherMotor, a PoncherPhyxMTL one has been created and assigned", transform);
        

        //assign player tag if not already
        if (tag != "Poncher")
        {

            tag = "Poncher";
            Debug.LogWarning("PoncherMotor script assigned to object without the tag 'Poncher', tag has been assigned automatically", transform);
        }
    }
    


    //Functions  :::::::::::::::::::::::::::::

    //move rigidbody to a target and return the bool "have we arrived?"
    public bool MoveTo(Vector3 destination, float acceleration , float stopDistance, bool ignoreY)
    {
        Vector3 relativePos = (destination - transform.position);
        if (ignoreY)
            relativePos.y = 0;

        DistanceToTarget = relativePos.magnitude;
        if (DistanceToTarget <= stopDistance)
            return true;
        else
            rigidBodie.AddForce(relativePos * acceleration * Time.deltaTime, ForceMode.VelocityChange);

        return false; // Keep moving we havent arrive
    }

    //Rotates Rigid bodie to face its current Velocity
    public void RotateVelocity(float turnSpeed, bool ignoreY) 
    {
        Vector3 direction;
        if (ignoreY)
        {
            direction = new Vector3(rigidBodie.velocity.x, 0f, rigidBodie.velocity.z);
        }
        else
        {
            direction = rigidBodie.velocity;
        }

        if (direction.magnitude >0.1f)
        {
            Quaternion dirQ = Quaternion.LookRotation(direction);
            Quaternion slerp = Quaternion.Slerp(transform.rotation, dirQ, direction.magnitude * turnSpeed * Time.deltaTime);
            rigidBodie.MoveRotation(slerp);
        }
    }

    //Rotates rigidBodie to a specific direction
    public void RotateToDirection(Vector3 lookDir, float turnSpeed, bool ignoreY) 
    {
        Vector3 characterPos = transform.position;
        if (ignoreY)
        {
            characterPos.y = 0;
            lookDir.y = 0;
        }
        //Un número mayor o menor a este hará que mire hacia el otro lado al voltear
        lookDir.z = 0;

        Vector3 newDir = lookDir - characterPos;
        Quaternion dirQ = Quaternion.LookRotation(newDir);
        Quaternion slerp = Quaternion.Slerp(transform.rotation, dirQ, turnSpeed * Time.deltaTime);
        rigidBodie.MoveRotation(slerp);
    }


    public void ManageSpeed(float deceleration, float maxSpeed, bool ignoreY) 
    {
        currentSpeed = rigidBodie.velocity;
        if (ignoreY)
            currentSpeed.y = 0;

        if (currentSpeed.magnitude > 0)
        {
            rigidBodie.AddForce((currentSpeed * -1) * deceleration * Time.deltaTime, ForceMode.VelocityChange);
            if (rigidBodie.velocity.magnitude > maxSpeed)
                rigidBodie.AddForce((currentSpeed * -1) * deceleration * Time.deltaTime, ForceMode.VelocityChange);
        }
    }


}


/* NOTE: ManageSpeed does a similar job to simply increasing the friction property of a rigidbodies "physics material"
 * but this is unpredictable and can result in sluggish controls and things like gripping against walls as you walk/falls past them
 * it's not ideal for gameplay, and so we use 0 friction physics materials and control friction ourselves with the ManageSpeed function instead */

/* NOTE: when you use MoveTo, make sure the stopping distance is something like 0.3 and not 0
 * if it is 0, the object is likely to never truly reach the destination, and it will jitter on the spot as it
 * attempts to move toward the destination vector but overshoots it each frame
 */
