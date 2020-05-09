using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Use unity Lerps instead
 */
public class Tweener
{
    public enum TweenType
    {
        LinearTween,
        EaseInQuad,
        EaseOutQuad,
        EaseInOutQuad,
        EaseInQuint,
        EaseOutQuint,
        EaseInOutQuint,
        EaseInElastic,
        OutInQuartic
    }
    public static Vector2 Tween(TweenType type, float curTime, Vector2 beginPos, Vector2 endPos, float duration)
    {
        Vector2 result = Vector2.one;

        switch (type)
        {
            case TweenType.EaseInQuad:
                result = EaseInQuad(curTime, beginPos, endPos, duration);
                break;
            case TweenType.EaseOutQuad:
                result = EaseOutQuad(curTime, beginPos, endPos, duration);
                break;
            case TweenType.EaseInQuint:
                result = EaseInQuint(curTime, beginPos, endPos, duration);
                break;
            case TweenType.EaseOutQuint:
                result = EaseOutQuint(curTime, beginPos, endPos, duration);
                break;
            case TweenType.EaseInOutQuint:
                result = EaseInOutQuint(curTime, beginPos, endPos, duration);
                break;
            case TweenType.EaseInElastic:
                result = EaseInElasticSmall(curTime, beginPos, endPos, duration);
                break;
            case TweenType.OutInQuartic:
                result = OutInQuartic(curTime, beginPos, endPos, duration);
                break;
            default:
                result = EaseInQuad(curTime, beginPos, endPos, duration);
                break;
        }

        return result;
    }
    public static float Tween(TweenType type, float curTime, float beginingValue, float changeInValue, float duration)
    {
        float result;
        switch (type)
        {
            case TweenType.LinearTween:
                result = LinearTween(curTime, beginingValue, changeInValue, duration);
                break;
            case TweenType.EaseInQuad:
                result = EaseInQuad(curTime, beginingValue, changeInValue, duration);
                break;
            case TweenType.EaseOutQuad:
                result = EaseOutQuad(curTime, beginingValue, changeInValue, duration);
                break;
            case TweenType.EaseInOutQuad:
                result = EaseInOutQuad(curTime, beginingValue, changeInValue, duration);
                break;
            default:
                result = LinearTween(curTime, beginingValue, changeInValue, duration);
                break;
        }
        return result;
    }
    public static float LinearTween(float curTime, float beginingValue, float changeInValue, float duration)
    {
        return changeInValue * curTime / duration + beginingValue;
    }

    public static Color TweenColor(TweenType type, float curTime, Color originColor, Color changeInValue, float duration)
    {
        Color result = originColor;
        result.b = Tween(type, curTime, originColor.b, changeInValue.b, duration);
        result.g = Tween(type, curTime, originColor.g, changeInValue.g, duration);
        result.r = Tween(type, curTime, originColor.r, changeInValue.r, duration);
        Debug.Log("Color: " + result);
        return result;
    }

    public static float EaseInQuad(float curTime, float beginingValue, float changeInValue, float duration)
    {
        return changeInValue * (curTime /= duration) * curTime + beginingValue;
    }

