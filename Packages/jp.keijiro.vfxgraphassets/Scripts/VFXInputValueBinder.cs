#if ENABLE_INPUT_BINDERS

using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;
using UnityEngine.InputSystem;

namespace Klak.Vfx {

[AddComponentMenu("VFX/Property Binders/Input Value Binder")]
[VFXBinder("Input/Input Value")]
public sealed class VFXInputValueBinder : VFXBinderBase
{
    [VFXPropertyBinding("System.Single"), FormerlySerializedAs("_property")]
    public ExposedProperty Property = "FloatParameter";

    [Space]
    public InputAction Action;

    public override bool IsValid(VisualEffect component)
      => Action != null && component.HasFloat(Property);

    protected override void OnEnable()
    {
        Action?.Enable();
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        Action?.Disable();
        base.OnDisable();
    }

    public override void UpdateBinding(VisualEffect component)
      => component.SetFloat(Property, Action.ReadValue<float>());

    public override string ToString()
      => $"Input Value: '{Property}' -> {Action?.name}";
}

} // namespace Klak.Vfx

#endif
