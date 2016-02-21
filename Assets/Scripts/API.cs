using UnityEngine;
using System.Collections;
using LitJson;

public class API : MonoBehaviour
{

    string url = "http://opencorpora.org:8080/api.php";
    string token = "";
    public static int user_id = 0;

    static API inst;

    void Start()
    {
        if (inst != null && inst != this) Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            inst = this;
        }

     
   /*
        string s = "{\"login\": \"test\", \"password\": \"test\"}";

        SendRequest("login_test", JsonMapper.ToObject(s), GetData);
        JsonData data = JsonMapper.ToObject(s);
        print("s - ok  " + data["atr1"]);

        print(data.ToJson());


        if (data.Keys.Contains("error")) print("error detected  " + data["error"]);
        else
            print("its ok");
        */
    }
    /*
    void GetData(JsonData msg)
    {
        print(msg.ToJson());
        Message.instance.MessageInfo(msg.ToJson(), 2);
    }
    */
    /// <summary>
    /// Отправка запроса серверу, с возвратом ответа об ошибке в указанный метод
    /// </summary>
    /// <param name="action">Наименование экшена</param>
    /// <param name="requestData">данные, необходимые серверу для обработки запроса, в формате JsonData</param>
    /// <param name="onSuccessCallback">Метод, которому будет передан ответ от срвера в формате JsonData в случае успеха</param>
    /// <param name="onErrorCallback">Метод, которому будет передано сообщение об ошибке</param>
    public static void SendRequest(string action, JsonData requestData, System.Action<JsonData> onSuccessCallback, System.Action<string> onErrorCallback, bool dummyLoad = true)
    {
        inst.StartCoroutine(inst.CorRequest(action, requestData, dummyLoad, onSuccessCallback, false, onErrorCallback));
    }

    /// <summary>
    /// Отправка запроса серверу, с возвратом ответа об ошибке в попап
    /// </summary>
    /// <param name="action">Наименование экшена</param>
    /// <param name="requestData">данные, необходимые серверу для обработки запроса, в формате JsonData</param>
    /// <param name="onSuccessCallback">Метод, которому будет передан ответ от срвера в формате JsonData в случае успеха</param>
    public static void SendRequest(string action, JsonData requestData, System.Action<JsonData> onSuccessCallback, bool dummyLoad = true)
    {
        inst.StartCoroutine(inst.CorRequest(action, requestData, dummyLoad, onSuccessCallback, true, null));
    }

    IEnumerator CorRequest(string action, JsonData requestData, bool dummyLoad, System.Action<JsonData> onSuccessCallback, bool defaultOnError, System.Action<string> onErrorCallback)
    {
        if (defaultOnError) onErrorCallback = DefaultOnError;

        // Create a Web Form
        WWWForm form = new WWWForm();
        form.AddField("action", action);
        form.AddField("token", token);

        string rData = requestData != null ? requestData.ToJson() : "";
        form.AddField("data", rData);
        //form.AddBinaryData("fileUpload", bytes, "screenShot.png", "image/png");

        print(rData);
        WWW www = new WWW(url, form);

        if (dummyLoad)
        {
            DummyLoad.inst.isLoading = true;
           // DummyLoad.inst.Loop();
        }

        print("API: request sended");

        yield return www;

        print("API: answer received");

        if (dummyLoad) DummyLoad.inst.isLoading = false;

        if (string.IsNullOrEmpty(www.error))
        {
            print(www.text);

            JsonData data = JsonMapper.ToObject(www.text);
            // ghjdthbnm json на валидность
            print(data.ToJson());
            //если валиден, проверить на ошибку
            if (data.Keys.Contains("error"))
            {
                print("fdfdfd");
                if (onErrorCallback != null) onErrorCallback((string)data["error"]);
                yield return 0; // exit
            }

            // ошибок нет, отправить данные, но сначала проверю, есть ли ответ
            if (data.Keys.Contains("result"))
            {
                print(data["result"].ToJson());

                if (onSuccessCallback != null) onSuccessCallback(data["result"]);


                // если запрос был авторизация, то обновить токен из ответа
                if (requestData != null && action == "login")
                {
                    token = (string)data["result"]["token"];
                    user_id = (int)data["result"]["user_id"];
                }
            }

        }
        else
        {
            print("error! - " + www.error + " - " + www.text);
            if (onErrorCallback != null) onErrorCallback(www.error);
        }
    }

    void DefaultOnError(string errorMessage)
    {
        print(errorMessage);
        Message.instance.MessageInfo(errorMessage, 2);
    }


    public static void Disconnect()
    {
        inst.StopAllCoroutines();
        print("API: Disconnected");
    }
}
