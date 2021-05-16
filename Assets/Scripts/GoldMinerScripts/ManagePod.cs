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

    Vector3 point2;

    GameObject pickedBoulder;

    public Vector3 delta = new Vector3(0, 0, 0);

    // contains sign for correct moving
    float speed = 30f;

    // box border exchange wirh collider
    float minHeight = -5f;

    float maxWidth = 5f;

    float minWidth = -5f;

    void Start()
    {
        state = "rotate";
        pickedBoulder = null;

        gameObject.transform.position = new Vector3(0f, lineLenth, 0f);

        lr = GameObject.FindWithTag("Rope").GetComponent<LineRenderer>();
        point1 = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Transform transf = gameObject.transform;

        // Debug.Log(state);
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
                point2 = gameObject.GetComponent<Renderer>().bounds.center;
                lr.SetPositions(new Vector3[] { point1, point2 });

                delta = getDeltaVector3(point1, point2);

                transf.position += delta * speed / 10 * Time.deltaTime;

                if (
                    transf.position.x > maxWidth ||
                    transf.position.x < minWidth ||
                    transf.position.y < minHeight
                ) state = "rewind";

                break;
            case "rewind": // rewinding the hook
                point2 = gameObject.GetComponent<Renderer>().bounds.center;
                lr.SetPositions(new Vector3[] { point1, point2 });

                delta = getDeltaVector3(point1, point2);

                transf.position += delta * speed / 10 * Time.deltaTime * -1;

                //move boulder
                if (pickedBoulder != null)
                {
                    pickedBoulder.transform.position +=
                        delta * speed / 10 * Time.deltaTime;
                }

                // rewind till correct lenght
                float curLineLenth = distBetweenTwoPoints(point1, point2);

                if (curLineLenth <= lineLenth * lineLenth)
                {
                    state = "rotate";
                    if (pickedBoulder != null)
                    {
                        Destroy (pickedBoulder);
                        pickedBoulder = null;
                    }
                }

                break;
        }
    }

    Vector3 getDeltaVector3(Vector3 point1, Vector3 point2)
    {
        float c = Mathf.Sqrt(distBetweenTwoPoints(point1, point2));
        float a = (point2.x - point1.x);
        float b = (point2.y - point1.y);

        return new Vector3(a / c, b / c, 0f);
    }

    float distBetweenTwoPoints(Vector3 point1, Vector3 point2)
    {
        return (point2.x - point1.x) * (point2.x - point1.x) +
        (point2.y - point1.y) * (point2.y - point1.y);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        state = "rewind";

        string name = collider.gameObject.name;
        pickedBoulder = GameObject.Find("Boulders/" + name);
    }
}
