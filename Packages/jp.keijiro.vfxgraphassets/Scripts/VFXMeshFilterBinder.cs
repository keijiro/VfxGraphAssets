using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace Klak.Vfx {

[AddComponentMenu("VFX/Property Binders/Mesh Filter Binder")]
[VFXBinder("Utility/Mesh Filter")]
public sealed class VFXMeshFilterBinder : VFXBinderBase
{
    [VFXPropertyBinding("UnityEngine.Mesh")]
    [FormerlySerializedAs("_meshProperty")]
    public ExposedProperty Property = "Mesh";

    public MeshFilter Target = null;

    public override bool IsValid(VisualEffect component)
      => Target != null && component.HasMesh(Property);

    public override void UpdateBinding(VisualEffect component)
      => component.SetMesh(Property, Target.sharedMesh);

    public override string ToString()
      => $"MeshFilter : '{Property}' -> " +
         (Target != null ? Target.name : "(null)");
}

} // namespace Klak.Vfx
