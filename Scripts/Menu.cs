using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class Menu : MonoBehaviour
    {
        public AudioMixer audioMixer;

        public void PlayGame()
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            SceneManager.LoadScene(2);
        }

        public void Options()
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(1);
        }

        public void Pause()
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(6);
        }

        public void Tutorial()
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(4);
        }

        public void Credits()
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(3);
        }

        public void SetVolume (float volume)
        {
            audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void QuitGame()
        {
            Debug.Log("QUIT!");
            Application.Quit();
        }
    }
}