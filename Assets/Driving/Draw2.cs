using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Draw2 : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public PhoneSensor LocationList;
    private List<LLG> TrackingList;

    private List<float> latY;
    private List<float> lonX;
    private float xScale = 4.6f;
    private float yScale = 6f;
    private int tmp;

    public float milage;
    public float pitch;
    // private List<GameObject> Markers;

    private int count;
    private int refreshRate = 10;

    public GameObject prefabLivelUp;
    public GameObject prefabGDetect;
    public GameObject player;
    public bool testMode;

    public GameObject BackGround;
    public float BGIniScale;
    // Start is called before the first frame update
    void Start()
    {
        // testMode = true;
        tmp = -10;
        latY = new List<float>();
        lonX = new List<float>();
        TrackingList = new List<LLG>();

        if (lineRenderer != null)
        {
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.sortingOrder = 0;
        }
        // Markers = new List<GameObject>();
        count = 0;
        BGIniScale = (BackGround.transform.localScale.x +  BackGround.transform.localScale.y)/2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (tmp == refreshRate)
        {
            //座標読み込み
            TrackingList = LocationList.TrackingList;
            latY.Clear();
            lonX.Clear();
            count = TrackingList.Count;

            print("count = " + count.ToString() + ", TrackiingList.Count = " + TrackingList.Count.ToString());

            for (int i = 0; i < count; i++)
            {

                if(testMode)
                {
                    print("test mode");
                    latY.Add(i * 1f);
                    lonX.Add(i * i * 1f);
                    TrackingList[i].latitude = (i * 1f)/100.0f;
                    TrackingList[i].longtitude = (i * i * 1f)/100.0f;

                }
                else
                {
                    print("not test mode");
                    latY.Add(TrackingList[i].latitude);
                    lonX.Add(TrackingList[i].longtitude);
                }
            }
            Debug.Log(count);


            //座標変換
            var latMax = Mathf.Max(latY.ToArray());
            var latMin = Mathf.Min(latY.ToArray());
            var lonMax = Mathf.Max(lonX.ToArray());
            var lonMin = Mathf.Min(lonX.ToArray());
            for (int i = 0; i < count; i++)
            {
                if((latMax - latMin) == 0f)
                {
                    latY[i] = (latY[i] - ((latMax + latMin) / 2)) * yScale;
                    lonX[i] = (lonX[i] - ((lonMax + lonMin) / 2)) * xScale;
                }
                else
                {
                    latY[i] = (latY[i] - ((latMax + latMin) / 2)) * yScale / (latMax - latMin);
                    lonX[i] = (lonX[i] - ((lonMax + lonMin) / 2)) * xScale / (lonMax - lonMin);
                }

            }



            //////////////////////////////////////
            // 頂点を追加。
            lineRenderer.positionCount = 0;
            lineRenderer.positionCount = TrackingList.Count;
            // 追加した頂点の座標を設定
            for (int i = 0; i < count; i++)
            {
                var position = new Vector3(lonX[i], latY[i], 1);
                this.lineRenderer.SetPosition(i, position);
                //print("Latitudu is " + latY[i].ToString() + ", Y = " + TrackingList[i].latitude.ToString());
            }

            //背景画像を拡大縮小
            float BGScale;
            if((latMax - latMin) == 0f){
                BGScale = 1f;
            } else {
                BGScale =  0.00001f /(latMax - latMin);
            }
            Debug.Log("BGScale:" + BGScale);
            BackGround.transform.localScale = new Vector3(BGIniScale * BGScale, BGIniScale * BGScale, 0);

            //**Reset Marker****************************************************
            GameObject[] allMarkers = GameObject.FindGameObjectsWithTag("Marker");
            for (int i = 0; i < allMarkers.Length; i++)
            {
                Destroy(allMarkers[i]);
            }

            //Ser Marker
            for (int i = 0; i < LocationList.LevelUpList.Count; i++)
            {
                int j = LocationList.LevelUpList[i];
                var position = new Vector3(lonX[j], latY[j], 1);
                GameObject obj = Instantiate(prefabLivelUp, position, Quaternion.identity);
            }

            for (int i = 0; i < LocationList.GDetectList.Count; i++)
            {
                int j = LocationList.GDetectList[i];
                var position = new Vector3(lonX[j], latY[j], 1);
                GameObject obj = Instantiate(prefabGDetect, position, Quaternion.identity);
            }

            // for (int i = 0; i < count; i++)
            // {
            //     var position = new Vector3(lonX[i], latY[i], 1);
            //     if(bombPitchDist >= bombIntervalDist)
            //     {
            //         GameObject obj = Instantiate(prefabBomb, position, Quaternion.identity);
            //         bombPitchDist = 0f;
            //         bombOn.Add(i);
            //         print("bombOn count = " + bombOn.Count.ToString());
            //         for (int v = 0; v < bombOn.Count; v++)
            //         {
            //             print(bombOn[v].ToString());
            //         }
            //     }
            //     else
            //     {
            //         if(i == 0)
            //         {

            //         }
            //         else{
            //             var lat1 = TrackingList[i-1].latitude * Mathf.PI / 180f;
            //             var lon1 = TrackingList[i-1].longtitude * Mathf.PI / 180f;
            //             var lat2 = TrackingList[i].latitude * Mathf.PI / 180f;
            //             var lon2 = TrackingList[i].longtitude * Mathf.PI / 180f;
            //             var lon1to2 = (TrackingList[i].longtitude - TrackingList[i-1].longtitude ) * Mathf.PI / 180f; 

            //             var dist = 6371 * Mathf.Acos( Mathf.Cos(lat1) * Mathf.Cos(lon1to2) * Mathf.Cos(lat2) + Mathf.Sin(lat1) * Mathf.Sin(lat2));

            //             print("Distandce " + i.ToString() + "/ " + count.ToString() + " = " + dist.ToString()); 
            //             print(dist);
            //         }   
            //     }
            // }

            if(0 < count & count <= latY.Count)
            {
                player.transform.position = new Vector3(lonX[count-1], latY[count-1], 0f);
            }   

            tmp = 0;
        }
        tmp = tmp + 1;
    }
}
