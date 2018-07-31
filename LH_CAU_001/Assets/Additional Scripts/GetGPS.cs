﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GetGPS : MonoBehaviour
{

    bool gpsInit = false;

    LocationInfo currentGPSPosition;

    int gps_connect = 0;

    private double latitude;
    private double longitude;

    double detailed_num = 1.0;//기존 좌표는 float형으로 소수점 자리가 비교적 자세히 출력되는 double을 곱하여 자세한 값을 구합니다.


    [Header("GPS Coordinate")]
    public Text text_latitude;   
    public Text text_longitude;
    public Text text_refresh;

    public Text text_area;
    public Text text_number;
    public Text text_status;

    // Use this for initialization

    void Start()

    {

        Input.location.Start(0.5f);



        int wait = 1000; // 기본 값

        // Checks if the GPS is enabled by the user (-> Allow location )

        if (Input.location.isEnabledByUser)//사용자에 의하여 좌표값을 실행 할 수 있을 경우

        {

            while (Input.location.status == LocationServiceStatus.Initializing && wait > 0)//초기화 진행중이면

            {

                wait--; // 기다리는 시간을 뺀다

            }

            //GPS를 잡는 대기시간

            if (Input.location.status != LocationServiceStatus.Failed)//GPS가 실행중이라면

            {

                gpsInit = true;

                // We start the timer to check each tick (every 3 sec) the current gps position

                InvokeRepeating("RetrieveGPSData", 1f, 1.0f);//0.0001초에 실행하고 1초마다 해당 함수를 실행합니다.

            }

        }

        else//GPS가 없는 경우 (GPS가 없는 기기거나 안드로이드 GPS를 설정 하지 않았을 경우

        {

            text_latitude.text = "GPS not available";

            text_longitude.text = "GPS not available";

        }

    }

    void RetrieveGPSData()

    {

        currentGPSPosition = Input.location.lastData;//gps를 데이터를 받습니다.

        latitude = currentGPSPosition.latitude * detailed_num;
        text_latitude.text = latitude.ToString();  //위도 값을 받아,텍스트에 출력합니다

        longitude = currentGPSPosition.longitude * detailed_num;
        text_longitude.text = longitude.ToString();//경도 값을 받아, 텍스트에 출력합니다.

        gps_connect++;

        text_refresh.text = gps_connect.ToString();

       // SetArea();

    }

    public void SetArea()
    {
        if (37.50410461420570 < latitude && latitude < 37.50410461420580 && 126.956413269040 < longitude && longitude < 126.956413269050)
        {
            text_area.text = "H310";
        
        } else
        {
            text_area.text = "H207";
        }
    }

}