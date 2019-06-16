using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusObject : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * 100));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
