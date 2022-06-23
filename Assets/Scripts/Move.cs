using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float speed = 20;
    private PlayerController playerControllerScript;
    private float leftBound = -15;
    // Start is called before the first frame update
    void Start()
    {
        //get gameobject of player
        //get component of player controller u=in player
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if game Over
        if (playerControllerScript.gameOver == false)
        {
            MoveObject();
        }
        //destroy obstacle that out of bound
        //check if outbound
        if ((transform.position.x < leftBound && gameObject.CompareTag("Obstacle")) || (transform.position.x < leftBound && gameObject.CompareTag("Animal")))
        {
            Destroy(gameObject);
        }
    }

    //move object
    void MoveObject()
    {
        if (gameObject.CompareTag("Animal"))
        {
            //move forward
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        else
        {
            //move left
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
}
