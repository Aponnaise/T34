using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingZoneManager : MonoBehaviour {

    public static int? loadingZoneId;
    public Image black;
    public Animator anim;
    GameObject obj;

    private void Awake()
    {
        if (loadingZoneId == null)
        {
            loadingZoneId = 2;
        }
        obj = gameObject;
    }

    public static void LoadScene(int zone, int scene)
    {
        loadingZoneId = zone;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehav>().leaveWalk = true;
        GameObject.FindGameObjectWithTag("LoadingZoneManager").GetComponent<LoadingZoneManager>().StartFading(scene);
    }

    public void StartFading(int scene)
    {
        StartCoroutine(Fading(scene));
    }

    IEnumerator Fading(int scene)
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadScene(scene);
    }


}
