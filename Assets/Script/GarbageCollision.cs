using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollision : MonoBehaviour
{
    public CommWithArduino commWithArduino;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Garbage")
        {
            Debug.Log("ts");
            commWithArduino.collisionGarbage = true;
            StartCoroutine("Timer");
        }


    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.8f);
        commWithArduino.collisionGarbage = false;
    }

}
