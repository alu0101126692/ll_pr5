using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class keyword : MonoBehaviour
{
    public string[] keywords;
    private KeywordRecognizer recognizer;
    public bool reconociendo;
    public dictate reconocedorDictado;
    // Start is called before the first frame update
    void Start()
    {
        recognizer = new KeywordRecognizer(keywords);
        
    }

    // Update is called once per frame
    void Update()
    {
        administarReconocedor();
        
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args) {
        Debug.Log ("Keyword: " + args.text + "; Confidence: " + args.confidence + "; Start Time: " + args.phraseStartTime + "; Duration: "  + args.phraseDuration);
    }


    private void empezarKeyword() {

        recognizer.OnPhraseRecognized += OnPhraseRecognized;
    }
    private void administarReconocedor() {
        if (!reconociendo && !reconocedorDictado.dictando) {
            if (Input.GetKeyDown("f")) {
                empezarKeyword();
                reconociendo = true;
                //reconocedorDictado.parar();

                recognizer.Start();
            } 

            
        } else if (reconociendo){
            if (Input.GetKeyDown("f")) {
                reconociendo = false;
                recognizer.Stop();
                parar();
            } 
        }
    }
    
    public void parar() {
        recognizer.Dispose();
    }
}
