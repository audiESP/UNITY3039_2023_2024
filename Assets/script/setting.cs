using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class setting : MonoBehaviour
{   
    public GameObject menu;
    [SerializeField] private bool menukey = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
       if(menukey){
         if(Input.GetKeyDown(KeyCode.Space)){
            menu.SetActive(true);
            menukey=false;
            Time.timeScale=0;
         }
       }
       else if(Input.GetKeyDown(KeyCode.Space)){
           menu.SetActive(false);
           menukey=true;
           Time.timeScale=1;
       }
       
    }
    public void Restart(){
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
        Time.timeScale=1;

    }
    public void Exit(){
         SceneManager.LoadScene(0);
         Time.timeScale=1;

    }
}
