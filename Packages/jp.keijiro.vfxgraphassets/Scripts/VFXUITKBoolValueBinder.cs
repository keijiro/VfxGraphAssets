#if ENABLE_UITK_BINDERS

using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace Klak.Vfx {

[AddComponentMenu("VFX/Property Binders/UI Toolkit Bool Value Binder")]
[VFXBinder("UI Toolkit/Bool Value")]
public sealed class VFXUITKBoolValueBinder : VFXBinderBase
{
    [VFXPropertyBinding("System.Boolean")]
    public ExposedProperty Property = "BoolParameter";

    public UIFieldReference<bool> Target;

    protected override void OnDisable()
    {
        Target.ClearReference();
        base.OnDisable();
    }

    public override bool IsValid(VisualEffect component)
      => Target.Field != null && component.HasBool(Property);

    public override void UpdateBinding(VisualEffect component)
      => component.SetBool(Property, Target.Field.value);

    public override string ToString()
      => $"UITK Bool : {Property} -> {Target}";
}

} // namespace Klak.Vfx

#endif
