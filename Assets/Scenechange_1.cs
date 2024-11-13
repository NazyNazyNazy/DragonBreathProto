using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scenechange_1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(osu);
    }

    // Update is called once per frame
    void osu()
    {
        SceneManager.LoadScene("Shooting");
    }
}
