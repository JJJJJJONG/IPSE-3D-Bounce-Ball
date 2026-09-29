using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectBounce : MonoBehaviour
{
    public float rotatespeed = 10;
    public float amplitude = 1;
    
    void Update()
    {
        //회전
        transform.Rotate(new Vector3(0,10,0) * rotatespeed * Time.deltaTime, Space.Self);

        // 위아래로 움직임
        Vector3 position = transform.position;
        position.y += Mathf.Sin(Time.time*3) * (float)0.001 *amplitude;
        transform.position = position;
    }

  

}
