using UnityEngine;
using System.Collections;

public class snakemove : MonoBehaviour {
    public GameObject drum;
    private bool flashed = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var y = this.transform.position.y;
        var hit = drum.GetComponent<drumChange>().hit;
        if (y >= 25)
        {
            Destroy(this.gameObject);
        }
        else if (y >= 17 && hit)
        {
            flashed = true;
            drum.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f);
            Destroy(this.gameObject);
        }
        else if (y >= 23 && !flashed)
        {
            drum.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
        }
    }
}
