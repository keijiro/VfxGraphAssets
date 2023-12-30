using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace Klak.Vfx {

[AddComponentMenu("VFX/Property Binders/Mesh Filter Binder")]
[VFXBinder("Utility/Mesh Filter")]
public sealed class VFXMeshFilterBinder : VFXBinderBase
{
    public string MeshProperty
      { get => (string)_meshProperty;
        set => _meshProperty = value; }

    [VFXPropertyBinding("UnityEngine.Mesh"), SerializeField]
    ExposedProperty _meshProperty = "Mesh";

    public MeshFilter Target = null;

    public override bool IsValid(VisualEffect component)
      => Target != null && component.HasMesh(_meshProperty);

    public override void UpdateBinding(VisualEffect component)
      => component.SetMesh(_meshProperty, Target.sharedMesh);

    public override string ToString()
      => $"MeshFilter : '{_meshProperty}' -> " +
         (Target != null ? Target.name : "(null)");
}

} // namespace Klak.Vfx
