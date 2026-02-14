// UMD IMDM290 
// Fatima Kamara

using UnityEngine;

public class Lerp1 : MonoBehaviour
{
    GameObject[] spheres;
    static int numSphere = 300; 
    float time = 0f;
    Vector3[] startPosition, endPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Assign proper types and sizes to the variables.
        spheres = new GameObject[numSphere];
        startPosition = new Vector3[numSphere]; 
        endPosition = new Vector3[numSphere]; 
        
        // Define target positions. Start = random, End = heart 
        for (int i =0; i < numSphere; i++){

            // Random start positions
            float r = 8f;
            startPosition[i] = new Vector3(
                r * Random.Range(-1f, 1f), 
                r * Random.Range(-1f, 1f), 
                r * Random.Range(-1f, 1f)
                
            );        
            // Paramateic heart equation 
            float t = i * 2 * Mathf.PI / numSphere;
            float x = 16f * Mathf.Pow(Mathf.Sin(t), 3);
            float y = 13f * Mathf.Cos(t)
                    - 5f * Mathf.Cos(2f * t)
                    - 2f * Mathf.Cos(3f * t)
                    - Mathf.Cos(4f * t);

            endPosition[i] = new Vector3(x, y, 0) * 0.2f;
        }
        // Spheres
        for (int i =0; i < numSphere; i++){
            //Primative elements
            spheres[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere); 
            spheres[i].transform.localScale = Vector3.one * 0.15f;
            // Position
            spheres[i].transform.position = startPosition[i];
            Destroy(spheres[i].GetComponent<Collider>());
            // Color. Get the renderer of the spheres and assign colors.
            Renderer sphereRenderer = spheres[i].GetComponent<Renderer>();
            // HSV color space
            float hue = (float)i / numSphere; // Hue cycles through 0 to 1
            Color color = Color.HSVToRGB(hue, 1f, 1f); // Full saturation and brightness
            sphereRenderer.material.color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Measure Time 
        time += Time.deltaTime; // Time.deltaTime = The interval in seconds from the last frame to the current one
        // Rotating heart (Creative)
        transform.Rotate(Vector3.up * 20f * Time.deltaTime);

        for (int i =0; i < numSphere; i++){
            // Lerp : Linearly interpolates between two points.
            float beat = Mathf.Pow(Mathf.Sin(time * 8f), 8f);
            float pulse = 1f + beat * 1f;

            // Pulese to heart shape
            Vector3 animatedEnd = endPosition[i] * pulse;

            // 3D spiral motion
             animatedEnd.z = Mathf.Sin(time * 3f + i * 0.1f) * .04f;

            // lerpFraction variable defines the point between startPosition and endPosition (0~1)
            // let it oscillate over time using sin function
            float lerpFraction = Mathf.Sin(time) * 0.5f + 0.5f;

            // Lerp logic. Update position 
            spheres[i].transform.position = 
                Vector3.Lerp(startPosition[i], animatedEnd, lerpFraction);

            // Color Update over time
            Renderer sphereRenderer = spheres[i].GetComponent<Renderer>();
            float hue = (float)i / numSphere; // Hue cycles through 0 to 1
            Color color = Color.HSVToRGB(Mathf.Abs(hue * Mathf.Sin(time)), Mathf.Cos(time), 2f + Mathf.Cos(time)); // Full saturation and brightness
            sphereRenderer.material.color = color;
        }
    }
}
