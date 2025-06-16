using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private string _filePath;
    private SaveData _saveData = new SaveData();  //데이터 저장용 클래스

    private void Awake()
    {
        _filePath = Path.Combine(Application.persistentDataPath, "SaveData.json");
    }
    
    public void SetSaveData( PlayerStat playerStat, float _oxygen)  //데이터 저장용 클래스에 데이터를 넣음
    {
        _saveData._playerStat = playerStat;
        _saveData._oxygen = _oxygen;
    }

    public SaveData GetSaveData()  //데이터 리턴
    {
        return _saveData;
    }
    
    public void Save(SaveData data)  //데이터를 json으로 저장
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_filePath, json);
        Debug.Log("저장 완료: " + _filePath);
    }

    public SaveData Load()  //데이터 로드후 리턴
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            _saveData = data;
            //Debug.Log("불러오기 완료 / " + _saveData._oxygen);
            return data;
        }

        //Debug.LogWarning("저장 파일이 없습니다.");
        return null;
    }
}
