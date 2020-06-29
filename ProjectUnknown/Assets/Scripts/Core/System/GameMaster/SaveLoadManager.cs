using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public static class SaveLoadManager
{
    public static void SaveAllData()
    {
        LogHelper.GetInstance().Log("Saving datas from json".Bolden(), true);

        foreach (var obj in Resources.LoadAll("Data", typeof(ScriptableObject)))
        {
            SaveLoadManager.Save<object>(obj, obj.name);
        }
    }
    public static void LoadAllData()
    {
        LogHelper.GetInstance().Log("Loading datas from json".Bolden(), true);
        foreach (var obj in Resources.LoadAll("Data", typeof(ScriptableObject)))
        {
            SaveLoadManager.Load<object>(obj, obj.name);
        }
    }

    public static void Save<T>(T savedObject, string fileName)
    {
        LogHelper.GetInstance().Log("Saved Obj: ".Bolden() + fileName.Bolden(), true);
        var dataPath = "";
#if UNITY_STANDALONE_OSX
        dataPath = Application.dataPath + "/Resources/Data/StreamingAssets/SavedData/" + fileName + ".json";
#else
        dataPath = Application.dataPath + "/StreamingAssets/SavedData/" + fileName + ".json";
#endif
        string jsonSaved = JsonUtility.ToJson(savedObject);
        File.WriteAllText(dataPath, jsonSaved);
    }

    public static void Load<T>(T objectToLoad, string fileName)
    {
        LogHelper.GetInstance().Log("Loaded Obj: ".Bolden() + fileName.Bolden(), true);
        var dataPath = "";

#if UNITY_STANDALONE_OSX
        dataPath = Application.dataPath + "/Resources/Data/StreamingAssets/SavedData/" + fileName + ".json";
#else
        dataPath = Application.dataPath + "/StreamingAssets/SavedData/" + fileName + ".json";
#endif
        try
        {

            string jsonLoad = File.ReadAllText(dataPath);
            JsonUtility.FromJsonOverwrite(jsonLoad, objectToLoad);
        }
        catch (FileNotFoundException e)
        {
            Save(objectToLoad, fileName);
        }
    }

    public static void ResetSaves()
    {
        LogHelper.GetInstance().Log("Resetting Saves".Bolden(), true);
        foreach (var obj in Resources.LoadAll("Data", typeof(ISaveRestable)))
        {
            ((ISaveRestable)obj).ResetSave();
            SaveLoadManager.Save<object>(obj, obj.name);
        }
    }
}
