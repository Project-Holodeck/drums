using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using MidiParser;
using Scheduler;

public class GameHandler : MonoBehaviour {

    public GameObject snakePrefab;
    public GameObject[] drums;
    private drumChange[] drumChanges;
    private float[] drumPositions;
    public SerialPort serial;
    private List<DrumEvent> schedule = new List<DrumEvent>();
    
    int scheduleCount = 0;
    byte drumstate = 0;
    bool stillList = true;

    // Use this for initialization
    void Start () {
        Debug.Log("GameHandler.Start");

        string songNameNoExt = "test";

        // check for xml
        //if songNameNoExt + @".xml" exists in folder
        //  LoadSchedule
        //else
        PrepareSchedule(songNameNoExt + @".mid");
        //  SaveSchedule

        serial = new SerialPort();
        serial.PortName = "COM3";
        serial.BaudRate = 9600;
        serial.ReadTimeout = 100;
        serial.Open();

        drumChanges = new drumChange[4];
        drumPositions = new float[4];
        for (int i = 0; i < drums.Length; i++)
        {
            drumChanges[i] = drums[i].GetComponent<drumChange>();
            drumPositions[i] = drums[i].transform.position.x;
        }

    }

    void PrepareSchedule(string songName)
    {
        // Use external library to load midi file
        var midiFile = new MidiFile(songName);
        byte currTempo = 42;

        double ticksPerSecond = midiFile.TicksPerQuarterNote * currTempo / 60.0;
        int lastEventTime;
        
        double accumulatedTime;

        // For each track in midi
        foreach (var track in midiFile.Tracks)
        {
            lastEventTime = 0;
            accumulatedTime = 0;

            // For each sound event
            foreach (var midiEvent in track.MidiEvents)
            {
                // Channel 10 is drum
                if (midiEvent.MidiEventType == MidiEventType.NoteOn && midiEvent.Channel == 10)
                {
                    // Create new drum event
                    DrumEvent newDrumEvent;

                    accumulatedTime += (midiEvent.Time - lastEventTime) / ticksPerSecond;
                    
                    Debug.Log(midiEvent.Time);
                    lastEventTime = midiEvent.Time;

                    double drumTime = accumulatedTime;

                    // Assign drumpad based on drum type
                    switch (midiEvent.Arg2)
                    {
                        case 36:
                            newDrumEvent = new DrumEvent(drumTime, 0);
                            break;
                        case 40:
                            newDrumEvent = new DrumEvent(drumTime, 1);
                            break;
                        case 42:
                            newDrumEvent = new DrumEvent(drumTime, 2);
                            break;
                        default:
                            newDrumEvent = new DrumEvent(drumTime, 3);
                            break;
                    }

                    // Add to schedule
                    schedule.Add(newDrumEvent);
                }
                else if (midiEvent.MidiEventType == MidiEventType.MetaEvent && midiEvent.Arg1 == 0x51)
                {
                    ticksPerSecond = midiFile.TicksPerQuarterNote* currTempo / 60.0;
                    accumulatedTime += (midiEvent.Time - lastEventTime) / ticksPerSecond;
                    lastEventTime = midiEvent.Time;
                    currTempo = midiEvent.Arg2;
                }
            }
        }

        schedule.Sort();
    }

    // Update is called once per frame
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

        //Debug.Log(scheduleCount + ": " + schedule[scheduleCount].time + ":" + Time.time);
        
        // Check that it's the right time for the next drum event
        while (stillList && Time.time >= schedule[scheduleCount].time)
        {
            createSnake(schedule[scheduleCount].drum);
            scheduleCount++;
            if (scheduleCount >= schedule.Count)
            {
                stillList = false;
                break;
            }
        }
    }

    void createSnake(int col)
    {
        GameObject body = (GameObject)Instantiate(snakePrefab, new Vector3(drumPositions[col], -30f, 0), Quaternion.identity);
        body.GetComponent<snakemove>().drum = drums[col];
        body.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 20);
    }
}
