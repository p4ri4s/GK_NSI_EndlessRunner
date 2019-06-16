using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsShow : MonoBehaviour
{
    public GameObject[] assets;
    private int assetIndex = 0;

    void Start()
    {
        assets = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            assets[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject asset in assets)
        {
            asset.SetActive(false);
        }

        if (assets[assetIndex])
        {
            assets[assetIndex].SetActive(true);
        }
    }

    public void SwipeLeft()
    {
        if (assetIndex > 0)
        {
            assets[assetIndex].SetActive(false);
            assetIndex--;
            assets[assetIndex].SetActive(true);
        }
    }

    public void SwipeRight()
    {
        if (assetIndex < assets.Length - 1)
        {
            assets[assetIndex].SetActive(false);
            assetIndex++;
            assets[assetIndex].SetActive(true);
        }
    }
}
