using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessLooper : MonoBehaviour
{
    Grid grid;
    Transform[] tiles;
    List<Transform> tilesList;
    Queue<Transform> queue;
    int index = 0;
    float size = 11.8f;
    
    // Start is called before the first frame update
    void Start()
    {
        tiles = GetComponentsInChildren<Transform>();
        tilesList = new List<Transform>();
        foreach(Transform t in tiles) {
            if(t != transform) {
                tilesList.Add(t);
            }
        }
        queue = new Queue<Transform>(tilesList.Count);
        
        for(int i = 0; i < tilesList.Count; i++) {
            tilesList[i].position = new Vector3(0, (i - 2) * size, 0);
            index = i - 1;
            queue.Enqueue(tilesList[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Transform next = queue.Peek();
        if(next.position.y < -30) {
            next = queue.Dequeue();
            next.localPosition = new Vector3(0, index * size, 0);
            index++;
            queue.Enqueue(next);
        }
        Debug.Log(queue.Peek());
    }
}
