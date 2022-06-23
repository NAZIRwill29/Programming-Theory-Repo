using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    [SerializeField]
    private float jumpForce = 10;
    [SerializeField]
    private float gravityModifier = 1;
    private bool isOnGround = true;
    public bool gameOver;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem bloodParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        //get component
        playerRb = GetComponent<Rigidbody>();

        //set gravity
        Physics.gravity *= gravityModifier;

        //get component player animator
        playerAnim = GetComponent<Animator>();

        //get component audio source
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if spacebar pressed & is on ground & not game over
        if (Input.GetButtonDown("Jump") && isOnGround && !gameOver)
        {
            PlayerJump();
        }
    }

    void PlayerJump()
    {
        //add force to rigidbody
        //AddForce(howManyForce, Forcemode)
        //ForceMode.impulse - aplly force immediately
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        //make player unable to jump twice
        isOnGround = false;

        //make jump animation
        playerAnim.SetTrigger("Jump_trig");

        //make dirt paricle stop when jump
        dirtParticle.Stop();

        //make jump sound
        //PlayOneShot = play one only
        //PlayOneShot(sound, volume)
        playerAudio.PlayOneShot(jumpSound, 1.0f);
    }

    //function run when collide with other object
    //happen when player touch ground
    private void OnCollisionEnter(Collision collision)
    {

        //check coliide with what type of obj
        //collide with ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            //make player can jump again
            isOnGround = true;

            //make dirt paricle
            dirtParticle.Play();
        }
        //check collide obstacle
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            //when game over
            //Debug.Log("Game Over");
            //gameOver = true;

            //for player death
            //playerAnim.SetBool("Death_b", true);
            //playerAnim.SetInteger("DeathType_int", 1);

            //make dirt paricle stop when dead
            //dirtParticle.Stop();

            HitObstacle(collision);
        }
        //check collide with animal
        else
        {
            HitAnimal(collision);
        }
    }

    void HitObstacle(Collision collision)
    {
        //make explosion particle effect
        explosionParticle.Play();

        //make crash sound
        playerAudio.PlayOneShot(crashSound, 1.0f);

        StartCoroutine(DestroyWait(0.5f, collision));
    }

    void HitAnimal(Collision collision)
    {
        //make explosion particle effect
        bloodParticle.Play();

        //make crash sound
        playerAudio.PlayOneShot(crashSound, 1.0f);

        StartCoroutine(DestroyWait(0.1f, collision));
    }

    //destroy object
    IEnumerator DestroyWait(float waitTime, Collision collision)
    {
        yield return new WaitForSeconds(waitTime);

        //check if collision exist
        if (collision.gameObject)
        {
            Destroy(collision.gameObject);
        }
    }

}
