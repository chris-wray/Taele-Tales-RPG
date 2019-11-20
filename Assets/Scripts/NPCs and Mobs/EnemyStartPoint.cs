using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStartPoint : MonoBehaviour
{
    private SlimeController theEnemy;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 pos = transform.position;
        pos.x += 6;
        theEnemy = FindObjectOfType<SlimeController>();
        theEnemy.transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
