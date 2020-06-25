using UnityEngine;

[CreateAssetMenu(fileName = "Teleport", menuName = "AxeBehavior/Teleport", order = 1)]
public class TeleportAbility : AxeAbility
{
    [SerializeField]
    BoomeraxeParams datas = null;

    [SerializeField]
    private Color gemColor = Color.white;
    public override void Activate(Boomeraxe axe)
    {

        LogHelper.GetInstance().Log(("TELEPORT!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
        /// teleport 
        var pos = axe.GetAxePosition();
        Vector3 teleportPlace = pos + axe.GetContactPointNormal() * datas.teleportDistanceAwayFromDestination;

        VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.Teleport, axe.GetHolderBodyPosition(), Quaternion.identity);
        VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.Teleport, teleportPlace, Quaternion.identity);
        SFXSystem.GetInstance().PlaySound(SFXResources.SFXList.Teleport);

        teleportPlace.z = axe.GetHolderBodyPosition().z;

        axe.SetHolderPosition(teleportPlace);


        // Freeze for seconds


        // Grab the axe
        axe.GetGrip().SetAxeCatchable(true);
        axe.GetGrip().HoldAxe();
        axe.GetGrip().GetTimeAdjustor().SetGravityScaleFor(datas.timeScaleAfterTeleport, datas.lulTimeAfterTeleport);
    }


    public override string GetAbilityPower()
    {
        return "hasTeleportPower";
    }

    public override Color GetGemColor()
    {
        return this.gemColor;
    }
}