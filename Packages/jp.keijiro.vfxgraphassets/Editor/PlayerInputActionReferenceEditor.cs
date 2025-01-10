#if ENABLE_INPUT_BINDERS

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using System.Linq;

namespace Klak.Vfx.Editor {

[CustomPropertyDrawer(typeof(PlayerInputActionReference))]
public sealed class PlayerInputActionReferenceEditor : PropertyDrawer
{
    PlayerInput _source;
    string [] _names;
    int [] _indices;

    void UpdateCache(SerializedProperty property)
    {
        // Retrieve a reference to the PlayerInput from the parent binder object.
        var playerInput = property.serializedObject.
            FindProperty("Input").objectReferenceValue as PlayerInput;

        // Clear the cache if the reference differs from the previous one.
        if (_source != playerInput)
        {
            _source = playerInput;
            _names = null;
        }

        // Try updating the cache.
        if (_names == null && _source != null)
        {
            _names = _source.actions.Select(a => a.name).ToArray();
            _indices = Enumerable.Range(0, _names.Length).ToArray();
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        UpdateCache(property);

        EditorGUI.BeginProperty(position, label, property);

        // Label
        position = EditorGUI.PrefixLabel
            (position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Name field
        var name = property.FindPropertyRelative("Name");

        if (_names == null)
        {
            // Name list is unavailable: Just show a string field.
            EditorGUI.PropertyField(position, name, GUIContent.none);
        }
        else
        {
            // Show the action name drop-down.
            var index = System.Array.FindIndex(_names, s => s == name.stringValue);
            var newIndex = EditorGUI.IntPopup(position, index, _names, _indices);

            // Update the name value if the selection was changed.
            if (index != newIndex) name.stringValue = _names[newIndex];
        }

        EditorGUI.EndProperty();
    }
}

} // namespace Klak.Vfx.Editor

#endif
