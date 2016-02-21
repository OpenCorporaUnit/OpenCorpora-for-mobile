using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;

public class PoleTask : MonoBehaviour {
    // Use this for initialization
    public GameObject prefabTask;
    public Sprite[] mascomplex;

    public GameObject Content;

    GameObject inst;

	void Start () {
        //ParseJsonApiTask();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        // удаление дочерних объектов и проверить на новые задачи
        for (int i = 0; i < Content.transform.childCount; i++)
            Destroy(Content.transform.GetChild(i).gameObject);


        string json = @"
          {
              ""user_id""   : """ + API.user_id + @"""
          }
        ";


        API.SendRequest("get_available_morph_tasks", JsonMapper.ToObject(json), ParseJsonApiTask);
    }

    void ParseJsonApiTask(JsonData data)
    {

        for (int i = 0; i < data.Count; i++)
            // if (data[i].Keys.Contains("pools")) 
            InstTaskAPI(data[i]["complexity"].IsString ? (string)data[i]["complexity"] : ((int)data[i]["complexity"]).ToString(), (string)data[i]["name"], (string)data[i]["random_id"]);
    }

    public void InstTaskAPI(string complexity, string taskname, string rand) {
        inst = Instantiate(prefabTask) as GameObject;
        inst.transform.SetParent(Content.transform, false);
        inst.GetComponent<Task>().stringtask.text = taskname;
        inst.GetComponent<Task>().taskId = rand;
        inst.GetComponent<Task>().complexity.sprite = mascomplex[int.Parse(complexity)];
    }
}
