using UnityEngine;
using System.Collections;

public class Snakedestroy : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (this.transform.position.y >= 20 && this.transform.position.y <= 25)
        {
            //Destroy(gameObject);
        }

    }
}
