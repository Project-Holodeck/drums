using UnityEngine;
using System.Collections;

public class snakemove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(new Vector3(0, 10*Time.deltaTime, 0));
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("We hit something, captain!");
    }
}
