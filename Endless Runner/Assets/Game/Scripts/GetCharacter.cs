using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCharacter : MonoBehaviour
{
    private GameObject[] characterList;
    private int characterIndex = 0;

    void Awake()
    {
        characterIndex = PlayerPrefs.GetInt("CharacterIndex");
        characterList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject gO in characterList)
        {
            gO.SetActive(false);
        }

        if (characterList[characterIndex])
        {
            characterList[characterIndex].SetActive(true);
        }

    }
}
