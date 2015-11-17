using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Sheep : MonoBehaviour
{
    private Rigidbody rb;
    private float dist3;
    private float dist1;
    private float dist2;
    public int numsheep;
    private int[] sheepxcoords;
    private int[] sheepycoords;
    private double min;
    private double max;
    private float best1;
    private float best2;
    private float best3;
    private float sheep1;
    private float sheep2;
    private float sheep3;
    private float minradius = 10;
    private float slope1;
    private float slope2;
    private float b;
    private float c;
    float[] coord1 = new float[2];
    float[] coord2 = new float[2];
    float[] coord3 = new float[2];
    private int k = 0;
    private float x;
    private float y;
    private float dx;
    private float dy;
    private float dr;
    private float D;
    private float d;
    private float xx;
    private float yy;
    private float x1;
    private float y1;
    private float xx1;
    private float yy1;
    private float r;
    private float rr;
    public int s;
    float[] co = new float[2];
    float[] coo = new float[2];
    float[] cooo = new float[2];
    private float oppositex;
    private float oppositey;
    private float oppositeyy;
    private float oppositexx;
    private float distancesheppard1;
    private float distancesheppard2;
    private float check1;
    public float rangex;
    public float rangexx;
    public float rangey;
    public float rangeyy;
    float[] farthestcoords;
    private float speed = 1;


    struct SheepDistance
    {
        public int sheepId;
        public float distance;
    }
    // Now some methods are defined  for the social component. 

    // This is a sgn function that returns a -1 or a 1. 
    public int sgn(float dy)
    {
        if (dy < 0)
        {
            s = -1;
        }
        else
        {
            s = 1;
        }
        return s;
    }

    // This method returns the smallest euclidean distance. 
    public float comp(float dist, float dist1)
    {
        if (dist > dist1)
        {
            dist3 = dist1;
        }
        else
        {
            dist3 = dist;
        }
        return dist3;
    }

    // This method returns a 2-D coordinate array corresponding to the shortes euclidean distance.  
    public float[] comparecoords(float dist3, float dist)
    {
        co = new float[2];
        if (dist == dist3)
        {
            co[0] = x;
            co[1] = y;
        }
        else
        {
            co[0] = xx;
            co[1] = yy;
        }
        return co;
    }

    // This method returns a 2-D coordinate array which corresponds to the second option of shortest euclidean distance. 
    public float[] comparecoords1(float dist3, float dist)
    {
        coo = new float[2];
        if (dist == dist3)
        {
            coo[0] = x1;
            coo[1] = y1;
        }
        else
        {
            coo[0] = xx1;
            coo[1] = yy1;
        }
        return coo;
    }

    //  Compares the two options and checks which is the shortest and returns those corresponding coordinates into arrat cooo. 
    public float[] comparecoords2(float dist3, float dist)
    {
        cooo = new float[2];
        if (dist == dist3)
        {
            cooo[0] = co[0];
            cooo[1] = co[1];
        }
        else
        {
            cooo[0] = coo[0];
            cooo[1] = coo[1];
        }
        return cooo;
    }

    // Now some methods for the cognitive component. 

    // Method for checking farthest distance away from sheppard. 
    public float comparefarthest(float dist1, float dist2)
    {
        if (dist1 > dist2)
        {
            return dist1;
        }
        else
        {
            return dist2;
        }

    }

    // Checks which distance corresponds to distance x and returns corresponding coordinates. 
    // This method reurns the coordinates of the desired location of the sheep itself. 

    public float[] givefarthestcoords(float dist1, float dist2, float dist3)
    {
        farthestcoords = new float[2];
        if (dist1 == dist2)
        {
            if (oppositex >= rangex && oppositex <= rangexx && oppositey >= rangey && oppositey <= rangeyy)
            {
                farthestcoords[0] = oppositex;
                farthestcoords[1] = oppositey;
            }
            else
            {
                farthestcoords[0] = oppositexx;
                farthestcoords[1] = oppositeyy;
            }
        }
        else
        {
            if (oppositexx >= rangex && oppositexx <= rangex && oppositey >= rangey && oppositeyy <= rangeyy)
            {
                farthestcoords[0] = oppositexx;
                farthestcoords[1] = oppositeyy;
            }
            else
            {
                farthestcoords[0] = oppositex;
                farthestcoords[1] = oppositey;
            }

        }
        return farthestcoords;
    }

    void Start()
    {
        var K = GameObject.FindGameObjectWithTag("Kudde");
        float randx = Random.Range(0f, 10f);
        float randz = Random.Range(0f, 10f);
        K.transform.position = new Vector3(randx, 0, randz);
    }

    void Update()
    {
        // Ai for the sheep taking the hound into account. 
        var R = GameObject.FindGameObjectWithTag("Hond");
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Schaap");
        foreach (GameObject go in gos)
        {
            Vector3 Sheep1 = new Vector3(go.transform.position.x, 0, go.transform.position.z);
            Vector3 Honden = new Vector3(R.transform.position.x, 0, R.transform.position.z);
            float schaapx = Sheep1.x;
            float schaapy = Sheep1.z;
            float euclidd = Mathf.Sqrt((Mathf.Pow(Honden.x - schaapx, 2)) + (Mathf.Pow(Honden.y - schaapy, 2)));
            if (euclidd <= 5)
            {
                float helling = (schaapx - Honden.x) / (schaapy - Honden.y);
                float a = Mathf.Pow(helling, 2);
                float b = 2 * helling;
                float afstand = 1;
                float c = -Mathf.Pow(afstand, 2);
                float discriminant = Mathf.Pow(b, 2) - (4 * a * c);
                oppositey = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
                oppositeyy = (-b - Mathf.Sqrt(discriminant)) / (2 * a);
                oppositex = (helling * oppositey);
                oppositexx = (helling * oppositeyy);
                // Now we check what pair of coordinates is farthest from the sheppard, but within the range of the field. 
                distancesheppard1 = Mathf.Sqrt(Mathf.Pow(Honden.x - oppositex, 2) + Mathf.Pow(Honden.y - oppositey, 2));
                distancesheppard2 = Mathf.Sqrt(Mathf.Pow(Honden.x - oppositexx, 2) + Mathf.Pow(Honden.y - oppositeyy, 2));
                check1 = comparefarthest(distancesheppard1, distancesheppard2);
                // Now check which distance compares to which x and y and return these x and y coordinates in an array. 
                givefarthestcoords(check1, distancesheppard1, distancesheppard2); // These are the desired coordinates of the cognitive component in 
                                                                                  // the farthes coords array. 
                go.transform.Translate(new Vector3((farthestcoords[0] * speed * Time.deltaTime), 0, (farthestcoords[1] * speed * Time.deltaTime)));

            }
        }



        // Ai for the sheep taking the sheppard into account. 
        //    float oldposx = rb.position.x;
        //  float oldposy = rb.position.y;

        //cognitive compponent
        for (int t = 0; t < numsheep; t = t + 1) {
            GameObject[] goss = GameObject.FindGameObjectsWithTag("Schaap");
            foreach (GameObject go in goss)
            {


                var H = GameObject.FindGameObjectWithTag("Herder");
                Vector3 HerderCoordinaten = new Vector3(H.transform.position.x, 0, H.transform.position.z);
                Vector3 Sheep = new Vector3(go.transform.position.x, 0, go.transform.position.z);
                float herderx = HerderCoordinaten.x;
                float herdery = HerderCoordinaten.z;
                float sheepx = Sheep.x;
                float sheepy = Sheep.z;

                float euclid = Mathf.Sqrt((Mathf.Pow(herderx - sheepx, 2)) + (Mathf.Pow(herdery - sheepy, 2)));
                // The sheep each time runs a certain distance away. 
                //float DistanceToRun = 1;
                // If the sheep is within range of the sheppard then the sheep has to run in the other direction. 
                if (euclid <= 20)
                {
                    float helling = ((sheepx - herderx) / (sheepy - herdery));
                    float a = Mathf.Pow(helling, 2);
                    float b = 2 * helling;
                    float afstand = 1;
                    float c = -Mathf.Pow(afstand, 2);
                    float discriminant = Mathf.Pow(b, 2) - (4 * a * c);
                    oppositey = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
                    oppositeyy = (-b - Mathf.Sqrt(discriminant)) / (2 * a);
                    oppositex = (helling * oppositey);
                    oppositexx = (helling * oppositeyy);
                    // Now we check what pair of coordinates is farthest from the sheppard, but within the range of the field. 
                    distancesheppard1 = Mathf.Sqrt(Mathf.Pow(herderx - oppositex, 2) + Mathf.Pow(herdery - oppositey, 2));
                    distancesheppard2 = Mathf.Sqrt(Mathf.Pow(herderx - oppositexx, 2) + Mathf.Pow(herdery - oppositeyy, 2));
                    check1 = comparefarthest(distancesheppard1, distancesheppard2);
                    // Now check which distance compares to which x and y and return these x and y coordinates in an array. 
                    givefarthestcoords(check1, distancesheppard1, distancesheppard2); // These are the desired coordinates of the cognitive component. 

                }




                // Now the addition of the social component. 

                GameObject[] gosss = GameObject.FindGameObjectsWithTag("Schaap");
                foreach (GameObject go1 in gosss) {
                    Vector3 Sheep1 = new Vector3(go1.transform.position.x, 0, go1.transform.position.z);

                    for (int i = 0; i < numsheep; i = i + 1) {

                        List<SheepDistance> euclidean3 = new List<SheepDistance>(numsheep);

                        // create a certain amount of sheep with precalculated coordinates in unity. 
                        float euclidean1 = Mathf.Pow((Sheep1.x - sheepx), 2);
                        float euclidean2 = Mathf.Pow((Sheep1.z - sheepy), 2);
                        euclidean3[i] = new SheepDistance()
                        {
                            sheepId = i,
                            distance = i == t ? float.MaxValue : Mathf.Sqrt(euclidean1 + euclidean2)
                        };
                    }


                    euclidean3.Sort((s1, s2) => s1.distance == s2.distance ? 0 : ((s1.distance < s2.distance) ? -1 : 1));

                    int best1 = euclidean3[0].sheepId;
                    coord1[0] = sheepxcoords[best1];
                    coord1[1] = sheepycoords[best1];
                    int best2 = euclidean3[1].sheepId;
                    coord2[0] = sheepxcoords[best2];
                    coord2[1] = sheepycoords[best2];
                    int best3 = euclidean3[2].sheepId;
                    coord3[0] = sheepxcoords[best3];
                    coord3[1] = sheepycoords[best3];
                    // end of ranking code.
                    //  slope1 = (coord2[1] - coord1[1]) / (coord2[0] - coord1[0]);
                    dx = coord2[0] - coord1[0];
                    dy = coord2[1] - coord1[1];
                    dr = Mathf.Sqrt(Mathf.Pow(dx, 2) + Mathf.Pow(dy, 2));
                    D = (coord1[0] * coord2[1]) - (coord2[0] * coord1[1]);
                    x = ((D * dy) + sgn(dy) * dx * Mathf.Sqrt((Mathf.Pow(r, 2) * Mathf.Pow(dr, 2)) - Mathf.Pow(D, 2))) / Mathf.Pow(dr, 2);
                    xx = ((D * dy) - sgn(dy) * dx * Mathf.Sqrt((Mathf.Pow(r, 2) * Mathf.Pow(dr, 2)) - Mathf.Pow(D, 2))) / Mathf.Pow(dr, 2);
                    y = ((-D * dx) + Mathf.Abs(dy) * Mathf.Sqrt((Mathf.Pow(r, 2) * Mathf.Pow(dr, 2)) - Mathf.Pow(D, 2))) / Mathf.Pow(dr, 2);
                    yy = ((-D * dx) - Mathf.Abs(dy) * Mathf.Sqrt((Mathf.Pow(r, 2) * Mathf.Pow(dr, 2)) - Mathf.Pow(D, 2))) / Mathf.Pow(dr, 2);
                    // Now we have the coordinates of two points and need to find the point shortest to sheep 2.
                    dist1 = Mathf.Sqrt(Mathf.Pow(coord2[0] - x, 2) + Mathf.Pow(coord2[1] - y, 2));
                    dist2 = Mathf.Sqrt(Mathf.Pow(coord2[0] - xx, 2) + Mathf.Pow(coord2[1] - yy, 2));
                    // Point A is constructed below. 
                    dist3 = comp(dist1, dist2);
                    comparecoords(dist3, dist1);
                    float dxx = coord3[0] - coord1[0];
                    float dyy = coord3[1] - coord1[1];
                    float drr = Mathf.Sqrt(Mathf.Pow(dxx, 2) + Mathf.Pow(dyy, 2));
                    float DD = (coord1[0] * coord3[1]) - (coord3[0] * coord1[1]);
                    float x1 = ((DD * dyy) + sgn(dyy) * dxx * Mathf.Sqrt((Mathf.Pow(rr, 2) * Mathf.Pow(dr, 2)) - Mathf.Pow(DD, 2))) / Mathf.Pow(drr, 2);
                    float xx1 = ((DD * dyy) - sgn(dyy) * dxx * Mathf.Sqrt((Mathf.Pow(rr, 2) * Mathf.Pow(drr, 2)) - Mathf.Pow(DD, 2))) / Mathf.Pow(drr, 2);
                    float y1 = ((-DD * dxx) + Mathf.Abs(dyy) * Mathf.Sqrt((Mathf.Pow(rr, 2) * Mathf.Pow(drr, 2)) - Mathf.Pow(DD, 2))) / Mathf.Pow(drr, 2);
                    float yy1 = ((-DD * dxx) - Mathf.Abs(dyy) * Mathf.Sqrt((Mathf.Pow(rr, 2) * Mathf.Pow(drr, 2)) - Mathf.Pow(DD, 2))) / Mathf.Pow(drr, 2);
                    float dist11 = Mathf.Sqrt(Mathf.Pow(coord3[0] - x1, 2) + Mathf.Pow(coord3[1] - y1, 2));
                    float dist22 = Mathf.Sqrt(Mathf.Pow(coord3[0] - xx1, 2) + Mathf.Pow(coord3[1] - yy1, 2));
                    // Point B is constructed below. 
                    float dist33 = comp(dist11, dist22);
                    comparecoords1(dist33, dist11);
                    // two point A and B have been calculated now. 
                    float dist100 = (Mathf.Sqrt(Mathf.Pow(co[0] - coord2[0], 2) + Mathf.Pow(co[1] - coord2[1], 2))) + (Mathf.Sqrt(Mathf.Pow(co[0] - coord3[0], 2) + Mathf.Pow(co[1] - coord3[1], 2)));
                    float dist101 = (Mathf.Sqrt(Mathf.Pow(coo[0] - coord2[0], 2) + Mathf.Pow(coo[1] - coord2[1], 2))) + (Mathf.Sqrt(Mathf.Pow(coo[0] - coord3[0], 2) + Mathf.Pow(coo[1] - coord3[1], 2)));
                    float dist102 = comp(dist100, dist101);
                    comparecoords2(dist102, dist100);
                    float f1 = farthestcoords[0];
                    float f2 = farthestcoords[1];
                    float g1 = cooo[0];
                    float g2 = cooo[1];
                    // comparecoords2 contains the position where the sheep in the herd has to go to. 
                    // This desired position is calculated for each sheep and is updated whenever the sheep walk to the desired point and have reached it or 
                    // when a sheppard comes and scares or lures the sheep. 


                    // Now the social component and the cognitive component will be merged together.
                    // The utlimate position for each sheep will be calculated by using the social and cognitive component from this PSO problem. 
                    float c1 = 0.8f;
                    float c2 = 0.2f;
                    float r1 = Random.Range(0f, 1f);
                    float r2 = Random.Range(0f, 1f);
                    float newposx = sheepx + (c1 * r1 * farthestcoords[0]) + (c2 * r2 * cooo[0]);
                    float newposy = sheepy + (c1 * r1 * farthestcoords[1]) + (c2 * r2 * cooo[1]);

                    // Now tell the sheepOnject to move in the desired direction. 
                    //   Vecotr3 dirx = newposx - sheepx;
                    //  Vector3 dirz = newposy - sheepy;
                    //if(move > dist) move = dist;


                    // apply movement
                    transform.Translate(new Vector3((newposx * speed * Time.deltaTime), 0, (newposy * speed * Time.deltaTime)));

                }
            }
        }
        // closes update. 
    }