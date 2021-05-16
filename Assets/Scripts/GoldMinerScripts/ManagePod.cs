using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagePod : MonoBehaviour
{
    public GameObject target; // base object

    string state;

    LineRenderer lr;

    public float lineLenth = 2f;

    Vector3 point1;

    // contains sign for correct moving
    float speed = 30;

    // box border exchange wirh collider
    float minHeight = -5f;

    float maxWidth = 5f;

    float minWidth = -5f;

    void Start()
    {
        state = "rotate";

        lr = GameObject.FindWithTag("Rope").GetComponent<LineRenderer>();
        point1 = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 point2;
        float angle;
        float deltaX;
        float deltaY;
        Vector3 delta;
        Transform transf = gameObject.transform;

        switch (state)
        {
            case "rotate":
                transf
                    .RotateAround(target.transform.position,
                    new Vector3(0.0f, 0.0f, 1.0f),
                    speed * Time.deltaTime);

                // if right side end of the angles
                if (
                    transf.localEulerAngles.z >= 70 &&
                    transf.localEulerAngles.z < 90
                ) speed *= -1;

                // if left side end of the angles
                if (
                    transf.localEulerAngles.z >= 270 &&
                    transf.localEulerAngles.z < 290
                ) speed *= -1;

                point2 = gameObject.GetComponent<Renderer>().bounds.center;
                lr.SetPositions(new Vector3[] { point1, point2 });

                if (Input.GetKeyDown("space")) state = "shoot";

                break;
            case "shoot":
                angle = transf.localEulerAngles.z;
                angle *= Mathf.PI / 180f;

                deltaX = Mathf.Cos(angle);
                deltaY = Mathf.Sin(angle) * -1;

                delta = new Vector3(deltaX, deltaY, 0f);

                transf.Translate(delta * speed / 30 * Time.deltaTime);

                point2 = gameObject.GetComponent<Renderer>().bounds.center;

                lr.SetPositions(new Vector3[] { point1, point2 });

                // if (
                //     gameObject.transform.position.x > maxWidth ||
                //     gameObject.transform.position.x < minWidth ||
                //     gameObject.transform.position.y < minHeight
                // )
                // {
                //     state = "rewind";
                // }
                break;
            // case "rewind": // rewinding the hook...
            //     angle = transf.localEulerAngles.z;
            //     angle *= Mathf.PI / 180f;

            //     deltaX = Mathf.Cos(angle) * -1;
            //     deltaY = Mathf.Sin(angle) ;

            //     delta = new Vector3(deltaX, deltaY, 0f);

            //     transf.Translate(delta * speed / 30 * Time.deltaTime);
            //     point2 = gameObject.GetComponent<Renderer>().bounds.center;

            //     lr.SetPositions(new Vector3[] { point1, point2 });

            //     // rewind till correct lenght
            //     float curLineLenth =
            //         (point2.x - point1.x) * (point2.x - point1.x) +
            //         (point2.y - point1.y) * (point2.y - point1.y);

            //     if (curLineLenth <= lineLenth * lineLenth) state = "rotate";

            //     // find correct boulder and move within
            //     break;
        }
    }

    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     Debug.Log("collision" + collision.gameObject.name);
    //     state = "rewind";
    // }
}
