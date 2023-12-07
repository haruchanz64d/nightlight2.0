using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] private Image characterIcon;
    [SerializeField] private TextMeshProUGUI dialogueArea;
    public bool isDialogueActive = false;
    [SerializeField] private float typingSpeed = 0.1f;
    private Animator animator;
    [SerializeField] private Queue<DialogueLine> lines;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        if (instance == null) instance = this;
        lines = new Queue<DialogueLine>();
    }

    public void ShowDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;
        animator.Play("Popup");

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();
        characterIcon.sprite = currentLine.character.characterIcon;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));
    }

    private IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.dialogueLine.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        animator.Play("Idle");
    }
}
