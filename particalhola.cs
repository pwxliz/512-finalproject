using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particalhola : MonoBehaviour
{
    // Start is called before the first frame update
    public class Circleposition 
    {
        public float radius = 0f, angle = 0f, time = 0f, size = 0f;
        public Circleposition(float radius, float angle, float time, float size) 
        {
            this.radius = radius;
            this.angle = angle;
            this.time = time;
            this.size = size;
        }
    }
    private ParticleSystem particlesystem;
    private ParticleSystem.Particle[] particlearray;
    private Circleposition[] circle;
    private int tier = 10;
    public int count = 1000;
    public float size = 0.03f;
    public float miniradius = 5.0f;
    public float maxradius = 12.0f;
    public float collect_MaxRadius = 4f;
    public float collect_MinRadius = 1f;
    public bool clockwise = true;
    public float speed = 2f;
    public float pingpong = 0.02f;
    public Gradient gradient;
    private float[] radius;        
    private float[] collect_radius;
    public int isCollected = 0;
    void Start()
    {
        particlearray = new ParticleSystem.Particle[count];
        circle = new Circleposition[count];

        particlesystem = this.GetComponent<ParticleSystem>();
        particlesystem.startSpeed = 0;
        particlesystem.startSize = size;
        particlesystem.loop = false;
        particlesystem.maxParticles = count;
        particlesystem.Emit(count);
        particlesystem.GetParticles(particlearray);
        RandomlySpread();
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[5];
        alphaKey[0].time = 0; alphaKey[0].alpha = 1f;
        alphaKey[1].time = 0.4f; alphaKey[1].alpha = 0.4f;
        alphaKey[2].time = 0.6f; alphaKey[2].alpha = 1f;
        alphaKey[3].time = 0.9f; alphaKey[3].alpha = 0.4f;
        alphaKey[4].time = 1f; alphaKey[4].alpha = 0.9f;
        GradientColorKey[] colorKey = new GradientColorKey[2];
        colorKey[0].time = 0; colorKey[0].color = Color.white;
        colorKey[1].time = 1f; colorKey[1].color = Color.white;
        gradient.SetKeys(colorKey, alphaKey);

        radius = new float[count];
        collect_radius = new float[count];

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < count; i++) 
        {
            if (clockwise)
            {
                circle[i].angle -= (i % tier + 1) * (speed / circle[i].radius / tier);
            }
            else 
            {
                circle[i].angle += (i % tier + 1) * (speed / circle[i].radius / tier);
            }
            circle[i].angle = (360.0f + circle[i].angle) % 360.0f;
            float theta = circle[i].angle / 180 * Mathf.PI;
            particlearray[i].position = new Vector3(circle[i].radius * Mathf.Cos(theta), 0f, circle[i].radius * Mathf.Sin(theta));

            float light = Random.Range(0, 1);
            particlearray[i].startColor = gradient.Evaluate(light);

        }
        particlesystem.SetParticles(particlearray, particlearray.Length);
        

        
    }

    void RandomlySpread() 
    {
        for (int i = 0; i < count; ++i)
        {
            float midradius = (maxradius + miniradius) / 2;
            float minirate = Random.Range(1.0f, midradius / miniradius);
            float maxrate = Random.Range(midradius / maxradius, 1.0f);
            float radius = Random.Range(miniradius * minirate, maxradius * maxrate);

            float angle = Random.Range(0.0f, 360.0f);
            float theta = angle / 180 * Mathf.PI;
            float time = Random.Range(0.0f, 360.0f);
            float size = Random.Range(0.03f, 0.1f);

            circle[i] = new Circleposition(radius, angle, time, size);

            particlearray[i].position = new Vector3(circle[i].radius * Mathf.Cos(theta), 0f, circle[i].radius * Mathf.Sin(theta));

        }

        particlesystem.SetParticles(particlearray, particlearray.Length);
    }

   
}
