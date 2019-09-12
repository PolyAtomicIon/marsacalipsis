using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PlayerMovementScriptAndroid : MonoBehaviour
{
    private float ScreenWidth;
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

    private float lerpTime = 50f; // was 15f;
    private float currentLerpTime = 0;

    private float nextActionTime, period, _timer, fuel_used = 0.16f;

    private Vignette vignette;
    public Camera camera;
    public float perspective_max = 60f, perspective_min = 15f;

    public Text gameOver, quotes;

    private bool game_over_bool;
    private int touches;

    public Vector3 planetPosition;
    public float planetRadius;
    public float characterHeight;

    Color TeleportColor, DefaultColor;

    private void update_camera(){
        // Camera zoom disabled, because it looks weird (low fps) on smartphones, dont know why
        
        float diff = perspective_max - perspective_min;
        float x = diff * fuel / 100;
        camera.fieldOfView = perspective_min + x;
        x = fuel / 100f;
        x = 1 - x;
        vignette.intensity.value = 0.13f + x;
        vignette.color.value = DefaultColor;
    }

    public void update_fuel(float x = 15f){
        fuel += x;
        if(fuel > 100f)
            fuel = 100f;
        update_camera();
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
        
        TeleportColor = new Color();
        ColorUtility.TryParseHtmlString("#96071D", out TeleportColor);

        DefaultColor = new Color();
        ColorUtility.TryParseHtmlString("#054120", out DefaultColor);

        touches = 0;

        game_over_bool = false;
        ScreenWidth = Screen.width;
        
        gameOver.gameObject.SetActive(false);
        quotes.gameObject.SetActive(false);

        PostProcessVolume volume = camera.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignette);

        m_Rigidbody = GetComponent<Rigidbody>();
        fuel = 100f;
        nextActionTime = 0f;
        period = 0.025f;
        _timer = 0f;
        perspective_max = 65f;
        perspective_min = 20f;
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
        
        int i = 0;
		//loop over every touch found
		while (i < Input.touchCount) {

            if ( (Input.GetTouch (i).position.x > ScreenWidth / 2 || Input.GetTouch (i).position.x < ScreenWidth / 2) && game_over_bool) {
                if( touches == 15 ){
                    update_scene();
                    break;
                }
                else{
                    touches += 1;
                }
            } 
            i++;
        }

    }

    void FixedUpdate()
    {
        
        moveDirection = new Vector3(0, 0, 1).normalized;
   
        m_Rigidbody.MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection) * m_Speed * Time.deltaTime);
        
        int i = 0;
		//loop over every touch found
		while (i < Input.touchCount) {
            lerpTime = 30f;

			if (Input.GetTouch (i).position.x > ScreenWidth / 2) {
				transform.Rotate(new Vector3(0, 6, 0) * Time.deltaTime * m_Speed);
                model.rotation = Quaternion.Lerp(model.rotation, to_model_right.rotation, lerpTime * Time.deltaTime);
			}
			else if (Input.GetTouch (i).position.x < ScreenWidth / 2) {
				transform.Rotate(new Vector3(0, -6, 0) * Time.deltaTime * m_Speed);
                model.rotation = Quaternion.Lerp(model.rotation, to_model_left.rotation, lerpTime * Time.deltaTime);
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
        if(col.gameObject.name == "Asteroid(Clone)" || col.gameObject.name == "Asteroid2(Clone)")
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