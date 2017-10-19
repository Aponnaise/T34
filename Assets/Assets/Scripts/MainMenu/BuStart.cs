using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuStart : MonoBehaviour {

    public Image black;
    public Animator anim;

    void OnMouseOver() {
        if(Input.GetMouseButtonDown(0)) {
            StartCoroutine(Fading());
        }
    }

    IEnumerator Fading() {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadScene("LevelMenu");
    }
}
