using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public GameObject[] pathPrefabs;
    private List<GameObject> activePaths;
    private Transform playerTransform;
    private float spawnPathZ = 0.0f;
    private float pathLength = 15.0f;
    private int amountPathsOnScreen = 5;
    private int lastPathIndex = 0;
    private float highScoreRoadLength = 0;
    public GameObject highScorePointer;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        activePaths = new List<GameObject>();
        highScoreRoadLength = PlayerPrefs.GetFloat("HighScoreRoadLength");

        spawnPath(0);
        spawnPath(1);
        for (int i = 0; i < amountPathsOnScreen; i++)
        {
            spawnPath(getRandomIndexPath());
        }

        if (highScoreRoadLength > 0)
        {
            highScorePointer.SetActive(true);
            highScorePointer.transform.position = new Vector3(0, 0, highScoreRoadLength);
        }
    }

    void Update()
    {
        if (playerTransform.position.z > (spawnPathZ - amountPathsOnScreen * pathLength))
        {
            spawnPath(getRandomIndexPath());
            removePath();
        }
    }

    private void spawnPath(int pathIndex)
    {
        GameObject path = Instantiate(pathPrefabs[pathIndex]) as GameObject;
        path.transform.SetParent(transform);
        path.transform.position = Vector3.forward * spawnPathZ;
        spawnPathZ += pathLength;
        activePaths.Add(path);
    }

    private void removePath()
    {
        Destroy(activePaths[0]);
        activePaths.RemoveAt(0);
    }

    private int getRandomIndexPath()
    {
        if (pathPrefabs.Length <= 1)
        {
            return 0;
        }

        int randomIndex = lastPathIndex;
        while (randomIndex == lastPathIndex)
        {
            randomIndex = Random.Range(2, pathPrefabs.Length);
        }

        lastPathIndex = randomIndex;
        return randomIndex;
    }
}
