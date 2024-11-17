using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;   
using UnityEngine.SceneManagement; 
using SaveTheDog;

namespace SaveTheDog
{
    [AddComponentMenu("Save The Dog/Save The Dog UI Manager")]
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;

        public static UIManager Instance => instance;
        private static UIManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); 
            }
            else
            {
                Destroy(gameObject); 
            }
        }

        public void UpdateTimerText(int time,bool isGameOver=false)
        {
            if (timerText != null)
            {
                if(isGameOver)
                {
                    timerText.text="Game Over!!";
                    return;
                }

                if(time>0)
                {
                    timerText.text = time.ToString();
                }

                else
                {
                    timerText.text="Game Clear!!";
                }

                
                
            }
        }
    }

}
