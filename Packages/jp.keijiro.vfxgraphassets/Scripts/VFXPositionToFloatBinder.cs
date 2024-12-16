using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace Klak.Vfx {

[AddComponentMenu("VFX/Property Binders/Position To Float Binder")]
[VFXBinder("Utility/Position To Float")]
public sealed class VFXPositionToFloatBinder : VFXBinderBase
{
    public enum Axis { X, Y, Z };

    [VFXPropertyBinding("System.Single")]
    public ExposedProperty Property = "FloatParameter";

    public Transform Target = null;
    public Axis ReferenceAxis = Axis.X;
    public bool UseGlobal;

    float TargetValue
      => UseGlobal ? Target.position[(int)ReferenceAxis] :
                     Target.localPosition[(int)ReferenceAxis];

    public override bool IsValid(VisualEffect component)
      => Target != null && component.HasFloat(Property);

    public override void UpdateBinding(VisualEffect component)
      => component.SetFloat(Property, TargetValue);

    public override string ToString()
      => $"Position To Float : '{Property}' -> " +
         (Target != null ? Target.name : "(null)");
}

} // namespace Klak.Vfx
