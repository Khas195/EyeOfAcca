using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDoor", menuName = "Data/LevelDoor", order = 1)]
public class TransitionDoorProfile : ScriptableObject
{
    [Scene]
    public string doorHome = "";

    [ReadOnly]
    public Vector3 doorLocation = Vector3.one;

    [OnValueChanged("LinkDoor")]
    public TransitionDoorProfile landingPlace = null;
    public void LinkDoor(TransitionDoorProfile oldValue, TransitionDoorProfile newValue)
    {
        if (newValue == null) return;
        LogHelper.GetInstance().Log("Linking Door".Bolden() + (this.name.Colorize(Color.green) + " and " + newValue.name.Colorize(Color.green)).Bolden(), false, LogHelper.LogLayer.Developer);
        if (newValue.landingPlace != null && newValue.landingPlace != this)
        {
            LogHelper.GetInstance().Log("Unlinking Door".Bolden() + (newValue.name.Colorize(Color.yellow) + " and " + newValue.landingPlace.name.Colorize(Color.yellow)).Bolden(), false, LogHelper.LogLayer.Developer);
            newValue.landingPlace.landingPlace = null;
        }
        newValue.landingPlace = this;
    }

}
