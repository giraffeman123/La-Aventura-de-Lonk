using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public Animator animator;
    public GameObject arrowUp;
    public GameObject arrowLeft;
    public GameObject arrowDown;
    public GameObject arrowRight;
    private int LIFE = 8;
    public static int currentLife;
    private float MOVE_SPEED = 3f;
    private float ARROW_THRUST = 10f;
    //private bool waitActive = false;
    private Vector3 HeroPosition;


	// Use this for initialization
	void Start () {
        currentLife = LIFE;
    }

    // Update is called once per frame
    void Update () {
        HeroPosition = this.gameObject.transform.position;
        move();
        attack();
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Esqueleto" && (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Front")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Left")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Right")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Down"))
            || collision.gameObject.tag == "Rayo") { currentLife--; Debug.Log("Hero collided with Esqueleto" + currentLife); }
        if (collision.gameObject.tag == "BeeMan" && (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Front")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Left")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Right")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Down")) ) { currentLife -= 3; Debug.Log("Hero collided with BeeMan" + currentLife); }
        if (collision.gameObject.tag == "Jelka" && (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Front")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Left")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Right")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Down")) ) { currentLife -= 5; Debug.Log("Hero collided with BeeMan" + currentLife); }
        else { Debug.Log("Hero attacked Esqueleto" + currentLife); }
    }

    void attack()
    {
        if (Input.GetKey(KeyCode.J) && !animator.GetBool("Down") && !animator.GetBool("Left")
            && !animator.GetBool("Right") && !animator.GetBool("Up")) { animator.SetBool("Arrow", true); }
        if (Input.GetKeyUp(KeyCode.J)) { animator.SetBool("Arrow", false); }

        if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.L) && !animator.GetBool("Down") && !animator.GetBool("Left")
    && !animator.GetBool("Right") && !animator.GetBool("Up")) {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Arrow_Up_Idle")) {
                Vector3 flechaPos = HeroPosition;
                flechaPos.y = flechaPos.y + 1f;
                GameObject flecha = Instantiate(arrowUp,flechaPos,Quaternion.identity) as GameObject;
                flecha.GetComponent<Rigidbody2D>().velocity = new Vector3(0, ARROW_THRUST,0);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Arrow_Right_Idle")) {
                Vector3 flechaPos = HeroPosition;
                flechaPos.x = flechaPos.x + 1f;
                GameObject flecha = Instantiate(arrowRight, flechaPos, Quaternion.identity) as GameObject;
                flecha.GetComponent<Rigidbody2D>().velocity = new Vector3(ARROW_THRUST, 0, 0);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Arrow_Down_Idle")) {
                Vector3 flechaPos = HeroPosition;
                flechaPos.y = flechaPos.y - 1f;
                GameObject flecha = Instantiate(arrowDown, flechaPos, Quaternion.identity) as GameObject;
                flecha.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -ARROW_THRUST, 0);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Arrow_Left_Idle")) {
                Vector3 flechaPos = HeroPosition;
                flechaPos.x = flechaPos.x - 1f;
                GameObject flecha = Instantiate(arrowLeft, flechaPos, Quaternion.identity) as GameObject;
                flecha.GetComponent<Rigidbody2D>().velocity = new Vector3(-ARROW_THRUST, 0,0);
            }
        }
        //if (Input.GetKeyUp(KeyCode.J)) { Debug.Log("--------------"); }

        if (Input.GetKey(KeyCode.K)) { animator.SetBool("F",true); }
        if (Input.GetKeyUp(KeyCode.K)) { animator.SetBool("F", false); }
    }

    void move()
    {
        //********** UP
        if (Input.GetKey(KeyCode.W) && !animator.GetBool("Down") && !animator.GetBool("Left") && !animator.GetBool("Right") && !animator.GetBool("Arrow"))
        {
            animator.SetBool("Up", true);
            transform.Translate(Vector2.up * MOVE_SPEED * Time.deltaTime);
        }
        else if (Input.GetKeyUp(KeyCode.W))
            animator.SetBool("Up", false);

        //********** DOWN
        if (Input.GetKey(KeyCode.S) && !animator.GetBool("Up") && !animator.GetBool("Left") && !animator.GetBool("Right") && !animator.GetBool("Arrow"))
        {
            animator.SetBool("Down", true);
            transform.Translate(Vector2.down * MOVE_SPEED * Time.deltaTime);
        }
        else if (Input.GetKeyUp(KeyCode.S))
            animator.SetBool("Down", false);

        //********** LEFT
        if (Input.GetKey(KeyCode.A) && !animator.GetBool("Down") && !animator.GetBool("Up") && !animator.GetBool("Right") && !animator.GetBool("Arrow"))
        {
            animator.SetBool("Left", true);
            transform.Translate(Vector2.left * MOVE_SPEED * Time.deltaTime);
        }
        else if (Input.GetKeyUp(KeyCode.A))
            animator.SetBool("Left", false);

        //********** RIGHT
        if (Input.GetKey(KeyCode.D) && !animator.GetBool("Down") && !animator.GetBool("Left") && !animator.GetBool("Up") && !animator.GetBool("Arrow"))
        {
            animator.SetBool("Right", true);
            transform.Translate(Vector2.right * MOVE_SPEED * Time.deltaTime);
        }
        else if (Input.GetKeyUp(KeyCode.D))
            animator.SetBool("Right", false);
    }
}
