using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    private GameObject[] characterList;
    private int[] pointsToUnlockCharacter = new int[] { 0, 500, 1500, 2500, 5000};
    private int characterIndex = 0;
    private int highScore = 0;
    public GameObject characterLocked;
    public Text textCharacterLocked;


    private void Start()
    {
        characterIndex = PlayerPrefs.GetInt("CharacterIndex");
        highScore = PlayerPrefs.GetInt("HighScore");
        characterList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject gO in characterList)
        {
            gO.SetActive(false);
        }

        if(characterList[characterIndex])
        {
            characterList[characterIndex].SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
        {
            SwipeLeft();
        }
        else if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
        {
            SwipeRight();
        }
        else if(Input.GetKeyDown(KeyCode.Return))
        {
            Confirm();
        }

    }

    public void SwipeLeft()
    {
        if(characterIndex > 0)
        {
            characterList[characterIndex].SetActive(false);
            characterIndex--;
            if(highScore >= pointsToUnlockCharacter[characterIndex])
            {
                characterLocked.SetActive(false);
            }
            else
            {
                characterLocked.SetActive(true);
                textCharacterLocked.text = "You need to score " + pointsToUnlockCharacter[characterIndex] + " points";
            }
            characterList[characterIndex].SetActive(true);
        }
    }

    public void SwipeRight()
    {
        if (characterIndex < characterList.Length - 1)
        {
            characterList[characterIndex].SetActive(false);
            characterIndex++;
            if (highScore >= pointsToUnlockCharacter[characterIndex])
            {
                characterLocked.SetActive(false);
            }
            else
            {
                characterLocked.SetActive(true);
                textCharacterLocked.text = "You need to score " + pointsToUnlockCharacter[characterIndex] + " points";
            }
            characterList[characterIndex].SetActive(true);
        }
    }

    public void Confirm()
    {
        if(highScore >= pointsToUnlockCharacter[characterIndex])
        {
            PlayerPrefs.SetInt("CharacterIndex", characterIndex);
            SceneManager.LoadScene("Game");
        }
    }
}
