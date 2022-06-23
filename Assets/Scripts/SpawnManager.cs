using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] obstaclePrefabs;
    private Vector3 spawnPos = new Vector3(30, 0, 0);
    private float startDelay = 2;
    private float repeatRate = 2;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        //call spawn obstacle
        Invoke("SpawnObstacle", startDelay);
        //get player controller in player gameObj
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //function spawn obstacle
    void SpawnObstacle()
    {
        int randomRepetitionSpawn = Random.Range(1, 2);
        int randomNumObs = Random.Range(0, obstaclePrefabs.Length);
        //check if gameOver
        if (playerControllerScript.gameOver == false)
        {
            //spawn object
            Instantiate(obstaclePrefabs[randomNumObs], spawnPos, obstaclePrefabs[randomNumObs].transform.rotation);
        }
        //call method again
        Invoke("SpawnObstacle", randomRepetitionSpawn);
    }
}
