using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool isAlreadyTriggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isAlreadyTriggered) return;
            TriggerDialogue();
            isAlreadyTriggered = true;
        }
    }

    public void TriggerDialogue()
    {
        DialogueManager.instance.ShowDialogue(dialogue);
    }
}