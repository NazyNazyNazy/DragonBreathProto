using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System.Diagnostics;
using TMPro;
using UnityEngine.UI;   

public class PhoneSensor : MonoBehaviour
{

    public float Accel;
    //public GameObject TextBox;
    public TextMeshProUGUI AccelText;
    public TextMeshProUGUI LevelText;
    public AudioClip ClickSound;
    private AudioSource audioSource;


    public float longtitude;
    public float latitude;
    // public Text locationText;
    public List<LLG> TrackingList;
    public string LogTime;

    private LLG llg;
    // private int i;
    // private int tmp;

    private float lastLon;
    private float lastLat;
    private float millage;
    public static float milageTotal;
    private int encount;
    private float encountPitch;
    private float pitchCap;
    private float pitchMin;

    private int frameRate;
    private int count;

    private float adjustX;
    private float adjustY;
    private Vector3 revZ;

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();

        if (Input.location.isEnabledByUser) {
            StartCoroutine(GetLocation());
        }

        TrackingList =   new List<LLG>();
        // i = 0;
        // tmp = 0;
        // pitch = 0.015f;
        millage = 0f;
        milageTotal = 0f;
        lastLon = Input.location.lastData.longitude;
        lastLat = Input.location.lastData.latitude;
        frameRate = 60;
        count = 0;
        encountPitch = 0.015f;
        pitchCap = 0.01f;
        pitchMin = 0.0001f;
        SetZDirection();

        adjustX = 0f;
        adjustY = 0f;
    }

    private IEnumerator GetLocation()
    {
        Input.location.Start();
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(0.5f);
        }
        longtitude = Input.location.lastData.longitude;
        latitude = Input.location.lastData.latitude;
        yield break;

    }




    // Update is called once per frame
    void Update() {
        //accelaration sensor

        // var dir = Vector3.zero;
        // dir.x = -1 * Input.acceleration.x;
        // dir.z = -1 * Input.acceleration.y;
        // Accel = Mathf.Sqrt(Mathf.Pow(dir.x, 2) + Mathf.Pow(dir.y, 2));

        // var dir = Vector3.zero;
        // dir = Input.acceleration;
        // //Accel = Input.acceleration.magnitude;
        // Vector3 zAxisA = dir.normalized;
        // Vector3 zAxisB = revZ.normalized;
        // Quaternion rotation = Quaternion.FromToRotation(zAxisA, zAxisB);
        // Vector3 RotatedDir = rotation * dir;
        // Accel = Mathf.Sqrt(Mathf.Pow(RotatedDir.x, 2) + Mathf.Pow(RotatedDir.z, 2));


        var dir = Vector3.zero;
        dir = Input.acceleration;

        var Zdir = new Vector3(0f, 0f, -1f);
        Vector3 zAxisA = Zdir.normalized;
        Vector3 zAxisB = revZ.normalized;
        Quaternion rotation = Quaternion.FromToRotation(zAxisB, zAxisA);
        Vector3 RotatedDir = rotation * dir;
        Accel = Mathf.Sqrt(Mathf.Pow(RotatedDir.x, 2) + Mathf.Pow(RotatedDir.y, 2));

        // var dir = Vector3.zero;
        // dir.x = -1 * Input.acceleration.x - adjustX;
        // dir.y = -1 * Input.acceleration.y - adjustY;
        // Accel = Mathf.Sqrt(Mathf.Pow(dir.x, 2) + Mathf.Pow(dir.y, 2));

        float lat = Input.location.lastData.latitude;
        float lon = Input.location.lastData.longitude;

        AccelText.text = "Accelaration:" 
                        + Accel.ToString() 
                        // + "\nX:" + dir.x.ToString() 
                        // + "\nY:" + dir.y.ToString()
                        + "\nX:" + RotatedDir.x.ToString() 
                        + "\nY:" + RotatedDir.y.ToString() 
                        + "\n\nLon:" + lon.ToString() 
                        + "\nLat:" + lat.ToString()
                        + "\nMilage:" + milageTotal
                        + "\n" + encount.ToString() + "回";
        //TextBox.TextMeshPro = "Accelaration:" + Accel.ToString();

        if (Accel >= 0.3f) {
            // audioSource.PlayOneShot(ClickSound);
            UnityEngine.Debug.Log ("Large G detected");
            LevelText.text = "大きいGが検出されました";
            millage = 0f;
        }

        if(count == frameRate){
            Debug.Log("count2");
            count = 0;

            llg = new LLG();
            // llg.latitude = Input.location.lastData.latitude;
            // llg.longtitude = Input.location.lastData.longitude;
            llg.latitude = lat;
            llg.longtitude = lon;
            llg.time = System.DateTime.Now.ToString("G");
            llg.accelX = Input.acceleration.x; 
            llg.accelY = Input.acceleration.y; 
            llg.accelZ = Input.acceleration.z; 
            // llg.accelABC = tmpAccelABC;
            // (Mathf.Sqrt(Mathf.Pow(llg.accelX,2) + Mathf.Pow(llg.accelY,2) + Mathf.Pow(llg.accelZ,2)));
            // tmpAccelABC = 0;
            TrackingList.Add(llg);
            // i = TrackingList.Count - 1;
            LevelText.text =
                "現在の走行経験値" + millage.ToString() + " / " +encountPitch.ToString()
                + "\nLevelアップイベント獲得！"
                + "\n" + DriveDragon.Level.ToString() + "レベル"
                ;
            if (millage >= encountPitch) {
                // eventList.Add(llg);
                // encountID.Add(i);
                millage = millage - encountPitch;
                DriveDragon.Level += 1;
                // encount += 1;
                // ResultText.text =
                //     "現在の走行距離は" + millage.ToString()
                //     + "\nHPアップイベント獲得！"
                //     + "\n" + encount.ToString() + "回"
                //     ;
            }
            // float milageDelta = Mathf.Sqrt(Mathf.Pow(Input.location.lastData.latitude - lastLat, 2f) + Mathf.Pow((Input.location.lastData.longitude - lastLong), 2f));
            float milageDelta = Mathf.Sqrt(Mathf.Pow(lat - lastLat, 2f) + Mathf.Pow(lon - lastLon, 2f));
            if (milageDelta > pitchCap || milageDelta < pitchMin) {
                milageDelta = 0f;
            }
            millage = millage + milageDelta;
            milageTotal = milageTotal + milageDelta;
            // lastLon = Input.location.lastData.longitude;
            // lastLat = Input.location.lastData.latitude;
            lastLat = lat;
            lastLon = lon;
        }
        count +=1;
    }

    public void SetZDirection() {
        revZ = Input.acceleration;
    }

    public void SetInitialG() {
        adjustX = -1 * Input.acceleration.x;
        adjustY = -1 * Input.acceleration.y;
    }

}
