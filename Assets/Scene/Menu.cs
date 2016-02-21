using UnityEngine;
using System.Collections;
using LitJson;

public class Menu : MonoBehaviour {

    public GameObject Avtoriz;
    public GameObject Regist;
    public Profile Prof;
    string login;
    string password;

    private static Menu _instance;

    public static Menu instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Menu>();
            }
            return _instance;
        }
    }
    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick(int Type) {
        Message.instance.MessageInfo("??", Type);
    }

    public void LoginInAPI(string login, string pass, bool check) {
        // осуществить проверку после того как произошла авторизация на сайте
        this.login = login;
        this.password = pass;
        string json = @"
          {
              ""login""   : """ + login + @""",
              ""password"" : """ + pass + @"""
          }
        ";


        API.SendRequest("login", JsonMapper.ToObject(json), AutorizationComplete);

    }

    void AutorizationComplete(JsonData data)
    {

        LeanTween.scale(Avtoriz, Vector3.zero, 0f).onComplete = delegate {


                    PlayerPrefs.SetString("Login", login);

                    PlayerPrefs.SetString("Password", password);

            Prof.gameObject.transform.localScale = Vector3.one;
            Prof.OpenMenu();
        };
        print("marker");
    }

    public void RegistrationInAPI(string login, string pass, bool licenzi, bool podpis) {

        // вызвать после того как произошла регистрация
        LeanTween.scale(Regist, Vector3.zero, 0f).onComplete = delegate {
            LeanTween.scale(Avtoriz, Vector3.one, 0f);
        };
    }
}
