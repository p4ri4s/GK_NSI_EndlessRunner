using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostObject : MonoBehaviour
{
    private Transform playerTransform;
    private bool isPickedUp = false;
    public float timeBoost;

    private void Update()
    {
        if (isPickedUp)
        {
            transform.position = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject.GetComponent<Collider>());
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            StartCoroutine(boostEffect());
            isPickedUp = true;

        }
    }

    IEnumerator boostEffect()
    {
        yield return new WaitForSeconds(timeBoost);
        isPickedUp = false;
        Destroy(gameObject);
    }
}
