using UnityEngine;
using System.Collections;

public class drumChange : MonoBehaviour {

    // Use this for initialization
    public bool hit = false;

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
        if (hit)
        {
            GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        //Destroy(gameObject);
        Destroy(coll.gameObject);

        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
        }
        // want object to "glow" when hit, replacing drum temporarily with "test" sprite
        // line below does not work
        //this.gameObject.GetComponent<SpriteRenderer>().sprite = test;
    }

}
