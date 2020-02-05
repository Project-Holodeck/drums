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
    void Update()
    {
        //    this.transform.Translate(new Vector3(0, 10 * Time.deltaTime, 0));
        sr.color = Color.Lerp(sr.color, Color.white, Time.deltaTime / 0.25f); // slowly linear interpolate. takes about 3 seconds to return to white

        if (((Vector4) (sr.color - Color.white)).magnitude <= 0.4 && hit)
        {
            sr.color = Color.red;
        }
    }
}
