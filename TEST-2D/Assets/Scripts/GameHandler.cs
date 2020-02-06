using UnityEngine;
using System.Collections;
using System.IO.Ports;
using CodeMonkey;
using CodeMonkey.Utils;
using MidiParser;
using Scheduler;

public class GameHandler : MonoBehaviour {

    public GameObject snakePrefab;
    public GameObject[] drums;
    private drumChange[] drumChanges;
    private float[] drumPositions;
    public SerialPort serial;
    private list<DrumEvent> schedule;

    // Use this for initialization
    void Start () {
        Debug.Log("GameHandler.Start");
        var midiFile = new MidiFile("song.mid");

        foreach (var track in midiFile.Tracks) {
            foreach (var midiEvent in track.MidiEvents) {
                if (midiEvent.Channel == 10) {
                    if (midiEvent.MidiEventType == "NoteOn") {
                        if (midiEvent.Arg2 == 36) {
                            DrumEvent newDrumEvent = new DrumEvent(midiEvent.Time, 0)
                        }
                        else if (midiEvent.Arg2 == 40) {
                            DrumEvent newDrumEvent = new DrumEvent(midiEvent.Time, 1)
                        }
                        else if (midiEvent.Arg2 == 42) {
                            DrumEvent newDrumEvent = new DrumEvent(midiEvent.Time, 2)
                        }
                        else {
                            DrumEvent newDrumEvent = new DrumEvent(midiEvent.Time, 3)
                        }
                        schedule.Add(newDrumEvent);
                    }
                }
            }
        }

        serial = new SerialPort();
        serial.PortName = "COM3";
        serial.BaudRate = 9600;
        serial.Open();

        drumChanges = new drumChange[4];
        drumPositions = new float[4];
        for (int i = 0; i < drums.Length; i++)
        {
            drumChanges[i] = drums[i].GetComponent<drumChange>();
            drumPositions[i] = drums[i].transform.position.x;
        }

    }

    // Update is called once per frame
    int counter = 0;
    int scheduleCount;
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

            drumChanges[3].hit = (leftstick == 1) || (rightstick == 1);
            drumChanges[2].hit = (leftstick == 3) || (rightstick == 3);
            drumChanges[1].hit = (leftstick == 5) || (rightstick == 5);
            drumChanges[0].hit = (leftstick == 7) || (rightstick == 7);

            drumstate = newdrumstate;
        } catch (System.FormatException e)
        {
        }
        

        if (counter == schedule[scheduleCount].Time)
        {
            
            
                createSnake(schedule[scheduleCount].drum);
                if (scheduleCount+1 < schedule.Count) {
                    while (schedule[scheduleCount].time == schedule[scheduleCount+1].time) {
                        scheduleCount++;
                        createSnake(schedule[scheduleCount].drum);
                        if (scheduleCount+1 >= schedule.Count) {
                            break;
                        }
                    }
                }
                
            
        }
        counter++;
    }

    void createSnake(int col)
    {
        GameObject body = (GameObject)Instantiate(snakePrefab, new Vector3(drumPositions[col], -30f, 0), Quaternion.identity);
        body.GetComponent<snakemove>().drum = drums[col];
        body.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 20);
    }
}
