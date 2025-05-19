using System;
using System.IO;
using UnityEngine;

namespace Quest_Studio
{
    public class SaveLoadHandler : MonoBehaviour
    {
        //Instance
        #region
        private static SaveLoadHandler instance;
        public static SaveLoadHandler GetInstance() { return instance; }
        #endregion

        // Variables
        #region
        [Header("Variables")]
        // - Save Path -
        #region
        private string folderName = "SaveLoadSystem/Data";
        private string GetFolderName() { return folderName; }
        #endregion

        // - Save File -
        #region
        private string fileName = "SaveData.json";
        private string GetFileName() { return fileName; }
        #endregion

        // - Save File Path -
        #region
        private string saveFilePath;
        private string GetSaveFilePath() { return saveFilePath; }
        private void SetSaveFilePath(string saveFilePath) { this.saveFilePath = saveFilePath; }
        #endregion

        // - Save Data -
        #region
        [Header("Save Data")]
        [SerializeField]
        private Data saveData;
        private Data GetSaveData() { return saveData; }
        private void SetSaveData(Data data) { this.saveData = data; }
        #endregion

        // - Load Data -
        #region
        [Header("Load Data")]
        [SerializeField]
        private Data loadData;
        public Data GetLoadData() { return loadData; }
        private void SetLoadData(Data data) { this.loadData = data; }
        #endregion

        #endregion

        // Method
        #region
        // - Check Save File Exsist -
        #region
        private bool CheckSaveFileExists()
        {
            string folderPath = Application.dataPath + "/" + GetFolderName();
            string filePath = folderPath + "/" + GetFileName();
            if (!Directory.Exists(folderPath))
            {
                Debug.Log(filePath + "\nFolder Not Exists!");
                Directory.CreateDirectory(folderPath);
            }
            if (File.Exists(filePath))
            {
                Debug.Log(filePath + "\nFile Exists!");
                return true;
            }
            Debug.Log(filePath + "\nFile Not Exists!");
            return false;
        }
        #endregion

        // - Create Save -
        #region
        private void SaveData(string filePath, string json)
        {
            if (CheckSaveFileExists())
            {
                Debug.Log("Data Over Write!");
            }
            else
            {
                Debug.Log("New Data Saved!");
            }
            File.WriteAllText(filePath, json);
        }
        #endregion

        // - Load Save -
        #region
        private void LoadData(string filePath)
        {
            if (CheckSaveFileExists())
            {
                string loadString = File.ReadAllText(filePath);
                Data loadData = JsonUtility.FromJson<Data>(loadString);
                Debug.Log("Data Loaded!");
                SetLoadData(loadData);
            }
        }
        #endregion

        #endregion

        // Data Class
        #region
        [Serializable]
        public class Data
        {
            public string folderPath;

            public Data(string filePath)
            {
                this.folderPath = filePath;
            }
        }
        #endregion

        private void Awake()
        {
            // Instance
            #region
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
            #endregion

            SetSaveFilePath(Application.dataPath + "/" + GetFolderName() + "/" + GetFileName());
            SetSaveData(new Data(GetSaveFilePath()));
            SaveData(GetSaveFilePath(), JsonUtility.ToJson(GetSaveData()));
            LoadData(GetSaveFilePath());
        }

        private void Start()
        {

        }

        private void Update()
        {

        }
    }
}