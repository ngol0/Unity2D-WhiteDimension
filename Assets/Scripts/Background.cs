using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.5f;

    Material myMaterial;

    float offSet;

    //Vector2 offSet;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        //offSet = new Vector2(0, backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        offSet = Time.time * backgroundScrollSpeed;
        myMaterial.mainTextureOffset = new Vector2(0, offSet);

        //myMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }
}
