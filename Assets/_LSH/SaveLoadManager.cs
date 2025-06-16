using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private string _filePath;
    private SaveData _saveData = new SaveData();

    private void Awake()
    {
        _filePath = Path.Combine(Application.persistentDataPath, "SaveData.json");
    }
    
    public void SetSaveData( PlayerStat playerStat, float _oxygen)
    {
        
        _saveData._playerStat = playerStat;
        _saveData._oxygen = _oxygen;
    }

    public SaveData GetSaveData()
    {
        return _saveData;
    }
    
    public void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_filePath, json);
        Debug.Log("저장 완료: " + _filePath);
    }

    public SaveData Load()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            //Debug.Log("불러오기 완료 / " + data._oxygen);
            return data;
        }

        //Debug.LogWarning("저장 파일이 없습니다.");
        return null;
    }
}
