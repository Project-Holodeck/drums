using UnityEngine;
using System.Collections;
using System.IO.Ports;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour {
    private static float[] COL_POSITION = { -16.66f, -4.6f, 6.2f, 17.5f };

    public GameObject snakePrefab;
    public GameObject[] drums;
    private drumChange[] drumChanges;
    public SerialPort serial;

    // Use this for initialization
    void Start () {
        Debug.Log("GameHandler.Start");

        serial = new SerialPort();
        serial.PortName = "COM3";
        serial.BaudRate = 9600;
        serial.Open();

        drumChanges = new drumChange[4];
        for (int i = 0; i < drums.Length; i++)
        {
            drumChanges[i] = drums[i].GetComponent<drumChange>();
        }
    }

    // Update is called once per frame
    int counter = 0;
    byte drumstate = 0;

    void Update()
    {
        try
        {
            byte newdrumstate = byte.Parse(serial.ReadLine());
            //while (serial.BytesToRead > 0) newdrumstate = byte.Parse(serial.ReadLine());
            serial.DiscardInBuffer();

            byte leftstick = 0;
            byte rightstick = 0;

            if (((drumstate >> 3) & 7) == 0) leftstick = (byte) ((newdrumstate >> 3) & 7);
            if ((drumstate & 7) == 0) rightstick = (byte)(newdrumstate & 7);

            if (leftstick > 0 || rightstick > 0)
            {
                Debug.Log(leftstick);
                Debug.Log(rightstick);
            }

            drumChanges[3].hit = (leftstick == 1) || (rightstick == 1);
            drumChanges[2].hit = (leftstick == 3) || (rightstick == 3);
            drumChanges[1].hit = (leftstick == 5) || (rightstick == 5);
            drumChanges[0].hit = (leftstick == 7) || (rightstick == 7);

            drumstate = newdrumstate;
        } catch (System.FormatException e)
        {
        }
        counter++;

        if (counter == 60 * 2)
        {
            counter = 0;
            for (int i = 0; i < 4; i++)
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
