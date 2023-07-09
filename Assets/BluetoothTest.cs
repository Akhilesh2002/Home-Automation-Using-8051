using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluetoothTest : MonoBehaviour
{
    public Text deviceName;
    public Text dataToSend;
    private bool IsConnected;
    public static string dataRecived = "";
    // Start is called before the first frame update
    void Start()
    {
        IsConnected = false;
        BluetoothService.CreateBluetoothObject();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (IsConnected) {
            try
            {
               string datain =  BluetoothService.ReadFromBluetooth();
                if (datain.Length > 1)
                {
                    dataRecived = datain;
                    print(dataRecived);
                }

            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        
    }

    public void StartButton()
    {
        if (!IsConnected)
        {
            print(deviceName.text.ToString() + "are tried to connect");
            IsConnected =  BluetoothService.StartBluetoothConnection(deviceName.text.ToString());
        }
    }

    public void SendButton()
    {
        if (IsConnected && (dataToSend.ToString() != "" || dataToSend.ToString() != null))
        {
            BluetoothService.WritetoBluetooth(dataToSend.text.ToString());
        }
    }


    public void StopButton()
    {
        if (IsConnected)
        {
            BluetoothService.StopBluetoothConnection();
        }
        Application.Quit();
    }
}
