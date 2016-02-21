using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;

public class Task : MonoBehaviour {
    const int size = 10;            // сколько заданий подгрузить за раз

    public Text stringtask;
    public Image complexity;
    public Button openpoletasks;
    public string taskId;


    // стучит на сервер, просит пул задания, когда получает, переходим на поле/экран с заданием, которое уже будет сгенереноот ответа
    public void BtnGetTask_Click()
    {
        string json = @"
          {
              ""user_id""   : """ + API.user_id + @""",
              ""pool_id""   : """ + taskId + @""",
              ""size""   : """ + size + @"""
          }
        ";

        // get_morph_task('pool_id', 'size', 'timeout')

        JsonData d = JsonMapper.ToObject(json);

        print("marker 1");
        API.SendRequest("get_morph_task", d, Transport);
    }

    void Transport (JsonData d)
    {
        print("marker 0");
        ScreenDoTask.inst.SetData(d);
        print("marker 2");

    }
}
