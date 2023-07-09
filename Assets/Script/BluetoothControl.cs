using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BluetoothControl : MonoBehaviour
{
    public TMP_Text deviceName;
    //public Text dataToSend;
    private bool IsConnected;
    public static string dataRecived = "";

    [SerializeField] GameObject Environment;
    [SerializeField] GameObject DeviceSetupUI;

    // Start is called before the first frame update
    void Start()
    {
        IsConnected = false;
        BluetoothService.CreateBluetoothObject();

        Environment.SetActive(false);
        DeviceSetupUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsConnected)
        {
            try
            {
                string datain = BluetoothService.ReadFromBluetooth();
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

    // Setup Bluetooth Device 
    public void StartButton()
    {
        if (!IsConnected)
        {
            print(deviceName.text.ToString() + "are tried to connect");
            IsConnected = BluetoothService.StartBluetoothConnection(deviceName.text.ToString());
            if (IsConnected)
            {
                Environment.SetActive(true);
                DeviceSetupUI.SetActive(false);
            }
        }
    }

    // Sending Data 
    public void SendButton(string dataToSend)
    {
        if (IsConnected && (dataToSend != "" || dataToSend != null))
        {
            BluetoothService.WritetoBluetooth(dataToSend);
        }
    }

    // End Connection  
    public void StopButton()
    {
        if (IsConnected)
        {
            BluetoothService.StopBluetoothConnection();
        }
        Application.Quit();
    }
}
