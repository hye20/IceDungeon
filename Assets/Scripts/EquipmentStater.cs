using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentStater : MonoBehaviour
{
    Vector3 endPos = Vector3.zero;

    PenguinStarter penguinStarter;

    void Awake()
    {
        penguinStarter = GameObject.FindWithTag("Penguin").GetComponent<PenguinStarter>();
    }

    void Update()
    {
        if (GameManager.instance.player.controller.isStart)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, 2.0f * Time.deltaTime);

            if (penguinStarter.itemPicked)
            {
                Destroy(gameObject);
            }
        }
    }
}
