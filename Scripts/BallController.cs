using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour { 

    private bool ignoreNextCollision;
    public Rigidbody rb;
    public float impulseForce = 5f;
    private Vector3 startPos;
    public int perfectPass = 0;
    public bool isSpuperSpeedActive;

    // Start is called before the first frame update
    void Awake() {
        startPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision)
            return;

        if (isSpuperSpeedActive) { 
        if (!collision.transform.GetComponent<Goal>()) {
                Destroy(collision.transform.parent.gameObject, .5f);
                Debug.Log("Destroying platform");
            }
        }
        else
        {
            // adding reset level lol i a gonnnna rest lolllllll
            DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
            if (deathPart)
                deathPart.HitDeathPart();
        }
        
        // Debug.Log("Ball touched something");

        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);

        ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);

        perfectPass = 0;
        isSpuperSpeedActive = false;
    }

    private void Update()
    {
        if (perfectPass >= 3 && !isSpuperSpeedActive) {
            isSpuperSpeedActive = true;
            rb.AddForce(Vector3.down * 10, ForceMode.Impulse);
        }
    }

    private void AllowCollision() { 
        ignoreNextCollision = false;
    }

    public void ResetBall() {
        transform.position = startPos;
    }
}
