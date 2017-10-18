using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingZoneManager : MonoBehaviour {

    public static int? loadingZoneId;


    private void Awake()
    {
        if (loadingZoneId == null)
        {
            loadingZoneId = 2;
        }
    }

    public static void LoadScene(int zone, int scene)
    {
        loadingZoneId = zone;
        SceneManager.LoadScene(scene);
    }


}
