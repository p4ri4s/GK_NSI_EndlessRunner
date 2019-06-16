using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEffects : MonoBehaviour
{
    private Transform playerTransform;
    private bool isPickedUp = false;
    public float timeTrap;

    private void Update()
    {
        if(isPickedUp)
        {
            transform.position = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(trapEffect());
            Destroy(gameObject.GetComponent<Collider>());
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            isPickedUp = true;
            
        }
    }

    IEnumerator trapEffect()
    {
        yield return new WaitForSeconds(timeTrap);
        isPickedUp = false;
        Destroy(gameObject);
    }
}
