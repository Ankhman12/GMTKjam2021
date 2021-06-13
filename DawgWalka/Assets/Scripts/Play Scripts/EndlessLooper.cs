using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessLooper : MonoBehaviour
{
    Grid grid;
    public Transform[] tiles;
    List<Transform> tilesList;
    Queue<Transform> queue;
    Obstacle[] obstacles; 
    List<Obstacle> obstaclesList;
    public Transform objectPool;
    int index = 0;
    float size = 11.8f;
    
    // Start is called before the first frame update
    void Start()
    {
        tilesList = new List<Transform>();
        foreach(Transform t in tiles) {
            tilesList.Add(t);
        }
        queue = new Queue<Transform>(tilesList.Count);
        
        for(int i = 0; i < tilesList.Count; i++) {
            tilesList[i].position = new Vector3(0, (i - 2) * size, 0);
            index = i - 1;
            queue.Enqueue(tilesList[i]);
        }

        obstacles = GetComponentsInChildren<Obstacle>();
        obstaclesList = new List<Obstacle>();
        foreach(Obstacle obs in obstacles) {
            obstaclesList.Add(obs);
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
            Obstacle[] prev = next.GetComponentsInChildren<Obstacle>();
            foreach(Obstacle obs in prev) {
                obstaclesList.Add(obs);
            }
            Populate(next);
            queue.Enqueue(next);
        }
    }

    void Populate(Transform next) {
        int obstacleCount = Random.Range(1, 3);
        for(int i = 0; i < obstacleCount; i++) {
            int index = Random.Range (0, obstaclesList.Count); 
            Debug.Log(obstaclesList.Count);
            Obstacle obs = obstaclesList[index];
            obstaclesList.RemoveAt(index);
            int attempts = 0;
            while(attempts < 20) {
                Vector3 newPosition = RandomPosition(next);
                obs.transform.position = newPosition;
                Collider2D collider = obs.GetComponent<Collider2D>();
                bool isTouching = false;
                foreach(Obstacle obstacleTouching in obstacles) {
                    if(collider.IsTouching(obstacleTouching.GetComponent<Collider2D>())) {
                        isTouching = true;
                        break;
                    }
                }
                if(isTouching == false) {
                    break;
                }
                attempts++;
            }
            obs.transform.parent = next;
            

        }

    }

    Vector3 RandomPosition(Transform next) {
        Vector2 rectPos = next.transform.position;
        float rectHeight = next.GetComponent<SpriteRenderer>().sprite.bounds.extents.y - 2;
        float rectWidth = next.GetComponent<SpriteRenderer>().sprite.bounds.extents.x - 2;

        float xpos = rectPos.x + Random.Range(-rectWidth, rectWidth);
        float ypos = rectPos.y + Random.Range(-rectHeight, rectHeight);

        return new Vector3(xpos, ypos, next.transform.position.z);
    }
}


