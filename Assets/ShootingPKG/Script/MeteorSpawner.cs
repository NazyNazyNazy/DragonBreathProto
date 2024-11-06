using System.Collections;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    enum MeteorSize { Small, Medium, Large, }

    public bool isActive = false;

    [SerializeField]
    GameObject meteorPrefab = null;
    [SerializeField, Min(0)]
    float minMeteorSpeed = 1;
    [SerializeField, Min(0)]
    float maxMeteorSpeed = 4;
    [SerializeField, Range(-180, 180)]
    float minAngleZ = 0;
    [SerializeField, Range(-180, 180)]
    float maxAngleZ = 0;
    [SerializeField, Min(0.1f)]
    float minSpawnInterval = 3f; // 出現間隔の最小値
    [SerializeField, Min(0.1f)]
    float maxSpawnInterval = 6f; // 出現間隔の最大値

    bool spawning = false;

    void Update()
    {
        if (isActive && !spawning)
        {
            StartCoroutine(nameof(SpawnTimer));
        }
    }

    IEnumerator SpawnTimer()
    {
        spawning = true;

        yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));

        SpawnMeteor();

        spawning = false;
    }

    void SpawnMeteor()
    {
        GameObject meteorObj;
        MeteorController meteor;
        Vector3 scale = Vector3.one;
        Quaternion rotation = Quaternion.Euler(Vector3.forward * Random.Range(minAngleZ, maxAngleZ)); // 隕石の発射角度

        int hpMultiplier = 1;
        int scoreMultiplier = 1;

        int sizeCount = System.Enum.GetNames(typeof(MeteorSize)).Length;

        int choosedSize = Random.Range(0, sizeCount);

        switch ((MeteorSize)System.Enum.ToObject(typeof(MeteorSize), choosedSize))
        {
            case MeteorSize.Small:
                hpMultiplier = 2;
                scoreMultiplier = 2;
                scale *= 0.1f;
                break;

            case MeteorSize.Medium:
                hpMultiplier = 2;
                scoreMultiplier = 2;
                scale *= 0.1f;
                break;

            case MeteorSize.Large:
                hpMultiplier = 3;
                scoreMultiplier = 3;
                scale *= 0.1f;
                break;
        }

        meteorObj = Instantiate(meteorPrefab, transform.position, rotation * transform.rotation);
        meteorObj.transform.localScale = scale;
        meteor = meteorObj.GetComponent<MeteorController>();
        meteor.hp *= hpMultiplier;
        meteor.score *= scoreMultiplier;
        meteor.speed = Random.Range(minMeteorSpeed, maxMeteorSpeed);
    }
}
