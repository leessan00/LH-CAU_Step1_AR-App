using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Linq;

public class GetData_by_ID_Date : MonoBehaviour {

    public Dropdown ID_dropdown;
    public Dropdown Time_dropdown;

    public Text text_id;
    public Text text_date;
    public Text text_time;

    public Text log_time;
    public Text log_temp;
    public Text log_humi;

    private string url = "http://13.209.75.135/api/H310_720/num/date/";

    private int Select_Id;
    private string Select_Date;
    private int Select_Time;
    
    public int Array_lth;

    public List<string> list;

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

    public CJsonarray array;




    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}


    public void DateValue(object message)
    {
        Select_Date = message.ToString();
        text_date.text = Select_Date;

        if (Select_Id != null && Select_Date != null)
        {
            StartCoroutine(GetJson());
        }
    }

    public void ID_dropdownValue()

    {
        //get the selected index
        int menuIndex = ID_dropdown.GetComponent<Dropdown>().value;

        //get all options available within this dropdown menu
        List<Dropdown.OptionData> menuOptions = ID_dropdown.GetComponent<Dropdown>().options;

        //get the string value of the selected index
        string value = menuOptions[menuIndex].text;

        text_id.text = value; // 드롭다운에서 선택한 디바이스 표시
        Select_Id = menuIndex + 1; // 1, 2, 3

        if (Select_Date != null)
        {
            StartCoroutine(GetJson());
            Set_Time_dropdown();
        }
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
        Time_dropdown.AddOptions(list);
        Time_dropdown.value = 0;
    }

    public void Get()  // 
    {      
        SetLog();
    }


    IEnumerator GetJson()
    {
        UnityWebRequest www = UnityWebRequest.Get(url + Select_Id + "/" + Select_Date);
        Debug.Log(url + Select_Id + "/" + Select_Date);
        yield return www.SendWebRequest();


        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
        }
        else
        {

            Debug.Log(www.downloadHandler.text);

            fixarray = fixJson(www.downloadHandler.text);

            array = JsonUtility.FromJson<CJsonarray>(fixarray);

         //   log_time.text = (array.data[0].time).ToString(); // time 파싱한거 출력

            Array_lth = array.data.Length;

            list.Clear();
            for (int i = 0; i < Array_lth; i++)
            {
                list.Add(array.data[i].time);
            }


        }

    }

    public void SetLog()
    {
        if (Select_Time != null)
        {
            log_time.text = (array.data[Select_Time].time).ToString(); // time 파싱한거 출력
            log_temp.text = (array.data[Select_Time].temperature).ToString(); // temperature 파싱한거 출력
            log_humi.text = (array.data[Select_Time].humidity).ToString(); // humidity 파싱한거 출력
        }


    }



    string fixJson(string value)
    {
        // value = "{\"data\":" + value + "}";
        return value;
    }







}
