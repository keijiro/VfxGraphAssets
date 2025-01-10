#if ENABLE_INPUT_BINDERS

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using System.Linq;

namespace Klak.Vfx.Editor {

[CustomEditor(typeof(VFXPlayerInputEventBinder))]
sealed class VFXPlayerInputEventBinderEditor : UnityEditor.Editor
{
    #region Local members

    // Properties
    SerializedProperty _target;
    SerializedProperty _input;
    SerializedProperty _actionName;
    SerializedProperty _eventName;

    // Cache for action options
    string [] _nameOptions;
    int [] _indexOptions;

    // Variables used to detect focus on the drop-down
    readonly string _dropDownID = System.Guid.NewGuid().ToString();
    bool _dropDownWasFocused;

    // Refresh the action option cache.
    void RefreshActionOptions()
    {
        var input = _input.objectReferenceValue as PlayerInput;

        if (input != null)
        {
            _nameOptions = input.actions.Select(a => a.name).ToArray();
            _indexOptions = Enumerable.Range(0, _nameOptions.Length).ToArray();
        }
        else
        {
            _nameOptions = null;
            _indexOptions = null;
        }
    }

    #endregion

    #region Editor implementation

    void OnEnable()
    {
        _target = serializedObject.FindProperty("target");
        _input = serializedObject.FindProperty("Input");
        _actionName = serializedObject.FindProperty("_actionName");
        _eventName = serializedObject.FindProperty("EventName");

        RefreshActionOptions();
    }

    public override void OnInspectorGUI()
    {
        var shouldRefresh = false;

        serializedObject.Update();

        // Target VFX
        EditorGUILayout.PropertyField(_target);

        // PlayerInput (input source)
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(_input);
        shouldRefresh |= EditorGUI.EndChangeCheck();

        if (_nameOptions != null)
        {
            // Determine the current selection.
            var index = System.Array.FindIndex
                (_nameOptions, s => s == _actionName.stringValue);

            // Action selection drop-down and event name label
            GUI.SetNextControlName(_dropDownID);
            var newIndex = EditorGUILayout.IntPopup
                ("Action", index, _nameOptions, _indexOptions);
            EditorGUILayout.LabelField("Event Name", _eventName.stringValue);

            // Update the property if the selection was changed.
            if (index != newIndex)
            {
                _actionName.stringValue = _nameOptions[newIndex];
                _eventName.stringValue = "On" + _nameOptions[newIndex];
            }
        }

        // Detect focus on the drop-down.
        var dropDownFocus = GUI.GetNameOfFocusedControl() == _dropDownID;
        shouldRefresh |= dropDownFocus && !_dropDownWasFocused;
        _dropDownWasFocused = dropDownFocus;

        // Note about alpha attribute
        EditorGUILayout.HelpBox(
            "Input value (axis, pressure, etc.) will be set as an alpha " +
            "value of the event attribute.",
            MessageType.None
        );

        // Refresh the options if needed.
        if (shouldRefresh) RefreshActionOptions();

        serializedObject.ApplyModifiedProperties();
    }

    #endregion
}

} // namespace Klak.Vfx.Editor

#endif
