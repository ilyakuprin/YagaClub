using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject SecondSprite;
    public float Speed;

    private float startPos, length;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        var posX = transform.position.x - Time.deltaTime * Speed;
        if (posX < startPos - length) posX += 2 * length;
        transform.position = new Vector3(posX, transform.position.y, transform.position.z);

        var posXsecond = SecondSprite.transform.position.x - Time.deltaTime * Speed;
        if (posXsecond < startPos - length) posXsecond += 2 * length;
        SecondSprite.transform.position = new Vector3(posXsecond, transform.position.y, transform.position.z);
    }
}
