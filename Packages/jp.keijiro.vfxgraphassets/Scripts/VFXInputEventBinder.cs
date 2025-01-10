#if ENABLE_INPUT_BINDERS

using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;
using UnityEngine.InputSystem;

namespace Klak.Vfx {

public sealed class VFXInputEventBinder : VFXEventBinderBase
{
    public InputAction Action = null;

    ExposedProperty _alpha = "alpha";

    protected override void SetEventAttribute(object[] parameters)
    {
        if (Action != null)
            eventAttribute.SetFloat(_alpha, Action.ReadValue<float>());
    }

    protected override void OnEnable()
    {
        if (Action != null)
        {
            Action.performed += OnPerformed;
            Action.Enable();
        }
        base.OnEnable();
    }

    void OnDisable()
    {
        if (Action != null)
        {
            Action.performed -= OnPerformed;
            Action.Disable();
        }
    }

    void OnPerformed(InputAction.CallbackContext ctx)
      => SendEventToVisualEffect();
}

} // namespace Klak.Vfx

#endif
