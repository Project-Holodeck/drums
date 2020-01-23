using UnityEngine;
using System.Collections;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour {
    private static float[] COL_POSITION = { -16.66f, -8.33f, 0f, 8.33f, 16.66f };

    public GameObject snakePrefab;

    // Use this for initialization
    void Start () {
        Debug.Log("GameHandler.Start");
    }

    // Update is called once per frame
    int counter = 0;
    void Update()
    {
        counter++;

        if (counter == 60 * 2)
        {
            counter = 0;
            for (int i = 0; i < 5; i++)
            {
                createSnake(i);
            }
        }
    }

    void createSnake(int col)
    {
        GameObject body = (GameObject)Instantiate(snakePrefab, new Vector3(COL_POSITION[col], -30f, 0), Quaternion.identity);
        body.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Random.Range(10, 15));
    }
}
