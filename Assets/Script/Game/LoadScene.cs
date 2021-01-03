using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Game
{
    public class LoadScene  : MonoBehaviour
    {

        public void LoadNextScene()
        {
            StartCoroutine( Loading(SceneManager.GetActiveScene().buildIndex + 1));
        }


        public void LoadLastScene()
        {
            StartCoroutine( Loading(SceneManager.GetActiveScene().buildIndex - 1));
        }


        IEnumerator Loading(int scensIndex)
        {
            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene(scensIndex);
        }
    }
}
