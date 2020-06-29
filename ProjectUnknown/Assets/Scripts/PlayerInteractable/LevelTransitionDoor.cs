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
    Transform cachedTranformed = null;

    void Start()
    {
        UpdateProfilePosition();
        profile.SaveData();
    }
    [Button]
    public void UpdateProfilePosition()
    {
        LogHelper.GetInstance().Log("Update Position of ".Bolden() + this.profile.ToString().Bolden().Colorize(Color.green), true);
        if (cachedTranformed == null)
        {
            cachedTranformed = this.GetComponent<Transform>();
        }
        profile.doorLocation = cachedTranformed.position;
    }
#if UNITY_EDITOR


    [Button]
    public void CreateProfile()
    {
        var newProfile = ScriptableObject.CreateInstance<TransitionDoorProfile>();
        var sceneName = this.gameObject.scene.name;
        System.IO.Directory.CreateDirectory("Assets/Resources/GameSettings/LevelDoor/" + sceneName);
        UnityEditor.AssetDatabase.CreateAsset(newProfile, "Assets/Resources/GameSettings/LevelDoor/" + sceneName + "/" + sceneName + "-" + this.gameObject.name + ".asset");
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.EditorUtility.FocusProjectWindow();
        newProfile.doorHome = this.gameObject.scene.name;
        newProfile.doorLocation = this.transform.position;
        this.profile = newProfile;
        UnityEditor.EditorUtility.SetDirty(this.profile);

    }


#endif

    public TransitionDoorProfile GetProfile()
    {
        return profile;
    }
    public bool IsUsuable()
    {
        return profile.landingPlace != null;
    }

    public TransitionDoorProfile GetLandingLocation()
    {
        return profile.landingPlace;
    }


}
