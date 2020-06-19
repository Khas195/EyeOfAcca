using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class CameraScaleWithScreenSize : MonoBehaviour
{
    [SerializeField]
    Camera targetCamera = null;
    [SerializeField]
    float screenWidth = 10;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        AdjustCameraBasedOnResolution();
    }

    private void AdjustCameraBasedOnResolution()
    {
        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = 16.0f / 9.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = targetCamera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            targetCamera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = targetCamera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            targetCamera.rect = rect;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.AdjustCameraBasedOnResolution();
        float unitsPerpixel = screenWidth / Screen.width;
        float desiredHalfHeight = 0.5f * unitsPerpixel * Screen.height;
        targetCamera.orthographicSize = desiredHalfHeight;
    }
}
