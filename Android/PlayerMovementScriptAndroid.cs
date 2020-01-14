using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using System;

public class PlayerMovementScriptAndroid : MonoBehaviour
{

    enum Constants
    {
        max_touches = 15
    }

    private float ScreenWidth;
    Rigidbody m_Rigidbody;
    public float m_Speed = 100f;
    public float rotation_touch_speed = 2.5f;
    
    private Vector3 moveDirection;

    public Transform model;
    public Transform to_model_left;
    public Transform to_model_right;
    public Transform to_origin;
    public float fuel;

    Vector3 relativePosition;
    Quaternion targetRotation;

    private float lerpTime = 50f; // was 15f;
    private float currentLerpTime = 0;

    private float nextActionTime, period, _timer, fuel_used = 0.1f;

    public Text gameOver, quotes;

    private float rotation_speed = 1.2f;
    private bool game_over_bool;
    private int touches;

    public Vector3 planetPosition;
    public float planetRadius;
    public float characterHeight;

    public Camera main_cam;
    public PlayersCamera Main_Camera;

    public class PlayersCamera {
            
        public Camera camera;
            
        private Vignette vignette;
        PostProcessVolume volume;
        public float perspective_max = 115f, perspective_min = 15f;
        
        public Color TeleportColor, DefaultColor;

        public PlayersCamera(){
            TeleportColor = new Color();
            ColorUtility.TryParseHtmlString("#96071D", out TeleportColor);

            DefaultColor = new Color();
            ColorUtility.TryParseHtmlString("#054120", out DefaultColor);

        }

        public void StartSettings(){
            PostProcessVolume volume = camera.GetComponent<PostProcessVolume>();
            volume.profile.TryGetSettings(out vignette);
        }

        public void update_camera(float Fuel){
            // Camera zoom disabled, because it looks weird (low fps) on smartphones, dont know why
            
            float diff = perspective_max - perspective_min;
            float x = diff * Fuel / 100;
            camera.fieldOfView = perspective_min + x;
            x = Fuel / 100f;
            x = 1 - x;
            vignette.intensity.value = x;
            vignette.color.value = DefaultColor;
        }

    }

    public void update_fuel(float x = 60f){
        fuel += x;
        if(fuel > 100f)
            fuel = 100f;
        Main_Camera.update_camera(fuel);
    }

    void update_scene(){
        game_over_bool = false;
        Time.timeScale = 1f;

        GameObject[] clones = GameObject.FindGameObjectsWithTag ("clone");
            foreach(GameObject clone in clones) 
                Destroy(clone);
            
        SceneManager.LoadScene("GameAndroid");
    }

    void game_over(string message){
        Debug.Log(message);
        game_over_bool = true;
        gameOver.gameObject.SetActive(true);
        quotes.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    void Start()
    {

        touches = 0;

        game_over_bool = false;
        ScreenWidth = Screen.width;
        
        gameOver.gameObject.SetActive(false);
        quotes.gameObject.SetActive(false);

        m_Rigidbody = GetComponent<Rigidbody>();
        fuel = 500f;
        nextActionTime = 0f;
        period = 0.025f;
        _timer = 0f;
        
        Main_Camera = new PlayersCamera();
        Main_Camera.camera = main_cam;
        Main_Camera.StartSettings();

    }

    void Update(){
        currentLerpTime += Time.deltaTime;
        
        _timer += Time.deltaTime;
        if (_timer > nextActionTime ) {
            nextActionTime += period;
            update_fuel(-fuel_used);
            //Debug.Log(fuel);
        }

        if( fuel <= 0 ){
            game_over("out of fuel");
        } 
        
        int i = 0;
		//loop over every touch found
		while (i < Input.touchCount) {

            if ( (Input.GetTouch (i).position.x > ScreenWidth / 2 || Input.GetTouch (i).position.x < ScreenWidth / 2) && game_over_bool) {
                if( touches == (int) Constants.max_touches || Input.GetKey("r")){
                    update_scene();
                    break;
                }
                else{
                    touches += 1;
                }
            } 
            i++;
        }

        if (Input.GetKeyDown("r")) {
            update_scene();
        }  

    }

    void FixedUpdate()
    {
        // for PC testing
        moveDirection = new Vector3(0, 0, 1).normalized;
   
        m_Rigidbody.MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection) * m_Speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d")){
            transform.Rotate(new Vector3(0, rotation_speed, 0) * Time.deltaTime * m_Speed);
            model.rotation = Quaternion.Lerp(model.rotation, to_model_right.rotation, lerpTime * Time.deltaTime);
            rotation_speed += 0.015f;
            rotation_speed = Math.Max(rotation_speed, 2.3f);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a")){
            transform.Rotate(new Vector3(0, -rotation_speed, 0) * Time.deltaTime * m_Speed);
            model.rotation = Quaternion.Lerp(model.rotation, to_model_left.rotation, lerpTime * Time.deltaTime);
            rotation_speed += 0.015f;
            rotation_speed = Math.Max(rotation_speed, 2.3f);
        }
        else{
            model.rotation = Quaternion.Lerp(model.rotation, to_origin.rotation, lerpTime * Time.deltaTime);
            rotation_speed = 1.2f;
        }
       
        moveDirection = new Vector3(0, 0, 1).normalized;
   
        m_Rigidbody.MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection) * m_Speed * Time.deltaTime);

        int i = 0;
		//loop over every touch found
        // git
		while (i < Input.touchCount) {
            lerpTime = 30f;

			if (Input.GetTouch (i).position.x > ScreenWidth / 2 || Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d")) {
				transform.Rotate(new Vector3(0, rotation_speed, 0) * Time.deltaTime * m_Speed);
                model.rotation = Quaternion.Lerp(model.rotation, to_model_right.rotation, lerpTime * Time.deltaTime);
                rotation_speed += 0.015f;
                rotation_speed = Math.Max(rotation_speed, 2.3f);
			}
			else if (Input.GetTouch (i).position.x < ScreenWidth / 2 || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a")) {
				transform.Rotate(new Vector3(0, -rotation_speed, 0) * Time.deltaTime * m_Speed);
                model.rotation = Quaternion.Lerp(model.rotation, to_model_left.rotation, lerpTime * Time.deltaTime);
                rotation_speed += 0.015f;
                rotation_speed = Math.Max(rotation_speed, 2.3f);
			}
            else{
                // model.rotation = Quaternion.Lerp(model.rotation, to_origin.rotation, lerpTime * Time.deltaTime);
                rotation_speed = 1.2f;
            }
			++i;
		}
        
    }

    void LateUpdate(){
        lerpTime = 5f;
        model.rotation = Quaternion.Lerp(model.rotation, to_origin.rotation, lerpTime * Time.deltaTime);
    }

    void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.name == "Asteroid(Clone)" || col.gameObject.name == "Asteroid2(Clone)" || col.gameObject.name == "BigExplosion(Clone)" )
        {
            game_over("hit!");
        }
        /*
        if( col.gameObject.name == "MarsPod" ){
            Debug.Log("HeY");

            vignette.intensity.value = 1f;
            vignette.color.value = TeleportColor;
            
            Vector3 movingPosition = Random.onUnitSphere * (planetRadius + characterHeight * 0.5f) + planetPosition;
            gameObject.transform.position = movingPosition;

        }
        */
    }

}