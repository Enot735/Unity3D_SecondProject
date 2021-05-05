using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _speedTranslation = 10f;
    private float _speedRotation = 50f;

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Vertical");
        transform.Translate(0, 0, move * _speedTranslation * Time.deltaTime);

        float rotate = Input.GetAxis("Horizontal");
        transform.Rotate(0, rotate * move * _speedRotation * Time.deltaTime , 0);
    }
}
