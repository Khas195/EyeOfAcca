using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class SaveLoadManagerEditor
{
    [MenuItem("SaveLoad/SaveAll")]
    public static void SaveAllData()
    {
        SaveLoadManager.SaveAllData();
    }
    [MenuItem("SaveLoad/LoadAll")]
    public static void LoadAllData()
    {
        SaveLoadManager.LoadAllData();
    }
    [MenuItem("SaveLoad/NewSave")]
    public static void NewSave()
    {
        SaveLoadManager.ResetSaves();
    }
}
