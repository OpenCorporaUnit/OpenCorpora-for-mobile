using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using LitJson;

public class ScreenDoTask : MonoBehaviour {
   // public static 

    public Sprite ontap;
    public Sprite offtap;
    public GameObject circlebtn;
    public GameObject clickoncheck;
    public GameObject levelgrid;
    public GameObject btgrid;
    public GameObject[] circletask;
    public GameObject[] btntask;

    public Text stringtask;

    GameObject pref;

    bool shorttext = true;

    [System.Serializable]
    class TaskClass
    {
        public string id;               // для отправки серверу
        public int main_word;           // ключевое слово в контексте, его надо выделить
        public string full_text;        // полныйтекст
        public List<string> lst_Short_Text;    // массив слов в контексте, главное слово среди них
        public int answer = -1;         // помнит, какой ответ дали на этот вопрос
        public TaskClass(string id, int main_word, string full_text, List<string> lst_Short_Text)
        {
            this.id = id;
            this.main_word = main_word;
            this.full_text = full_text;
            this.lst_Short_Text = lst_Short_Text;
        }
    }

    public static ScreenDoTask inst;

    List<string> lstButtonAnswer;

    List<TaskClass> lstTask;

    int currentIndex = 0;

    void Awake () {
        inst = this;
        print("INIT!");
    }
	

    public void SetData(JsonData data)
    {
        // сгенерить кнопки вариантов ответов (ответы считать от 1 !!)
        lstButtonAnswer = new List<string>();

        //print(data["gram_descr"].Count);
        for (int i = 0; i < data["gram_descr"].Count; i++)
            lstButtonAnswer.Add((string)data["gram_descr"][i]);

        // теперь заполняю сами задания
        lstTask = new List<TaskClass>();

        for (int i = 0; i < data["instances"].Count; i++)
        {
            List<string> lstShortText = new List<string>();

            for (int a = 0; a < data["instances"][i]["context"].Count; a++)
                lstShortText.Add((string)data["instances"][i]["context"][a]);


                lstTask.Add(new TaskClass((string)data["instances"][i]["id"],
                                        (int)data["instances"][i]["mainword"],
                                        (string)data["instances"][i]["sentence_text"],
                                        lstShortText));
        }

        // показать всё
        CheckBtn();
        InstBtn();
        Profile.inst.OpenPole(3);
    }

    void CheckBtn() {
        for (int i = 0; i < btntask.Length; i++)
            Destroy(btntask[i]);

        btntask = new GameObject[lstButtonAnswer.Count + 1];
        for (int i = 0; i < lstButtonAnswer.Count; i++)
        {
            int s = i;
            btntask[i] = Instantiate(clickoncheck) as GameObject;
            btntask[i].transform.SetParent(btgrid.transform, false);
            btntask[i].transform.GetChild(0).GetComponent<Text>().text = lstButtonAnswer[i];
            btntask[i].GetComponent<Button>().onClick.AddListener(delegate { OnBtnClock(s); });
        }

        btntask[lstButtonAnswer.Count] = Instantiate(clickoncheck) as GameObject;
        btntask[lstButtonAnswer.Count].transform.SetParent(btgrid.transform, false);
        btntask[lstButtonAnswer.Count].transform.GetChild(0).GetComponent<Text>().text = "Другое";
        btntask[lstButtonAnswer.Count].GetComponent<Button>().onClick.AddListener(delegate { OnBtnClock(98); });
    }

    void InstBtn() {
        for (int i = 0; i < circletask.Length; i++)
            Destroy(circletask[i]);

        circletask = new GameObject[lstTask.Count];
       
        for (int i = 0; i < lstTask.Count; i++) {
            int s = i;
            circletask[i] = Instantiate(circlebtn) as GameObject;
            circletask[i].transform.SetParent(levelgrid.transform, false);
            circletask[i].GetComponent<Button>().onClick.AddListener(delegate { OnClock(s); });
        }
        OnClock(0);
    }

    public void ShortLongTxt() {
        shorttext = !shorttext;
        ShowText();
    }

    void ShowText()
    {
        if (shorttext)
        {
            string st = "";
            for (int i = 0; i < lstTask[currentIndex].lst_Short_Text.Count; i++)
                if (i == lstTask[currentIndex].main_word)
                    st += " <b><i><size=" + (stringtask.fontSize + 5) + "><color=red>" + lstTask[currentIndex].lst_Short_Text[i] + "</color></size></i></b>";
                else
                    st += " " + lstTask[currentIndex].lst_Short_Text[i];

            stringtask.text = st;
        }
        else
            stringtask.text = lstTask[currentIndex].full_text;
    }


    public void OnClock(int n) {
        currentIndex = n;

        for (int i = 0; i < circletask.Length; i++)
            if (n == i)
            {
                ShowText();
                ColorizeAnswerButton();
                circletask[i].GetComponent<Image>().sprite = ontap;
            }
            else {
                circletask[i].GetComponent<Image>().sprite = offtap;
            }
    }

    void ColorizeAnswerButton()
    {
        int n = lstTask[currentIndex].answer;
        foreach (var b in btntask)
            b.GetComponent<Image>().color = Color.white;

        if (n > -1)
        {
            if (n == 98) n = btntask.Length - 1;
            btntask[n].GetComponent<Image>().color = Color.green;
        }
    }

    public void OnBtnClock(int n) {
        lstTask[currentIndex].answer = n;
        ColorizeAnswerButton();

        //  {"user_id" : "3678", "answers": [ ["2449933", "1"], ["2450187", "1"] ]}

        string json = @"
          {
              ""user_id""   : """ + API.user_id + @""",
              ""answers""   : [ [
                    """ + lstTask[currentIndex].id + @""",
                    """ + (lstTask[currentIndex].answer + 1) + @"""
             ] ]
          }
        ";

        // get_morph_task('pool_id', 'size', 'timeout')

        API.SendRequest("save_morph_task", JsonMapper.ToObject(json), null, false);
    }
}
