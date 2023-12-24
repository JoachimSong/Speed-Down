using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    Vector3 speedVec3;
    GameObject topLine;


    void Start()
    {
        speedVec3.y = speed;
        topLine = GameObject.Find("TopLine");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        transform.position += speedVec3 * Time.deltaTime * GameManager.VelocityRate();
        if (transform.position.y > topLine.transform.position.y)
            Destroy(gameObject);
    }
}
