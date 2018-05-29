using UnityEngine;
using System.Collections;

public class PlayerCtr:MonoBehaviour {

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float maxSpeed = 500f;

    private Rigidbody2D rigidbody;


    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {

        float h = Input.GetAxis("Horizontal");


        rigidbody.AddForce(Vector2.right * h * speed);

        if (Input.GetButtonDown("Jump")) {

            rigidbody.AddForce(Vector2.up * maxSpeed);
        }

    }
}
