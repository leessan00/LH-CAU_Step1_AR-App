using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using System;

public class GetData : MonoBehaviour
{

    private string Date;
    private string Time;
    private string Temp;
    private string Humi;
    private string Num;

    private int cnt = 0;

    private string scnt;

    [Header("H310_720")]
    public Text text_date;
    public Text text_time;
    public Text text_temp;
    public Text text_humi;
    public Text text_num;

    public Text getCnt;

    [System.Serializable]
    public class CJson
    {
        public String _id;
        public String date;
        public String time;
        public int temperature;
        public int humidity;
        public int num;
        public String __v;
    }

    [System.Serializable]
    public class CJsonarray
    {
        public CJson[] data;
    }


    // Use this for initialization
    void Start()
    {
        
        // InvokeRepeating("IncreaseNum", 1f, 1.0f);//0.0001초에 실행하고 1초마다 해당 함수를 실행합니다.
        Debug.Log("Script Test.cs Start");
    }

    // Update is called once per frame
    void Update()
    {


    }

    void IncreaseNum()
    {
        getCnt.text = cnt.ToString();

        cnt++;

        if (cnt > 6)
        {
            cnt = 0;
        }

        getCnt.text = cnt.ToString();

        scnt = getCnt.text.ToString();
        cnt = System.Convert.ToInt32(scnt);

        StartCoroutine(GetJson());

    }


    IEnumerator GetJson()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://13.209.75.135/api/H310_720");
        yield return www.SendWebRequest();


        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
        }
        else
        {

            Debug.Log(www.downloadHandler.text);

            string fixarray = fixJson(www.downloadHandler.text);

            CJsonarray array = JsonUtility.FromJson<CJsonarray>(fixarray);

            text_date.text = array.data[cnt].date; // date 파싱한거 출력
            text_time.text = array.data[cnt].time; // time 파싱한거 출력
            text_temp.text = (array.data[cnt].temperature).ToString(); // temperature 파싱한거 출력
            text_humi.text = (array.data[cnt].humidity).ToString(); // humidity 파싱한거 출력
            text_humi.text = (array.data[cnt].humidity).ToString(); // humidity 파싱한거 출력
            text_num.text = (array.data[cnt].num).ToString(); // num 파싱한거 출력


        }



    }



    string fixJson(string value)
    {
        // value = "{\"data\":" + value + "}";
        return value;
    }



    /*
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] datas;
        }
    }
    */



}
