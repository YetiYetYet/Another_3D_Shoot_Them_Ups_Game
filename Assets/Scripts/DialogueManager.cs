using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    #region singleton

    private static DialogueManager _instance;
    public static DialogueManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion

    public List<GameObject> dialogues;
    public int dialogueIndex;

    private void OnEnable()
    {
        //throw new NotImplementedException();
    }

    private void OnDisable()
    {
        //throw new NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var dialogue in dialogues)
        {
            dialogue.SetActive(false);
        }

        StartCurrentDialogue();
    }

    private void StartCurrentDialogue()
    {
        if (dialogues.Count < dialogueIndex)
        {
            Debug.LogError("NoDialogueFound");
        }
        else
        {
            dialogues[dialogueIndex].SetActive(true);
        }
        
    }

    public void LoadNextDialogue()
    {
        dialogues[dialogueIndex].SetActive(false);
        dialogueIndex++;
    }

    public void StartDialogue()
    {
        dialogues[dialogueIndex].SetActive(true);
    }
    
    public void DialogueFinish()
    {
        dialogues[dialogueIndex].SetActive(false);
        LoadNextDialogue();
        GameManager.Instance.ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
