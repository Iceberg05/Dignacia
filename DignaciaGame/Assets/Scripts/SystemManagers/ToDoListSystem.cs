using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToDoListSystem : MonoBehaviour
{
    [SerializeField] GameObject taskUIObject;
    [SerializeField] TMP_Text taskNameText;
    [SerializeField] TMP_Text taskDescriptionText;

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

    void Start()
    {
        
    }
    void Update()
    {
        for(int i = 0; i <= allTasks.Count; i++)
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
}
