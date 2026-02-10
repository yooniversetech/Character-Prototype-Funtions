//using NUnit.Framework;
//using System.Collections.Generic;
//using UnityEngine;

//public class UIStateController : MonoBehaviour
//{
//    // 1) 상태 → 시스템 규칙
//    [SerializeField]
//    private List<UIStateEntry> stateEntries;
//    private Dictionary<UIState, UIStateRule> stateTable;

//    // 2) 상태 → UI 표현
//    [SerializeField]
//    private List<UIStateViewEntry> viewEntries;
//    private Dictionary<UIState, IUI> uiTable;

//    // 3) 시스템 참조
//    [SerializeField] private PlayerInputSystem playerInput;
//    [SerializeField] private TimeSystem timeSystem;
//    [SerializeField] private CameraSystem cameraSystem;

//    private UIState currentState = UIState.Normal;

//    private void Awake()
//    {
//        // 규칙 테이블 생성
//        stateTable = new Dictionary<UIState, UIStateRule>();
//        foreach (var entry in stateEntries)
//            stateTable.Add(entry.state, entry.rule);

//        // UI 테이블 생성
//        uiTable = new Dictionary<UIState, IUI>();
//        foreach (var view in viewEntries)
//            uiTable.Add(view.state, (IUI)view.uiBehaviour);
//    }

//    public void SetState(UIState newState)
//    {
//        ExitState(currentState);
//        ApplyState(newState);
//        currentState = newState;
//    }

//    private void ApplyState(UIState state)
//    {
//        // 시스템 규칙 적용
//        var rule = stateTable[state];
//        playerInput.SetEnabled(rule.allowPlayerInput);
//        timeSystem.SetPaused(rule.pauseTime);
//        cameraSystem.SetLocked(rule.lockCamera);

//        // UI 표시
//        if (uiTable.TryGetValue(state, out var ui))
//            ui.ShowUI();
//    }

//    private void ExitState(UIState state)
//    {
//        if (uiTable.TryGetValue(state, out var ui))
//            ui.HideUI();
//    }
//}

//[System.Serializable]
//public class UIStateEntry
//{
//    public UIState state;
//    public UIStateRule rule;
//}