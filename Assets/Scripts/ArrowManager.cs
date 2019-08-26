using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour {

    private Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Arrow_Down") || animator.GetCurrentAnimatorStateInfo(0).IsName("Arrow_Left")
             || animator.GetCurrentAnimatorStateInfo(0).IsName("Arrow_Right") || animator.GetCurrentAnimatorStateInfo(0).IsName("Arrow_Up"))
            Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Corner" || collision.gameObject.tag == "Arrow") {  animator.SetBool("isCollided", true); Debug.Log("Collided with corner"); }
        if (collision.gameObject.tag == "Arbol Ojo Loco") { animator.SetBool("isCollided", true); Debug.Log("Collided with ojo loco"); }
        if (collision.gameObject.tag == "Esqueleto") { animator.SetBool("isCollided", true); Debug.Log("Collided with Esqueleto"); }
        if (collision.gameObject.tag == "BeeMan") { animator.SetBool("isCollided", true); Debug.Log("Collided with Esqueleto"); }
        if (collision.gameObject.tag == "Jelka") { animator.SetBool("isCollided", true); Debug.Log("Collided with Jelka"); }
        //if (collision.gameObject.tag == "Arbol Ojo Loco") { animator.SetBool("collision", true); Debug.Log("Collided with ojo loco"); }
    }
}
