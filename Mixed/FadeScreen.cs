using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScreen : MonoBehaviour
{

    public bool fadeIn;

    private Animator anim;
    private int desiredBuildIndex;

    private void Awake() {

        anim = GetComponent<Animator>();
    }

    private void Start() {

        if (!fadeIn) return;
        anim.Play("FadeIn");
    }
    public void TransitionToScene (int buildIndex) {

        this.desiredBuildIndex = buildIndex;
        anim.Play("FadeOut");
    }

    public void GoToDesiredScene () {
        SceneManager.LoadScene(desiredBuildIndex);
    }
}
