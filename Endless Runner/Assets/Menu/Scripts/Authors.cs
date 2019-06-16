using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Authors : MonoBehaviour
{
    public GameObject authorsWindow;

    public void openAuthorsWindow()
    {
        authorsWindow.SetActive(true);
    }

    public void closeWindow()
    {
        authorsWindow.SetActive(false);
    }
}
