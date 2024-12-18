#if ENABLE_UITK_BINDERS

using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace Klak.Vfx {

[AddComponentMenu("VFX/Property Binders/UI Toolkit Float Value Binder")]
[VFXBinder("UI Toolkit/Float Value")]
public sealed class VFXUITKFloatValueBinder : VFXBinderBase
{
    [VFXPropertyBinding("System.Single")]
    public ExposedProperty Property = "FloatParameter";

    public UIFieldReference<float> Target;

    protected override void OnDisable()
    {
        Target.ClearReference();
        base.OnDisable();
    }

    public override bool IsValid(VisualEffect component)
      => Target.Field != null && component.HasFloat(Property);

    public override void UpdateBinding(VisualEffect component)
      => component.SetFloat(Property, Target.Field.value);

    public override string ToString()
      => $"UITK Float : {Property} -> {Target}";
}

} // namespace Klak.Vfx

#endif
