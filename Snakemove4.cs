using UnityEngine;
using System.Collections;

public class Snakemove4 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(new Vector3(0, 8 * Time.deltaTime, 0));
    }
}
