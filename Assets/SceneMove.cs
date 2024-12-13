using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public void BtnOnClick()
    {
        SceneManager.LoadScene("DriveResult");
    }
}