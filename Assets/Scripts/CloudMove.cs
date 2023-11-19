using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    Vector2 target;
    // Start is called before the first frame update
    void Start()
    {
        target = new Vector2(-20, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(target, transform.position) < .1f)
        {
            Destroy(gameObject);
        }

        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * .3f);

    }
}
