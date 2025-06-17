using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// potData = GameManager.Instance.GetSaveData().potData;  //데이터 로드
//GameManager.Instance.saveLoadManager.SetSaveData("PotData", potData);  //데이터 저장

public class SaveLoadManager : MonoBehaviour
{
    private string _filePath;
    private SaveData _saveData = new SaveData();  //데이터 저장용 클래스

    private void OnEnable()
    {
        _filePath = Path.Combine(Application.persistentDataPath, "SaveData.json");
    }

    private void Start()
    {
        _saveData = GameManager.Instance.GetSaveData();
    }

    public void SetSaveData<T>( string name, T value)  //데이터 저장용 클래스에 데이터를 넣음
    {
        switch (name)
        {
            case "Oxygen":
                if (value is int oxygenValue)
                {
                    _saveData.oxygen = oxygenValue;
                }
                else
                {
                    Debug.LogWarning("Invalid type");
                }
                break;
            case "PlayerStat":
                if (value is PlayerStat playerStatValue)
                {
                    _saveData.playerStat = playerStatValue; 
                }
                else
                {
                    Debug.LogWarning("Invalid type");
                }
                break;
            case "PotData":
                if (value is PotData PotDataValue)
                {
                    _saveData.potData = PotDataValue; 
                }
                else
                {
                    Debug.LogWarning("Invalid type");
                }
                break;
            case "Plant":
                if (value is PlantData PlantValue)
                {
                    _saveData.plant = PlantValue; 
                }
                else
                {
                    Debug.LogWarning("Invalid type");
                }
                break;
            case "CurGrow":
                if (value is float CurGrowValue)
                {
                    _saveData.curGrow = CurGrowValue; 
                }
                else
                {
                    Debug.LogWarning("Invalid type");
                }
                break;
            case "GrowthStage":
                if (value is int GrowthStageValue)
                {
                    _saveData.growthStage = GrowthStageValue; 
                }
                else
                {
                    Debug.LogWarning("Invalid type");
                }
                break;
            case "PlantData":
                if (value is List<PlantData> PlantDataValue)
                {
                    _saveData.plantData = PlantDataValue; 
                }
                else
                {
                    Debug.LogWarning("Invalid type");
                }
                break;
            case "PlantDatas":
                if (value is PlantData[] PlantDatasValue)
                {
                    _saveData.plantDatas = PlantDatasValue; 
                }
                else
                {
                    Debug.LogWarning("Invalid type");
                }
                break;
            case "PotInstance":
                if (value is List<PotInstance> PotInstanceValue)
                {
                    _saveData.potInventory = PotInstanceValue; 
                }
                else
                {
                    Debug.LogWarning("Invalid type");
                }
                break;
            case "SurviveDays":
                if (value is int SurviveDaysValue)
                {
                    _saveData.surviveDays = SurviveDaysValue; 
                }
                else
                {
                    Debug.LogWarning("Invalid type");
                }
                break;
        }
        Save(_saveData);
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
            return _saveData;
        }
        _saveData = new SaveData();  // 빈 데이터라도 초기화
        return _saveData;
    }
    
    public void DeleteSaveData()  //데이터 삭제
    {
        if (File.Exists(_filePath))
        {
            File.Delete(_filePath);
            Debug.Log("저장 데이터 삭제 완료: " + _filePath);
        }
        else
        {
            Debug.LogWarning("삭제할 저장 파일이 없습니다.");
        }
    }
}
