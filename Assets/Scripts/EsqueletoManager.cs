using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsqueletoManager : MonoBehaviour {

    public Animator animator;
    private int option = 0;
    private int LIFE = 3;
    public static int currentLife;
    private bool waitActive = false;
    private AnimatorStateInfo animatorState;
    private float MOVE_SPEED = 0.5f;
	// Use this for initialization
	void Start () {
        currentLife = LIFE;
	}

    IEnumerator Wait()
    {
        waitActive = true;
        yield return new WaitForSeconds(3.0f);
        option++;
        waitActive = false;
    }

    void typeOfMovement()
    {
        switch (option)
        {
            case 0:
                animator.SetBool("up", true);
                break;
            case 1:
                animator.SetBool("down", true);
                break;
            case 2:
                animator.SetBool("up", false);
                break;
            case 3:
                animator.SetBool("idle", true); animator.SetBool("down", false);
                break;
            case 4:
                animator.SetBool("down", true);
                break;
            case 5:
                animator.SetBool("up", true);
                break;
            case 6:
                animator.SetBool("down", false);
                break;
            case 7:
                animator.SetBool("idle", true); animator.SetBool("up", false);
                break;
            case 8:
                option = 0;
                animator.SetBool("idle", false);
                break;
        }
    }

    void move()
    {
        animatorState = animator.GetCurrentAnimatorStateInfo(0);
        if (animatorState.IsName("Esqueleto_MoveUp")) { transform.Translate(Vector2.up * MOVE_SPEED * Time.deltaTime); }
        if (animatorState.IsName("Esqueleto_MoveDown")) { transform.Translate(Vector2.down * MOVE_SPEED * Time.deltaTime); }
    }
    // Update is called once per frame
    void Update () {
        typeOfMovement();
        move();
        if (!waitActive) { StartCoroutine(Wait()); }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Animator heroAnimator = collision.gameObject.GetComponent<Animator>();
        if (collision.gameObject.tag == "Hero" && ( heroAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Front")
            || heroAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Left")
            || heroAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Right")
            || heroAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Down") ) ) {
            currentLife--;
            Debug.Log("Esqueleto collided with Hero"+currentLife);
            if (currentLife == 0) { Destroy(this.gameObject); currentLife = LIFE;   }
        }
    }
}
