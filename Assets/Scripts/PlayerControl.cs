using UnityEngine;
using System.Collections;

public class PlayerControl:MonoBehaviour {

    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;


    public float moveForce = 10f;
    public float maxSpeed = 5f;
    public AudioClip[] jumpClips;
    public float jumpForce = 1000f;
    public AudioClip[] taunts;
    public float tauntProbility = 50f;
    public float tauntDelay = 1f;

    private int tauntIndex;
    private Transform groundCheck;
    private bool grounded = false;
    private Animator anim;

    private void Awake() {
        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        grounded = Physics2D.Linecast(transform.position,groundCheck.position,1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded) {
            jump = true;
        }

    }

    private void FixedUpdate() {

        float h = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed",Mathf.Abs(h));

        if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed) {

            GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
           // transform.Translate(Vector2.right * h * maxSpeed);
        }

        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed) {

            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed,GetComponent<Rigidbody2D>().velocity.y);

            //transform.Translate(Vector2.right * h * maxSpeed);
        }

        if (h > 0 && !facingRight)
        {
            Flip();
        }
        else if (h < 0 && facingRight) {
            Flip();
        }


        if (jump)
        {
            // Set the Jump animator trigger parameter.
            anim.SetTrigger("Jump");

            // Play a random jump audio clip.
            int i = Random.Range(0,jumpClips.Length);
            //AudioSource.PlayClipAtPoint(jumpClips[i],transform.position);

            // Add a vertical force to the player.
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }

    }


    void Flip() {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
