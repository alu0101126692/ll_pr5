using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class dictate : MonoBehaviour
{


    private DictationRecognizer dictado;
    public bool dictando;
    public keyword reconocedorKeyword;

    void Start()
    {
        dictado = new DictationRecognizer();
        

    }

    // Update is called once per frame
    void Update()
    {
        administarDictado();
    }


    private void empezarDictado() {

        dictado.DictationResult += (text, confidence) =>
        {
            Debug.LogFormat("Dictation result: {0}", text);
        };

        dictado.DictationHypothesis += (text) =>
        {
            Debug.LogFormat("Dictation hypothesis: {0}", text);
        };

        dictado.DictationComplete += (completionCause) =>
        {
            if (completionCause != DictationCompletionCause.Complete)
                Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
        };

        dictado.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        };
    }
    private void administarDictado() {
        if (!dictando && !reconocedorKeyword.reconociendo) {
            if (Input.GetKeyDown("g")) {
                empezarDictado();
                dictando = true;
                //reconocedorKeyword.parar();
                dictado.Start();
            } 

            
        } else if (dictando){
            if (Input.GetKeyDown("g")) {
                dictando = false;
                dictado.Stop();
                parar();
            } 
        }
    }

    public void parar() {
        if (dictado != null)
            dictado.Dispose();
    }
}
