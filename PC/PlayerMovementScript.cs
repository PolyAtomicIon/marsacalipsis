using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float m_Speed = 25f;
    
    private Vector3 moveDirection;

    public Transform model;
    public Transform to_model_left;
    public Transform to_model_right;
    public Transform to_origin;
    
    public float fuel;

    Vector3 relativePosition;
    Quaternion targetRotation;

    private float lerpTime = 20f; // was 15f;
    private float currentLerpTime = 0;

    private float nextActionTime, period, _timer, fuel_used = 0.6f;

    private Vignette vignette;
    public Camera camera;
    public float perspective_max = 60f, perspective_min = 20f;

    public Text gameOver, quotes;

    private void update_camera(){
        float diff = perspective_max - perspective_min;
        float x = diff * fuel / 100;
        camera.fieldOfView = perspective_min + x;
        x = fuel / 100f;
        x = 1 - x;
        vignette.intensity.value = 0.2f + x;
    }

    public void update_fuel(float x = 10f){
        fuel += x;
        if(fuel > 100f)
            fuel = 100f;
        update_camera();
    }

    void update_scene(){
        Time.timeScale = 1f;

        GameObject[] clones = GameObject.FindGameObjectsWithTag ("clone");
            foreach(GameObject clone in clones) 
                Destroy(clone);
            
        SceneManager.LoadScene("Game");
    }

    void game_over(string message){
        Debug.Log(message);
        gameOver.gameObject.SetActive(true);
        Time.timeScale = 0f;
        
        quotes.gameObject.SetActive(true);
    }

    void Start()
    {
        gameOver.gameObject.SetActive(false);
        quotes.gameObject.SetActive(false);

        PostProcessVolume volume = camera.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignette);

        m_Rigidbody = GetComponent<Rigidbody>();
        fuel = 100f;
        nextActionTime = 0f;
        period = 0.1f;
        _timer = 0f;
        perspective_max = 65f;
        perspective_min = 30f;
    }

    void Update(){
        currentLerpTime += Time.deltaTime;
        
        _timer += Time.deltaTime;
        if (_timer > nextActionTime ) {
            nextActionTime += period;
            update_fuel(-fuel_used);
            Debug.Log(fuel);
        }

        if( fuel <= 0 ){
            game_over("out of fuel");
        }
        
        if (Input.GetKeyDown("r")) {
            update_scene();
        }  

    }

    void FixedUpdate()
    {

        moveDirection = new Vector3(0, 0, 1).normalized;
   
        m_Rigidbody.MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection) * m_Speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d")){
            transform.Rotate(new Vector3(0, 5, 0) * Time.deltaTime * m_Speed);
            model.rotation = Quaternion.Lerp(model.rotation, to_model_right.rotation, lerpTime * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a")){
            transform.Rotate(new Vector3(0, -5, 0) * Time.deltaTime * m_Speed);
            model.rotation = Quaternion.Lerp(model.rotation, to_model_left.rotation, lerpTime * Time.deltaTime);
        }
        else{
            model.rotation = Quaternion.Lerp(model.rotation, to_origin.rotation, lerpTime * Time.deltaTime);
        }

    }

    
    void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.name == "Asteroid(Clone)" || col.gameObject.name == "Asteroid2(Clone)")
        {
            game_over("hit!");
        }
    }

}