using UnityEngine;
using System.Collections;

public class Snakedestroy1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (this.transform.position.y >= 10 && this.transform.position.y <= 15)
        {
            Destroy(gameObject);
        }
        
    }
}
