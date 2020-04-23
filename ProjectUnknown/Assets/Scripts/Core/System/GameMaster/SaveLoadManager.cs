using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public static class SaveLoadManager
{
    public static void SaveAllData()
    {
        LogHelper.GetInstance().Log("Saving datas from json".Bolden(), true);
        foreach (var obj in Resources.FindObjectsOfTypeAll(typeof(ScriptableObject)) as ScriptableObject[])
        {
            var savedObj = Resources.Load("Datas/" + obj.name);
            if (savedObj != null)
            {
                SaveLoadManager.Save<object>(obj, obj.name);
            }
        }
    }
    public static void LoadAllData()
    {
        LogHelper.GetInstance().Log("Loading datas from json".Bolden(), true);
        foreach (var obj in Resources.FindObjectsOfTypeAll(typeof(ScriptableObject)) as ScriptableObject[])
        {
            var loadedObj = Resources.Load("Datas/" + obj.name);
            if (loadedObj != null)
            {
                SaveLoadManager.Load<object>(obj, obj.name);
            }
        }
    }

    public static void Save<T>(T savedObject, string fileName)
    {
        LogHelper.GetInstance().Log("Saved Obj: ".Bolden() + fileName.Bolden(), true);
        string jsonSaved = JsonUtility.ToJson(savedObject);
        File.WriteAllText(Application.dataPath + "/StreamingAssets/SavedData/" + fileName + ".json", jsonSaved);
    }
    public static void Load<T>(T objectToLoad, string fileName)
    {
        LogHelper.GetInstance().Log("Loaded Obj: ".Bolden() + fileName.Bolden(), true);
        string jsonLoad = File.ReadAllText(Application.dataPath + "/StreamingAssets/SavedData/" + fileName + ".json");
        JsonUtility.FromJsonOverwrite(jsonLoad, objectToLoad);
    }
}
