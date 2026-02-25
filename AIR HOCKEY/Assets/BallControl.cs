using UnityEngine;

public class BallControl : MonoBehaviour {
    private Rigidbody2D rb2d;
    private AudioSource audioSource;

    public AudioClip hitPlayerSound; // som da colisão com raquete
    public AudioClip hitWallSound;   // som da colisão com parede

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        Invoke("GoBall", 2);
    }

    void GoBall() {
        float rand = Random.Range(0, 2);
        if(rand < 1){
            rb2d.AddForce(new Vector2(1, 3));
        } else {
            rb2d.AddForce(new Vector2(-1, -3));
        }
    }

    void Update() {
        var pos = transform.position;
        if (pos.y > 7.5) {
            pos.y = 2;
            pos.x = 0;
            rb2d.linearVelocity = Vector2.zero;
        } else if (pos.y < -7.5) {
            pos.y = -2;
            pos.x = 0;
            rb2d.linearVelocity = Vector2.zero;
        } else if (pos.x > 4.5) {
            pos.x = 4.5f;
        } else if (pos.x < -4.5) {
            pos.x = -4.5f;
        }
        transform.position = pos;
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if(coll.collider.CompareTag("Player")) {
            // comportamento da bola
            Vector2 vel;
            vel.x = rb2d.linearVelocity.x / 2;
            vel.y = rb2d.linearVelocity.y / 2;
            rb2d.linearVelocity = vel;

            // som da raquete
            if (hitPlayerSound != null) {
                audioSource.PlayOneShot(hitPlayerSound);
            }
        } else if(coll.collider.CompareTag("Respawn")) {
            // som da parede
            if (hitWallSound != null) {
                audioSource.PlayOneShot(hitWallSound);
            }
        }
    }
}
