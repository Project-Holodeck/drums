using UnityEngine;
using System.Collections;

public class drumChange : MonoBehaviour {

    // Use this for initialization
    SpriteRenderer sr;
    public Sprite test;
    void Start () {
        //  this.gameObject.AddComponent<BoxCollider2D>();
 
        sr = GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        //    this.transform.Translate(new Vector3(0, 10 * Time.deltaTime, 0));
    this.gameObject.AddComponent<BoxCollider2D>();
        sr.color = Color.Lerp(sr.color, Color.white, Time.deltaTime / 0.25f); // slowly linear interpolate. takes about 3 seconds to return to white
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        Debug.Log("We hit something, captain!");
        if (coll.gameObject.name == "Snake"){ }
        {
            //Destroy(gameObject);
            Destroy(coll.gameObject);
            GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
            // want object to "glow" when hit, replacing drum temporarily with "test" sprite
            // line below does not work
            //this.gameObject.GetComponent<SpriteRenderer>().sprite = test;
        }
        
    }

}
