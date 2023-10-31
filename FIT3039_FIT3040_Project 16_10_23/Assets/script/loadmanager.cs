using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadmanager : MonoBehaviour
{
    public GameObject LoadScreen;
    public Slider slider;
    public Text text;
   
    public void LoadNextLevel()
    {
       StartCoroutine(LoadLevel());
    }
    IEnumerator LoadLevel(){
        LoadScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
        operation.allowSceneActivation=false;
        while(!operation.isDone){
            slider.value = operation.progress;
            text.text=operation.progress*100 +"%";
            if(operation.progress>=0.9f){
                slider.value=1;
                text.text="Press any key to continue";
                if(Input.anyKeyDown){
                     operation.allowSceneActivation= true;
                }
            }
      

            yield return null;

        }
    }
}
