using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles += new Vector3(0, 5.0f, 0);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            
        }

    }
}
