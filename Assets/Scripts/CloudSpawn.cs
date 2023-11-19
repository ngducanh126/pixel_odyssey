using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawn : MonoBehaviour
{
    public GameObject[] clouds;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnCloud", 0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnCloud() {
        int random = Random.Range(0, clouds.Length);
        Instantiate(clouds[random], new Vector2(GameObject.Find("Player").transform.position.x + 10, Random.Range(0, 6.05f)), transform.rotation);
    }
}
