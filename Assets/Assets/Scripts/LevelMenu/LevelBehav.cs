using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelBehav : MonoBehaviour {

    public Image black;
    public Animator anim;
    string levelName;

    void Start () {
        levelName = transform.name;
	}

    void OnMouseOver() {
        if(Input.GetMouseButtonDown(0)) {
            switch(levelName) {
                case "1":
                    //Variablen für das Level setzen
                    break;
                case "2":
                    //Variablen für das Level setzen
                    break;

            }
           
            StartCoroutine(Fading());
        }
    }

    IEnumerator Fading() {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadScene("Frankfurt");
    }
}
