using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scaling : MonoBehaviour
{
    public Slider ScaleBar;
    public GameObject BackGround;
    // public Transform BackGroundTf;
    private Vector3 iniScaleV;
    private float BGScale;
    private float iniScale;
    
    // Start is called before the first frame update
    void Start()
    {
        // ScaleBar.gameObject.SetActive(false);
        iniScaleV = BackGround.transform.localScale;
        iniScale = iniScaleV.magnitude;
        Debug.Log("Initial Scale"+ iniScaleV.ToString() + iniScale.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(ScaleBar.value);
        BGScale =  Mathf.Sqrt(((Mathf.Pow(iniScale, 2)) / 3)) / ScaleBar.value;
        BackGround.transform.localScale = new Vector3(BGScale, BGScale, BGScale);
        Debug.Log("Scale"+ BGScale.ToString());
    }
}
