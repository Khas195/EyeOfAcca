using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class LogTextItem
{
    public Text item;
    public float currentExistTime;
    public float currentFadeTime;
}
public class LogItemPool : ObjectPooling<LogTextItem>
{
    [SerializeField]
    [Required]
    InGameLogUI logUI = null;
    [SerializeField]
    [Required]
    GameObject logPrototype = null;
    [SerializeField]
    bool bottupUpOrder = false;


    protected override void ActivateOjbect(LogTextItem target)
    {
        target.item.gameObject.SetActive(true);
        target = ResetLogProperties(target);

        target.item.gameObject.transform.SetParent(logUI.transform);
        if (bottupUpOrder == false)
        {
            target.item.gameObject.transform.SetAsFirstSibling();
        }
    }

    protected override LogTextItem CreateInstance()
    {
        var go = GameObject.Instantiate(logPrototype, Vector3.zero, Quaternion.identity);
        LogTextItem result = CreateLogItem(go);

        return result;
    }

    private static LogTextItem CreateLogItem(GameObject go)
    {
        LogTextItem newLog = new LogTextItem();
        newLog.item = go.GetComponent<Text>();
        newLog = ResetLogProperties(newLog);
        return newLog;
    }
    private static LogTextItem ResetLogProperties(LogTextItem target)
    {
        target.currentExistTime = 0;
        target.currentFadeTime = 0;
        var itemColor = target.item.color;
        itemColor.a = 1f;
        target.item.color = itemColor;
        return target;
    }

    protected override void DeactivateObject(LogTextItem target)
    {
        target.item.gameObject.SetActive(false);
        target.item.gameObject.transform.SetParent(this.transform);
    }
}