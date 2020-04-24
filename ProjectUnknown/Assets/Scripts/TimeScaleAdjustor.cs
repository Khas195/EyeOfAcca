using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class TimeScaleAdjustor : MonoBehaviour
{
    [SerializeField]
    [Required]
    Rigidbody2D body = null;
    bool first = true;

    [SerializeField]
    private float _timeScale = 1;
    public float timeScale
    {
        get { return _timeScale; }
        set
        {
            if (!first)
            {
                body.mass *= timeScale;
                body.velocity /= timeScale;
                body.angularVelocity /= timeScale;
            }
            first = false;

            _timeScale = Mathf.Abs(value);

            body.mass /= timeScale;
            body.velocity *= timeScale;
            body.angularVelocity *= timeScale;
        }
    }

    void Awake()
    {
        timeScale = _timeScale;
    }


    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime * timeScale;
        body.velocity += Physics2D.gravity * dt;
    }

}
