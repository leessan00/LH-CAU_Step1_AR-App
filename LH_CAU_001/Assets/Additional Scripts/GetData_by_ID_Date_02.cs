using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Linq;

public class GetData_by_ID_Date_02 : MonoBehaviour
{

    public Dropdown Time_dropdown;

    public Text text_date;
    public Text text_time;

    public Text log_temp01;
    public Text log_humi01;
    public Text log_temp02;
    public Text log_humi02;
    public Text log_temp03;
    public Text log_humi03;

    private string url = "http://13.209.75.135/api/H310_720/num/date/";

    private string Select_Date;
    private int Select_Time;

    public int Array_lth;

    public List<string> list01;
    public List<string> list02;
    public List<string> list03;


    [System.Serializable]
    public class CJson
    {

        public String time;
        public int temperature;
        public int humidity;

    }

    [System.Serializable]
    public class CJsonarray
    {
        public CJson[] data;
    }

    public string fixarray;

    public CJsonarray array01;
    public CJsonarray array02;
    public CJsonarray array03;




    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void DateValue(object message)
    {
        Select_Date = message.ToString();
        text_date.text = Select_Date;

            StartCoroutine(GetJson01());
            StartCoroutine(GetJson02());
            StartCoroutine(GetJson03());
            Set_Time_dropdown();

    }

    public void Time_dropdownValue()
    {
        //get the selected index
        int menuIndex = Time_dropdown.GetComponent<Dropdown>().value;

        //get all options available within this dropdown menu
        List<Dropdown.OptionData> menuOptions = Time_dropdown.GetComponent<Dropdown>().options;

        //get the string value of the selected index
        string value = menuOptions[menuIndex].text;

        text_time.text = value; // 드롭다운에서 선택한 디바이스 표시
        Select_Time = menuIndex; //
    }

    public void Set_Time_dropdown() // 날짜 선택 -> 디바이스 선택 -> 데이터 파싱 -> 시간 드롭다운 설정
    {
        Time_dropdown.ClearOptions();
        Time_dropdown.AddOptions(list01);
        Time_dropdown.value = 0;
    }

    public void Get()  // 
    {
        SetLog();
    }


    IEnumerator GetJson01()
    {
        UnityWebRequest www = UnityWebRequest.Get(url + "1" + "/" + Select_Date);
        yield return www.SendWebRequest();


        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
        }
        else
        {

            Debug.Log(www.downloadHandler.text);

            fixarray = fixJson(www.downloadHandler.text);

            array01 = JsonUtility.FromJson<CJsonarray>(fixarray);

            Array_lth = array01.data.Length;

            list01.Clear();
            for (int i = 0; i < Array_lth; i++)
            {
                list01.Add(array01.data[i].time);
            }


        }

    }

    IEnumerator GetJson02()
    {
        UnityWebRequest www = UnityWebRequest.Get(url + "2" + "/" + Select_Date);
        yield return www.SendWebRequest();


        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
        }
        else
        {

            Debug.Log(www.downloadHandler.text);

            fixarray = fixJson(www.downloadHandler.text);

            array02 = JsonUtility.FromJson<CJsonarray>(fixarray);


        }

    }

    IEnumerator GetJson03()
    {
        UnityWebRequest www = UnityWebRequest.Get(url + "3" + "/" + Select_Date);
        yield return www.SendWebRequest();


        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
        }
        else
        {

            Debug.Log(www.downloadHandler.text);

            fixarray = fixJson(www.downloadHandler.text);

            array03 = JsonUtility.FromJson<CJsonarray>(fixarray);



        }

    }

    public void SetLog()
    {
        if (Select_Time != null)
        {
            log_temp01.text = (array01.data[Select_Time].temperature).ToString(); // temperature 파싱한거 출력
            log_humi01.text = (array01.data[Select_Time].humidity).ToString(); // humidity 파싱한거 출력

            log_temp02.text = (array02.data[Select_Time].temperature).ToString(); // temperature 파싱한거 출력
            log_humi02.text = (array02.data[Select_Time].humidity).ToString(); // humidity 파싱한거 출력

            log_temp03.text = (array03.data[Select_Time].temperature).ToString(); // temperature 파싱한거 출력
            log_humi03.text = (array03.data[Select_Time].humidity).ToString(); // humidity 파싱한거 출력
        }


    }



    string fixJson(string value)
    {
        // value = "{\"data\":" + value + "}";
        return value;
    }







}
