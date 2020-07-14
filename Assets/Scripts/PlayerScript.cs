
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    public Sprite runSprite; //Added for lab 12 to make him jump
    public Sprite jumpSprite;

    public LayerMask groundLayer;

    float speed = 3f;
    Rigidbody2D rb;
    Vector3 startingPosition; // If we hit a spike we will teleport player to starting position.

    public string currentLetter;

    public GameObject currentLetterDisplay; 
    public GameObject TimeDisplay; 

    private float timer = 0f;
    private int intTimer = 0; 

    public AudioSource source; //Sound effect


    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the rigidbody component added to the object and store it in rb
        startingPosition = transform.position;

        currentLetter = "A";
    }



    void Update()
    {
        timer += Time.deltaTime;

        if((int)timer != intTimer){
            intTimer = (int) timer;
            TimeDisplay.GetComponent<UnityEngine.UI.Text>().text = "Time: " + intTimer; 
        }

        if(gameObject.transform.position.y < -15f){
            transform.position = startingPosition;
        }




        var input = Input.GetAxis("Horizontal"); // This will give us left and right movement (from -1 to 1). 
        var movement = input * speed;

        rb.velocity = new Vector3(movement, rb.velocity.y, 0);

       
        if(movement < 0){
            float xScale = Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(-xScale, transform.localScale.y, transform.localScale.z);
        }
        else if(movement > 0){
            float xScale = Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
        }

        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();//Added for lab 12

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            
            if(isGrounded()){
                rb.AddForce(new Vector3(0, 400, 0)); 
            }
            
        }

    if (isGrounded()){ //Added in lab 12; change him back to normal sprite after jumping
        sr.sprite = runSprite;
         
    }
    else if(!isGrounded()){
        sr.sprite = jumpSprite;
    }
    else{
        Debug.Log("No Sprite Found");
    }
        
    }


    void OnTriggerEnter2D(Collider2D col) // col is the trigger object we collided with
    { 
        
          string s1 = col.GetComponent<UnityEngine.UI.Text>().text;
        //Touching the current letter (Must increment currentletter, delete the letter collided with, and check if they won by getting z)
        if(string.Compare(currentLetter, s1, true) == 0){

            //Check if it was Z, this means they won
            if(string.Compare(currentLetter, "Z") == 0){
                PassingVarThroughScene.finalTime = intTimer;
                SceneManager.LoadScene("WinScreen");
            }
            
            //Incrementing the current letter
            char c1 = Convert.ToChar(currentLetter);
            c1++; 
            currentLetter = Char.ToString(c1); 
            currentLetterDisplay.GetComponent<UnityEngine.UI.Text>().text = "Current Letter: " + currentLetter; 

            //They collected the letter so It must now be destroyed
            Destroy(col.gameObject);
            //PLay happy collected noise here
            source.Play();

        }


    }

    bool isGrounded() {
    Vector2 position = transform.position;
    Vector2 direction = Vector2.down;
    float distance = 1.0f;
    
    Debug.DrawRay(position, direction, Color.green); //Used to see character hitbox
    RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
    if (hit.collider != null) {
          
          //We hit something

          string s1 = hit.collider.GetComponent<UnityEngine.UI.Text>().text;
        //Touching the current letter (Must increment currentletter, delete the letter collided with, and check if they won by getting z)
        if(string.Compare(currentLetter, s1, true) == 0){

            //Check if it was Z, this means they won
            if(string.Compare(currentLetter, "Z") == 0){
                PassingVarThroughScene.finalTime = intTimer;
                SceneManager.LoadScene("WinScreen");
            }
            
            //Incrementing the current letter
            char c1 = Convert.ToChar(currentLetter);
            c1++; 
            currentLetter = Char.ToString(c1); 
            currentLetterDisplay.GetComponent<UnityEngine.UI.Text>().text = "Current Letter: " + currentLetter; 

            //They collected the letter so It must now be destroyed
            Destroy(hit.collider.gameObject);
            //PLay happy collected noise here
            source.Play();

        }


          
          
          //Debug.Log(hit.collider.GetComponent<UnityEngine.UI.Text>().text);
        return true;

      
    }
    


    return false;
}
}
