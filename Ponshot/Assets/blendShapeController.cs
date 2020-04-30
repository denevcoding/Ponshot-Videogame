using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blendShapeController : MonoBehaviour
{

    public Animator blendAnimator;
    bool activated = true;

    // Start is called before the first frame update
    void Start()
    {
        blendAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            blendAnimator.applyRootMotion = false;           
            StartCoroutine(BlinkTimmer());
        }

        if (!activated)
        {            
            blendAnimator.applyRootMotion = true;
            StartCoroutine(BlinkTimmer());
        }
        
       // blendAnimator.applyRootMotion = true;
    }

    IEnumerator BlinkTimmer()
    {
        
        yield return new WaitForSeconds(4);
        activated = !activated;

    }
}
