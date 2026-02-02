using System.Collections;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance { get; private set; }

    [SerializeField] private DialogueUI _ui;
    [SerializeField] private TypewriterEffect _typewriter;

    private DialogueData _currentDialogue;
    private int _currentLineIndex;
    private bool _endingDialogue;
    private bool _canAdvance;


    public bool IsActive { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _ui.Hide();
        _ui.HideArrow();
    }

    private void Update()
    {
        if (!IsActive) return;
        if (!_canAdvance) return;

        if (Input.GetKeyDown(KeyCode.Space))
            Advance();
    }

    public void StartDialogue(DialogueData dialogue)
    {
        if (IsActive) return;
        StartCoroutine(StartDialogueRoutine(dialogue));
    }



    private void Advance()
    {
        if (_typewriter.IsTyping)
        {
            _typewriter.Skip(
                _currentDialogue.Lines[_currentLineIndex].Text,
                _ui.DialogueText
            );
            return;
        }

        _currentLineIndex++;

        if (_currentLineIndex >= _currentDialogue.Lines.Count)
        {
            EndDialogue();
        }
        else
        {
            PlayCurrentLine();
        }
    }

    private void PlayCurrentLine()
    {
        StartCoroutine(PlayLineRoutine());
    }

    private void EndDialogue()
    {
        if (_endingDialogue) return;
        StartCoroutine(EndDialogueRoutine());
    }

    private IEnumerator StartDialogueRoutine(DialogueData dialogue)
    {
        _currentDialogue = dialogue;
        _currentLineIndex = 0;

        IsActive = true;

        _ui.Show();
        GameStateController.Instance.SetState(GameState.Dialogue);

        yield return null; //Wait 1 frame

        PlayCurrentLine();
    }
    private IEnumerator PlayLineRoutine()
    {
        _ui.HideArrow();
        _canAdvance = false;
        var line = _currentDialogue.Lines[_currentLineIndex];

        _ui.SetLine(line);

        if (line.PreDelay > 0)
            yield return new WaitForSeconds(line.PreDelay);

        _typewriter.Play(line.Text, _ui.DialogueText, line);

        while (_typewriter.IsTyping)
            yield return null;

        if (line.PostDelay > 0)
            yield return new WaitForSeconds(line.PostDelay);

        _canAdvance = true;
        _ui.ShowArrow();

    }


    private IEnumerator EndDialogueRoutine()
    {
        _ui.HideArrow();
        _endingDialogue = true;

        _ui.Hide();
        GameStateController.Instance.SetState(GameState.Exploration);

        yield return null; //Wait 1 frame

        IsActive = false;
        _currentDialogue = null;
        _endingDialogue = false;
    }

}
