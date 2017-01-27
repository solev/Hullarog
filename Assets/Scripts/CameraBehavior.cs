using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;

    public float proportionY = 5f;
    public float proportionZ = 5f;

    public float proportion = 0.1f;

    public float smoothing = 5f;

    Vector3 offset;


    void Start()
    {
        Vector3 point = Middle();
        offset = transform.position - point;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player1.transform.position, player2.transform.position);

        if (distance < 12)
            distance = 12;

        Vector3 point = Middle();

        //Debug.Log("Players distance: " + distance);

        //Vector3 direction = (transform.position - point);



        //Vector3 targetCamPos = direction * distance * proportion;

        //Vector3 targetCamPos = new Vector3(point.x, distance*proportionY , distance*proportionZ);

        //transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing*Time.deltaTime);


        Vector3 targetCamPos = point + offset * distance * proportion;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

    }

    Vector3 Middle()
    {
        Vector3 point = new Vector3(
            (player1.transform.position.x + player2.transform.position.x) / 2,
            (player1.transform.position.y + player2.transform.position.y) / 2,
            (player1.transform.position.z + player2.transform.position.z) / 2
        );

        return point;
    }
}
