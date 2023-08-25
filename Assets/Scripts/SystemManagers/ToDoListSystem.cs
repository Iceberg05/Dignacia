using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToDoListSystem : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject taskUIObject;

    [SerializeField] float betweenObjectsValue;

    [System.Serializable]
    public class ToDoListTask
    {
        [Header("Main Informations")]
        [Space]

        public string taskName;
        public string description;

        [Header("Tasks With Processes")]
        [Space]

        public bool isContinuous;
        public float progress;
        public float completeValue;

        [Header("After the Task Part")]
        [Space]

        public bool isItDone;
        public string steamAchievementName;
    }
    public List<ToDoListTask> allTasks = new List<ToDoListTask>();

    void Awake()
    {
        panel.SetActive(false);
    }
    void Update()
    {
        for(int i = 0; i < allTasks.Count; i++)
        {
            if(allTasks[i].isContinuous && allTasks[i].progress >= allTasks[i].completeValue && !allTasks[i].isItDone)
            {
                FinishTask(i);
                break;
            }
        }
    }
    public void FinishTask(int taskIndex)
    {
        allTasks[taskIndex].isItDone = true;
    }
    public void OpenUI(bool mustOpen)
    {
        if(mustOpen)
        {
            panel.SetActive(true);
            float heightOfTasks = panel.GetComponent<RectTransform>().rect.height - 10f;
            for (int i = 0; i < allTasks.Count; i++)
            {
                GameObject taskObject = Instantiate(taskUIObject, panel.transform.Find("ScrollArea").transform.Find("Content"));
                taskObject.transform.position = new Vector3(taskObject.transform.position.x, heightOfTasks, taskObject.transform.position.z);
                taskObject.transform.Find("TitleText").GetComponent<TMP_Text>().text = allTasks[i].taskName;
                taskObject.transform.Find("DescriptionText").GetComponent<TMP_Text>().text = allTasks[i].description;
                if(allTasks[i].isContinuous) taskObject.transform.Find("ProgressText").GetComponent<TMP_Text>().text = allTasks[i].progress + " / " + allTasks[i].completeValue;
                else taskObject.transform.Find("ProgressText").GetComponent<TMP_Text>().text = "";
                heightOfTasks -= betweenObjectsValue;
            }
        } else
        {
            panel.SetActive(false);
            foreach(Transform child in panel.transform.Find("ScrollArea").transform.Find("Content"))
            {
                Destroy(child.gameObject);
            }
        }
    }
}
