using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrivePathDraw : MonoBehaviour
{
    public LineRenderer lineRenderer;
    //public Material lineMaterial;
    // public Location LocationList;
    public PhoneSensor LocationList;
    private List<LLG> TrackingList;
    public Transform DriveCanvas;
    public Transform PathDraw; //temporary added

    private List<float> latY;
    private List<float> lonX;
    private float xScale = 4.6f;
    private float yScale = 6f;
    private int tmp;

    public float milage;
    public float pitch;
    private int markerCount;
    public GameObject markerTemplate;
    private List<GameObject> Markers;

    private int count;
    private int refreshRate = 10;

    //---New for Bombing
    public GameObject settingWindow;
    // public GameObject prefabBomb;
    // public GameObject prefabFlare;
    // public GameObject prefabFruit;

    public GameObject prefabLivelUp;
    public GameObject prefabGDetect;
    public GameObject player;
    public bool testMode;

    // private int bombInterval;
    // private float bombIntervalDist = 16.0f; // distance for bombing pitch

    // private int bombMax;
    //private int bombPitchCount = 0;
    // private float bombPitchDist = 0f;
    // private List<int> bombOn;

    // private int fruitMax;
    // private int fruitPitchCout = 0;
    // private int fruitInterval = 5;

    // private int BPCount; 
    // public Text BombMaxText;
    // public Text BombPitchText;
    // public Text FruitMaxText;
    // public Text FruitPitchText;

    // public FireBtn fireBtn;
    // public LineRenderer fuseRenderer;
    // public int countFuse = 0;
    // public int fuseSkipPitch = 5;
    // public Slider fuseSlider;

    // public float hitBoxSize = 2.0f; //size for hitbox
    // public float fruitForce; // strength to accelarate monster to fruits
    private float CutOff = 0.1f;

// Start is called before the first frame update
void Start()
    {
        testMode = true;
        tmp = -10;
        latY = new List<float>();
        lonX = new List<float>();
        TrackingList = new List<LLG>();

        if (lineRenderer != null)
        {

            /*
            // マテリアル設定
            if (lineMaterial != null)
            {
                lineRenderer.material = lineMaterial;
            }
            // 色設定
            {
                var colorKeys = new[]
                {
                    new GradientColorKey( Color.red, 0.5f ),
                    new GradientColorKey( Color.blue, 0.5f ),
                };
                var alphaKeys = new[]
                {
                    new GradientAlphaKey( 1, 0 ),
                    new GradientAlphaKey( 1, 0 ),
                };
                var gradient = new Gradient();
                gradient.SetKeys(colorKeys, alphaKeys);
                lineRenderer.colorGradient = gradient;
            }
            */
            // 線の太さ。
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.sortingOrder = 0;

        }

        //Markers = new List<GameObject>();
        //GameObject obj = Instantiate(prefabBomb, Vector3.zero, Quaternion.identity);


        // bombMax = 2;
        // hitBoxSize = (float)bombMax;

        // bombInterval = 16;
        // bombIntervalDist = (float)bombInterval;

        // fruitMax = 10;
        // fruitForce = (float)(fruitMax * 0.98f);

        // BombMaxText.text = "所持数：" + bombMax.ToString();
        // BombPitchText.text = "投下間隔：" + bombInterval.ToString();
        // FruitMaxText.text = "所持数：" + fruitMax.ToString();
        // FruitPitchText.text = "投下間隔：" + fruitInterval.ToString();



        count = 0;

        //Set Fuse line property
        // if (fuseRenderer != null)
        // {
        //     // 線の太さ。
        //     fuseRenderer.startWidth = 0.1f;
        //     fuseRenderer.endWidth = 0.1f;
        //     fuseRenderer.sortingOrder = 1;

        // }


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



            //12.13 追加分
            // if (LocationList.drivingFlag ==1)
            // {
            //    count = TrackingList.Count;
            // }
            // else if (LocationList.drivingFlag == 3)
            // {
            //     count = LocationList.SliderNom;
            // }

            count = TrackingList.Count;
            //Debug.Log(count);
            //Debug.Log(TrackingList.Count);
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


                //
                //print("Drawing:" + i.ToString() + " " + TrackingList[i].latitude.ToString() + " " + TrackingList[i].longtitude.ToString());
            }
            //ここまで
            Debug.Log(count);


            //座標変換
            var latMax = Mathf.Max(latY.ToArray());
            var latMin = Mathf.Min(latY.ToArray());
            var lonMax = Mathf.Max(lonX.ToArray());
            var lonMin = Mathf.Min(lonX.ToArray());

            //for (int i = 0; i < TrackingList.Count; i++)
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
            //for (int i = 0; i < TrackingList.Count; i++)


            for (int i = 0; i < count; i++)
            {
                var position = new Vector3(lonX[i], latY[i], 1);
                this.lineRenderer.SetPosition(i, position);
                //print("Latitudu is " + latY[i].ToString() + ", Y = " + TrackingList[i].latitude.ToString());
            }




            //**Reset Bomb and Fruit****************************************************
            // GameObject[] allBomb = GameObject.FindGameObjectsWithTag("Bomb");
            // for (int i = 0; i < allBomb.Length; i++)
            // {
            //     Destroy(allBomb[i]);
            // }

            // GameObject[] allFruit = GameObject.FindGameObjectsWithTag("Fruit");
            // for (int i = 0; i < allFruit.Length; i++)
            // {
            //     Destroy(allFruit[i]);
            // }
            //**Reset Bomb and Fruit End****************************************************

            //New Dropping Bonb based on distance　カボチャのマーカーを置くだけ（引火しない）----------------------------------------
            // bombPitchDist = 0f;
            // bombOn = new List<int>();
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

            //             // if(dist > CutOff)
            //             // {
            //             //     bombPitchDist = bombPitchDist + dist;
            //             // }
            //             print("Distandce " + i.ToString() + "/ " + count.ToString() + " = " + dist.ToString()); 
            //             print(dist);
            //         }

                    
            //     }

            // }
    

            //     //ENd New Dropping Bonb based on distance----------------------------------------

            if(0 < count & count <= latY.Count)
            {
                player.transform.position = new Vector3(lonX[count-1], latY[count-1], 0f);
                // .X = lonX[count-1];
                // player.transform.position.Y = latY[count-1];
            }   

            tmp = 0;
        }
        tmp = tmp + 1;

    }


    public void TestModeOn()
    {
        if(testMode)
        {
            testMode = false;
        }
        else
        {
            testMode = true;
        }
    }

    // public void BombMaxPlus()
    // {
    //     bombMax++;
    //     hitBoxSize = (float)bombMax; 
    //     BombMaxText.text = "有効半径：" + bombMax.ToString();
    // }

    // public void BombMaxMinus()
    // {
    //     if(bombMax > 1)
    //     {
    //         bombMax--;
    //         hitBoxSize = (float)bombMax;
    //         BombMaxText.text = "有効半径：" + bombMax.ToString();
    //     }
    // }

    // public void BomPitchPlus()
    // {
    //     bombInterval++;
    //     bombIntervalDist = (float)bombInterval;
    //     BombPitchText.text = "投下間隔：" + bombInterval.ToString();
    // }

    // public void BombPitchMinus()
    // {
    //     if(bombInterval > 1)
    //     {
    //         bombInterval--;
    //         bombIntervalDist = (float)bombInterval;
    //         BombPitchText.text = "投下間隔：" + bombInterval.ToString();
    //     }
    // }

    // public void FruitMaxPlus()
    // {
        
    //     fruitMax++;
    //     //fruitForce = (float)fruitMax;
    //     fruitForce = fruitMax * 0.98f;
    //     FruitMaxText.text = "引付力：" + fruitMax.ToString();
        

    // }

    // public void FruitMaxMinus()
    // {
    //     if(fruitMax > 0)
    //     {
            
    //         fruitMax--;
    //         //fruitForce = (float)fruitMax;
    //         fruitForce = fruitMax * 0.98f;
    //         FruitMaxText.text = "引付力：" + fruitMax.ToString();
            
    //     }
    // }

    // public void FruitPitchPlus()
    // {
    //     fruitInterval++;
    //     FruitPitchText.text = "投下間隔：" + fruitInterval.ToString();
    // }

    // public void FruitPitchMinus()
    // {
    //     if(fruitInterval > 0)
    //     {
    //         fruitInterval--;
    //         FruitPitchText.text = "投下間隔：" + fruitInterval.ToString();
    //     }
    // }

    // public void SettingOn()
    // {
    //     settingWindow.SetActive(true);
    //     BombMaxText.text = "有効半径：" + bombMax.ToString();
    //     BombPitchText.text = "投下間隔：" + bombInterval.ToString();
    //     FruitMaxText.text = "引付力：" + fruitMax.ToString(); //fruitMax.ToString();
    //     FruitPitchText.text = "特になし：" + fruitInterval.ToString();
    // }

    // public void SettingOff()
    // {
    //     settingWindow.SetActive(false);
    // }

    // public void ResetBomb()
    // {
    //     fuseRenderer.positionCount = 0;
    //     fuseRenderer.positionCount = 0;
    //     GameObject[] allBomb = GameObject.FindGameObjectsWithTag("Bomb");
    //     for (int i = 0; i < allBomb.Length; i++)
    //     {
    //         Destroy(allBomb[i]);
    //     }

    //     GameObject[] allMonster = GameObject.FindGameObjectsWithTag("Enemy");
    //     for (int i = 0; i < allMonster.Length; i++)
    //     {
    //         Destroy(allMonster[i]);
    //     }

    //     // fireBtn.Standby = false;
    //     // fireBtn.waitDone = false;
    // }

    // public void ResetMarker()
    // {
    //     GameObject[] allMarker = GameObject.FindGameObjectsWithTag("Marker");
    //     for (int i = 0; i < allMarker.Length; i++)
    //     {
    //         Destroy(allMarker[i]);
    //     }
    // }

    /*
    public void FireFuse()
    {
        count = TrackingList.Count; 
        fuseRenderer.positionCount = 0;
        if(fireBtn.Standby)
        {
            Debug.Log("Stanby");
            //Write fuse
            if(fireBtn.FireOn)
            {
                Debug.Log("FireOn");

                for(int countFuse = 0; countFuse <= count; countFuse++)
                {
                    fuseRenderer.positionCount = countFuse;
                    for (int i = 0; i <= countFuse; i++)
                    {
                        var position = new Vector3(lonX[i], latY[i], 1);
                        fuseRenderer.SetPosition(i, position);

                        print("bombOn count = " + bombOn.Count.ToString());
                        
                        for (int v = 0; v < bombOn.Count; v++)
                        {
                            print(bombOn[v].ToString());
                            print("BonbOn counting up =" + v.ToString());
                            if(i == bombOn[v])
                            {
                                GameObject obj = Instantiate(prefabFlare, position, Quaternion.identity);
                            }
                        }                    

                    }   
                }
                fireBtn.waitDone = true; //Stage cleare checking
            }
            else
            {
                fuseRenderer.positionCount = 0;
            }           
        }
    }
    */

}