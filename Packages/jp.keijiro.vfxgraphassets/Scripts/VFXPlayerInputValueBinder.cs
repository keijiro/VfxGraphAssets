#if ENABLE_INPUT_BINDERS

using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;
using UnityEngine.InputSystem;

namespace Klak.Vfx {

[System.Serializable]
public sealed class PlayerInputActionReference { public string Name; }

[AddComponentMenu("VFX/Property Binders/Player Input Value Binder")]
[VFXBinder("Input/Player Input Value")]
public sealed class VFXPlayerInputValueBinder : VFXBinderBase
{
    [VFXPropertyBinding("System.Single"), FormerlySerializedAs("_property")]
    public ExposedProperty Property = "FloatParameter";

    [Space]
    public PlayerInput Input;
    public PlayerInputActionReference Action;

    InputAction _cachedAction = null;

    public override bool IsValid(VisualEffect component)
      => Input != null && Action != null &&
         Input.actions.FindAction(Action.Name) != null &&
         component.HasFloat(Property);

    public override void UpdateBinding(VisualEffect component)
    {
        if (_cachedAction == null && Input != null && Action != null)
            _cachedAction = Input.actions.FindAction(Action.Name);
        component.SetFloat(Property, _cachedAction.ReadValue<float>());
    }

    public override string ToString()
      => $"Player Input Value: '{Property}' -> {Action?.Name}";
}

} // namespace Klak.Vfx

#endif
