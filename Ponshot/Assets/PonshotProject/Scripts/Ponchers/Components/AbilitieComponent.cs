using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilitieComponent : MonoBehaviour
{

    //Component Behaviour Methods
    public abstract void CheckPreconditions();

    public abstract void Init();

    public abstract void End();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
