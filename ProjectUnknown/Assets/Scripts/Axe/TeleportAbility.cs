using UnityEngine;

[CreateAssetMenu(fileName = "Teleport", menuName = "AxeBehavior/Teleport", order = 1)]
public class TeleportAbility : AxeAbility
{
    [SerializeField]
    BoomeraxeParams datas;
    public override void Activate(Boomeraxe axe)
    {
        base.Activate(axe);

        LogHelper.GetInstance().Log(("TELEPORT!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
        /// teleport 
        var pos = axe.GetAxePosition();
        Vector3 teleportPlace = pos + axe.GetContactPointNormal() * datas.teleportDistanceAwayFromDestination;
        VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.Teleport, axe.GetHolderBodyPosition(), Quaternion.identity);
        VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.Teleport, teleportPlace, Quaternion.identity);

        teleportPlace.z = axe.GetHolderBodyPosition().z;

        axe.SetHolderPosition(teleportPlace);


        axe.GetGrip().GetTimeAdjustor().SetGravityScaleFor(datas.timeScaleAfterTeleport, datas.lulTimeAfterTeleport);
        // Freeze for seconds


        // Grab the axe
        axe.GetGrip().SetAxeCatchable(true);
        axe.GetGrip().HoldAxe();
    }

}