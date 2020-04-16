using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConditionsCheckers", menuName = "BehaviorCondition/ConditionsChecker")]
public class ConditionsChecker : ScriptableObject
{
    [SerializeField]
    List<CharacterBehaviorCondition> conditions = new List<CharacterBehaviorCondition>();
    [SerializeField]
    bool needAllToSastisfied = false;

    public bool IsSatisfied(Character character)
    {
        if (needAllToSastisfied)
        {
            return AllIsSatisfied(character);
        }
        else
        {
            return AtleastOneIsSatisfied(character);
        }
    }

    private bool AllIsSatisfied(Character character)
    {
        for (int i = 0; i < conditions.Count; i++)
        {
            if (conditions[i].IsSatisfied(character) == false)
            {
                return false;
            }
        }
        return true;
    }

    private bool AtleastOneIsSatisfied(Character character)
    {
        for (int i = 0; i < conditions.Count; i++)
        {
            if (conditions[i].IsSatisfied(character) == true)
            {
                return true;
            }
        }
        return false;
    }
}