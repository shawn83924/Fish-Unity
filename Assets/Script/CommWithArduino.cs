using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
public class CommWithArduino : MonoBehaviour
{

    string portName ="COM4";
    public int baudRate = 9600;
    SerialPort arduinoSerial;
    public bool collisionGarbage = false;
    public bool inflate = false;
    public bool deflate = false;
    public bool electricShock = false;
    // Start is called before the first frame update
    void Start()
    {
        arduinoSerial = new SerialPort("COM4", baudRate);
        //arduinoSerial.ReadTimeout = 10;
        arduinoSerial.Open();
    }

    // Update is called once per frame
    void Update()
    {
        if (arduinoSerial.IsOpen)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                collisionGarbage = true;
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                collisionGarbage = false;
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                inflate = true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                deflate = true;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                electricShock = true;
            }

            if (collisionGarbage)
            {
                arduinoSerial.WriteLine("1\n");
            }
            if (!collisionGarbage)
            {
                arduinoSerial.WriteLine("0\n");
            }
            if (inflate)
            {             
                inflate = false;
                arduinoSerial.WriteLine("2\n");
            }
            if (deflate)
            {
                deflate = false;
                arduinoSerial.WriteLine("3\n");
            }
            if (electricShock)
            {
                electricShock = false;
                arduinoSerial.WriteLine("4\n");
            }
        }
    }

}
