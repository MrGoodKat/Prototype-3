using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRb;
    public ParticleSystem playerDeathParticle;
    public ParticleSystem dirtParticle;
    private Animator playerAnim;
    public AudioClip crashSFX, jumpSFX;
    private AudioSource audioSource;
    public float jumpForce = 10f;
    public float gravityForce = 2f;
    public bool isOnGround = true;
    public bool isGameOver = false;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.25f;
        Physics.gravity *= gravityForce;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !isGameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            isOnGround = false;
            audioSource.PlayOneShot(jumpSFX);
            dirtParticle.Stop();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isGameOver)
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            isGameOver = true;
            playerAnim.SetInteger("DeathType_int", 1);
            playerAnim.SetBool("Death_b", true);
            playerDeathParticle.Play();
            dirtParticle.Stop();
            audioSource.PlayOneShot(crashSFX);
            Debug.Log("Game Over");
        }
        
    }
}
