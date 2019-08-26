using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeManManager : MonoBehaviour {

    public Animator animator;
    public static bool waitActive = false;
    public static int currentLife = 19;
    private int LIFE = 19;
    // Use this for initialization
    void Start()
    {
        currentLife = LIFE;
    }

    IEnumerator Wait()
    {
        waitActive = true;
        yield return new WaitForSeconds(1.5f);
        //currentLife--;
        waitActive = false;
        //animator.SetBool("isCloseEyes", false);
    }
    // Update is called once per frame
    void Update()
    {
        //if (!waitActive && animator.GetBool("isCloseEyes")) { StartCoroutine(Wait()); }
        checkNumberOfEsqueletos();
    }

    void checkNumberOfEsqueletos()
    {
        if (gameObject.activeSelf)
        {
            if (LevelManagerInGameLevel2.currentNumberOfEsqueletos == 5 || (currentLife % 5 == 0 && currentLife != 0))
            {
                StartCoroutine(Wait());
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Animator heroAnimator = collision.gameObject.GetComponent<Animator>();

        if (collision.gameObject.tag == "Arrow" && !waitActive) { currentLife--; Debug.Log("BeeMan LIFE: " + currentLife); }
        if (collision.gameObject.tag == "Hero" && !waitActive
            && (heroAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Front")
            || heroAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Left")
            || heroAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Right")
            || heroAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Down"))) { currentLife--; Debug.Log("BeeMan LIFE: " + currentLife); }

    }
}
