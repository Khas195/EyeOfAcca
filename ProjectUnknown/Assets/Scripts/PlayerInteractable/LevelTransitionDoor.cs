using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class LevelTransitionDoor : MonoBehaviour
{
    [SerializeField]
    [Required]
    TransitionDoorProfile profile = null;

#if UNITY_EDITOR
    [Button]
    public void CreateProfile()
    {
        var newProfile = ScriptableObject.CreateInstance<TransitionDoorProfile>();
        var sceneName = this.gameObject.scene.name;
        System.IO.Directory.CreateDirectory("Assets/Resources/Datas/LevelDoor/" + sceneName);
        UnityEditor.AssetDatabase.CreateAsset(newProfile, "Assets/Resources/Datas/LevelDoor/" + sceneName + "/" + sceneName + "-" + this.gameObject.name + ".asset");
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.EditorUtility.FocusProjectWindow();
        newProfile.doorHome = this.gameObject.scene.name;
        newProfile.doorLocation = this.transform.position;
        this.profile = newProfile;

    }
#endif
    public bool IsUsuable()
    {
        return profile.landingPlace != null;
    }

    public TransitionDoorProfile GetLandingLocation()
    {
        return profile.landingPlace;
    }


}
