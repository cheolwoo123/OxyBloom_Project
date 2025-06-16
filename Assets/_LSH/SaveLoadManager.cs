using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private string filePath;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "SaveData.json");
    }

    public void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        Debug.Log("저장 완료: " + filePath);
    }

    public SaveData Load()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("불러오기 완료");
            return data;
        }

        Debug.LogWarning("저장 파일이 없습니다.");
        return null;
    }
}
