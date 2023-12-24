using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragilePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    private float breakTimer;
    private bool breakStart;
    void Start()
    {
        breakTimer = 0;
        breakStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (breakStart)
        {
            breakTimer += Time.deltaTime;
            if (breakTimer >= 3)
            {
                Destroy(gameObject);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            breakStart = true;
        }
    }
}
