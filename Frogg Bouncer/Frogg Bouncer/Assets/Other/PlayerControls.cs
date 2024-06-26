//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Other/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""TheOneButton"",
            ""id"": ""a73a8211-ca0b-45e1-90b0-dc8cc19ede9f"",
            ""actions"": [
                {
                    ""name"": ""Tounge"",
                    ""type"": ""Button"",
                    ""id"": ""6f526f0a-0dca-474d-81af-2b02dd98a27e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=1.401298E-45,behavior=2)"",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""29083aa2-a129-465b-9871-b2eb590e57ae"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tounge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c1bafbc-c498-4d49-8ed1-d2e5e670715c"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tounge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06b41da9-11d0-4a08-aae8-c26764d7f4c1"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tounge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // TheOneButton
        m_TheOneButton = asset.FindActionMap("TheOneButton", throwIfNotFound: true);
        m_TheOneButton_Tounge = m_TheOneButton.FindAction("Tounge", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // TheOneButton
    private readonly InputActionMap m_TheOneButton;
    private List<ITheOneButtonActions> m_TheOneButtonActionsCallbackInterfaces = new List<ITheOneButtonActions>();
    private readonly InputAction m_TheOneButton_Tounge;
    public struct TheOneButtonActions
    {
        private @PlayerControls m_Wrapper;
        public TheOneButtonActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Tounge => m_Wrapper.m_TheOneButton_Tounge;
        public InputActionMap Get() { return m_Wrapper.m_TheOneButton; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TheOneButtonActions set) { return set.Get(); }
        public void AddCallbacks(ITheOneButtonActions instance)
        {
            if (instance == null || m_Wrapper.m_TheOneButtonActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_TheOneButtonActionsCallbackInterfaces.Add(instance);
            @Tounge.started += instance.OnTounge;
            @Tounge.performed += instance.OnTounge;
            @Tounge.canceled += instance.OnTounge;
        }

        private void UnregisterCallbacks(ITheOneButtonActions instance)
        {
            @Tounge.started -= instance.OnTounge;
            @Tounge.performed -= instance.OnTounge;
            @Tounge.canceled -= instance.OnTounge;
        }

        public void RemoveCallbacks(ITheOneButtonActions instance)
        {
            if (m_Wrapper.m_TheOneButtonActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ITheOneButtonActions instance)
        {
            foreach (var item in m_Wrapper.m_TheOneButtonActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_TheOneButtonActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public TheOneButtonActions @TheOneButton => new TheOneButtonActions(this);
    public interface ITheOneButtonActions
    {
        void OnTounge(InputAction.CallbackContext context);
    }
}
