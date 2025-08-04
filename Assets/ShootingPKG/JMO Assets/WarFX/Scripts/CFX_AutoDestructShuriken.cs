using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(AudioSource))] // AudioSource を必須に
public class CFX_AutoDestructShuriken : MonoBehaviour
{
    public bool OnlyDeactivate;

    void OnEnable()
    {
        // 効果音を再生
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.Play();
        }

        StartCoroutine("CheckIfAlive");
    }

    IEnumerator CheckIfAlive()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (!GetComponent<ParticleSystem>().IsAlive(true))
            {
                if (OnlyDeactivate)
                {
#if UNITY_3_5
                    this.gameObject.SetActiveRecursively(false);
#else
                    this.gameObject.SetActive(false);
#endif
                }
                else
                    GameObject.Destroy(this.gameObject);
                break;
            }
        }
    }
}