    public static float EaseOutQuad(float curTime, float beginingValue, float changeInValue, float duration)
    {

        return -changeInValue * (curTime /= duration) * (curTime - 2) + beginingValue;
    }
    public static float EaseInOutQuad(float curTime, float beginingValue, float changeInValue, float duration)
    {
        if ((curTime /= duration / 2) < 1) return changeInValue / 2 * curTime * curTime + beginingValue;
        return -changeInValue / 2 * ((--curTime) * (curTime - 2) - 1) + beginingValue;
    }
    public static float EaseInQuint(float curTime, float beginingValue, float changeInValue, float duration)
    {

        return changeInValue * Mathf.Pow(curTime / duration, 5) + beginingValue;
    }
    public static float EaseOutQuint(float curTime, float beginingValue, float changeInValue, float duration)
    {
        return changeInValue * (Mathf.Pow(curTime / duration - 1, 5) + 1) + beginingValue;
    }
    public static float EaseInOutQuint(float curTime, float beginingValue, float changeInValue, float duration)
    {
        var temp = curTime / duration;
        if (temp / 2 < 1)
        {
            return changeInValue / 2 * Mathf.Pow(curTime, 4) + beginingValue;
        }
        return -changeInValue / 2 * (Mathf.Pow(curTime - 2, 4) - 2) + beginingValue;
    }
    public static float EaseInElasticSmall(float curTime, float beginingValue, float changeInValue, float duration)
    {
        var ts = (curTime /= duration) * curTime;
        var tc = ts * curTime;
        return beginingValue + changeInValue * (33 * tc * ts + -59 * ts * ts + 32 * tc + -5 * ts);
    }
    public static float OutInQuartic(float curTime, float beginingValue, float changeInValue, float duration)
    {
        var ts = (curTime /= duration) * curTime;
        var tc = ts * curTime;
        return beginingValue + changeInValue * (6 * tc + -9 * ts + 4 * curTime);
    }
    public static Vector2 EaseInQuad(float curTime, Vector2 beginPos, Vector2 endPos, float duration)
    {
        Vector2 result = Vector2.one;
        result.x = EaseInQuad(curTime, beginPos.x, endPos.x - beginPos.x, duration);
        result.y = EaseInQuad(curTime, beginPos.y, endPos.y - beginPos.y, duration);
        return result;
    }
    public static Vector2 EaseInQuint(float curTime, Vector2 beginPos, Vector2 endPos, float duration)
    {
        Vector2 result = Vector2.one;
        result.x = EaseInQuint(curTime, beginPos.x, endPos.x - beginPos.x, duration);
        result.y = EaseInQuint(curTime, beginPos.y, endPos.y - beginPos.y, duration);
        return result;
    }
    public static Vector2 EaseOutQuint(float curTime, Vector2 beginPos, Vector2 endPos, float duration)
    {
        Vector2 result = Vector2.one;
        result.x = EaseOutQuint(curTime, beginPos.x, endPos.x - beginPos.x, duration);
        result.y = EaseOutQuint(curTime, beginPos.y, endPos.y - beginPos.y, duration);
        return result;
    }
    public static Vector2 EaseInOutQuint(float curTime, Vector2 beginPos, Vector2 endPos, float duration)
    {
        Vector2 result = Vector2.one;
        result.x = EaseInOutQuint(curTime, beginPos.x, endPos.x - beginPos.x, duration);
        result.y = EaseInOutQuint(curTime, beginPos.y, endPos.y - beginPos.y, duration);
        return result;
    }
    public static Vector2 EaseOutQuad(float curTime, Vector2 beginPos, Vector2 endPos, float duration)
    {
        Vector2 result = Vector2.one;
        result.x = EaseOutQuad(curTime, beginPos.x, endPos.x - beginPos.x, duration);
        result.y = EaseOutQuad(curTime, beginPos.y, endPos.y - beginPos.y, duration);
        return result;
    }
    public static Vector2 EaseInElasticSmall(float curTime, Vector2 beginPos, Vector2 endPos, float duration)
    {
        Vector2 result = Vector2.one;
        result.x = EaseInElasticSmall(curTime, beginPos.x, endPos.x - beginPos.x, duration);
        result.y = EaseInElasticSmall(curTime, beginPos.y, endPos.y - beginPos.y, duration);
        return result;
    }
    public static Vector2 OutInQuartic(float curTime, Vector2 beginPos, Vector2 endPos, float duration)
    {
        Vector2 result = Vector2.one;
        result.x = OutInQuartic(curTime, beginPos.x, endPos.x - beginPos.x, duration);
        result.y = OutInQuartic(curTime, beginPos.y, endPos.y - beginPos.y, duration);
        return result;
    }
}