using UnityEngine;
using System.Collections;

public class Snakemove3 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(new Vector3(0, 15 * Time.deltaTime, 0));
    }
}
