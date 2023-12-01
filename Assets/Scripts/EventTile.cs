using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTile : MonoBehaviour
{
    public string eventName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            //do something : 전투 신 로드(loadScene(eventName))
        }
    }
}
