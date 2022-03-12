//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.2.0
//     from Assets/Others/PlayerControls.inputactions
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

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""8daaf4bd-7ebe-4ff1-9789-a14a7b716d9d"",
            ""actions"": [
                {
                    ""name"": ""Horizontal"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8d62357f-1b27-4a74-a14b-4e02c8b97eff"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""cf15370e-a638-4410-9a2b-3de3f13d99a5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""3b1ef4a9-1d78-4663-baaa-7f72392d7ec2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""LeftArrow|RightArrow"",
                    ""id"": ""d0266e14-2649-4c14-9266-372459d9c406"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3cc5eb29-46af-4611-a5d5-e4f21d2f7995"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""8e51628b-3842-439c-ade3-8ff713e802f4"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""A|D"",
                    ""id"": ""9fb44344-9732-458e-b4be-a9d70f6eacf9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""011258e5-e120-4ba6-b29e-739b5b8e592c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""cf5d3b31-72f1-4f11-81f8-96ba3c8e72f1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""86892d61-32c7-4221-a9ce-4eadb5a8ab82"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b2b7ccf0-de88-4fc6-a41b-d08457f47dca"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3ecf6a7-1323-4859-b0e4-5320dfdde09a"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7989a657-5e51-4de1-98e6-ed9e4e3c2a76"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a0e123d-c041-4858-bd42-0a0137017639"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Hacking"",
            ""id"": ""0c2a02d8-529e-4679-8be1-c69e5943d2c6"",
            ""actions"": [
                {
                    ""name"": ""ChangeBit"",
                    ""type"": ""Button"",
                    ""id"": ""711f4679-4c02-4ca9-a074-7a3c2cbd9c43"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Activate"",
                    ""type"": ""Button"",
                    ""id"": ""35be4ba8-6849-4484-a08c-8c85f3f32696"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PreviousBit"",
                    ""type"": ""Button"",
                    ""id"": ""aab13dcb-ef72-45e9-bbbe-6cda98b91b3f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""NextBit"",
                    ""type"": ""Button"",
                    ""id"": ""3a0dd916-8e8a-4fd7-a826-7beb12e172a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""BackToMenu"",
                    ""type"": ""Button"",
                    ""id"": ""c201f34b-0dfa-48bb-9fc7-2252f0f3f9c8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5b1c0b2d-b2a4-47bd-bd48-800778465cb0"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Activate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63a06655-af82-4a55-92ce-c242589790be"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Activate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21350b04-a777-4e92-98ce-d999da721fb7"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PreviousBit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f924f40a-2cbd-4b1a-aba9-5d21a7962400"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextBit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af675634-4c3e-4c7e-bed8-b50fde4ffdb9"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeBit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c5f4b34-7d42-47cf-8791-2633db0c7b92"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeBit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""600ec9c1-5c05-4e5f-96b0-4a125d9beec4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeBit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85681c0d-8f57-47d0-9c56-f68f3373bf49"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BackToMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_Horizontal = m_Movement.FindAction("Horizontal", throwIfNotFound: true);
        m_Movement_Jump = m_Movement.FindAction("Jump", throwIfNotFound: true);
        m_Movement_Interact = m_Movement.FindAction("Interact", throwIfNotFound: true);
        // Hacking
        m_Hacking = asset.FindActionMap("Hacking", throwIfNotFound: true);
        m_Hacking_ChangeBit = m_Hacking.FindAction("ChangeBit", throwIfNotFound: true);
        m_Hacking_Activate = m_Hacking.FindAction("Activate", throwIfNotFound: true);
        m_Hacking_PreviousBit = m_Hacking.FindAction("PreviousBit", throwIfNotFound: true);
        m_Hacking_NextBit = m_Hacking.FindAction("NextBit", throwIfNotFound: true);
        m_Hacking_BackToMenu = m_Hacking.FindAction("BackToMenu", throwIfNotFound: true);
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

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_Horizontal;
    private readonly InputAction m_Movement_Jump;
    private readonly InputAction m_Movement_Interact;
    public struct MovementActions
    {
        private @PlayerControls m_Wrapper;
        public MovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Horizontal => m_Wrapper.m_Movement_Horizontal;
        public InputAction @Jump => m_Wrapper.m_Movement_Jump;
        public InputAction @Interact => m_Wrapper.m_Movement_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @Horizontal.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnHorizontal;
                @Horizontal.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnHorizontal;
                @Horizontal.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnHorizontal;
                @Jump.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Interact.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Horizontal.started += instance.OnHorizontal;
                @Horizontal.performed += instance.OnHorizontal;
                @Horizontal.canceled += instance.OnHorizontal;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);

    // Hacking
    private readonly InputActionMap m_Hacking;
    private IHackingActions m_HackingActionsCallbackInterface;
    private readonly InputAction m_Hacking_ChangeBit;
    private readonly InputAction m_Hacking_Activate;
    private readonly InputAction m_Hacking_PreviousBit;
    private readonly InputAction m_Hacking_NextBit;
    private readonly InputAction m_Hacking_BackToMenu;
    public struct HackingActions
    {
        private @PlayerControls m_Wrapper;
        public HackingActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ChangeBit => m_Wrapper.m_Hacking_ChangeBit;
        public InputAction @Activate => m_Wrapper.m_Hacking_Activate;
        public InputAction @PreviousBit => m_Wrapper.m_Hacking_PreviousBit;
        public InputAction @NextBit => m_Wrapper.m_Hacking_NextBit;
        public InputAction @BackToMenu => m_Wrapper.m_Hacking_BackToMenu;
        public InputActionMap Get() { return m_Wrapper.m_Hacking; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(HackingActions set) { return set.Get(); }
        public void SetCallbacks(IHackingActions instance)
        {
            if (m_Wrapper.m_HackingActionsCallbackInterface != null)
            {
                @ChangeBit.started -= m_Wrapper.m_HackingActionsCallbackInterface.OnChangeBit;
                @ChangeBit.performed -= m_Wrapper.m_HackingActionsCallbackInterface.OnChangeBit;
                @ChangeBit.canceled -= m_Wrapper.m_HackingActionsCallbackInterface.OnChangeBit;
                @Activate.started -= m_Wrapper.m_HackingActionsCallbackInterface.OnActivate;
                @Activate.performed -= m_Wrapper.m_HackingActionsCallbackInterface.OnActivate;
                @Activate.canceled -= m_Wrapper.m_HackingActionsCallbackInterface.OnActivate;
                @PreviousBit.started -= m_Wrapper.m_HackingActionsCallbackInterface.OnPreviousBit;
                @PreviousBit.performed -= m_Wrapper.m_HackingActionsCallbackInterface.OnPreviousBit;
                @PreviousBit.canceled -= m_Wrapper.m_HackingActionsCallbackInterface.OnPreviousBit;
                @NextBit.started -= m_Wrapper.m_HackingActionsCallbackInterface.OnNextBit;
                @NextBit.performed -= m_Wrapper.m_HackingActionsCallbackInterface.OnNextBit;
                @NextBit.canceled -= m_Wrapper.m_HackingActionsCallbackInterface.OnNextBit;
                @BackToMenu.started -= m_Wrapper.m_HackingActionsCallbackInterface.OnBackToMenu;
                @BackToMenu.performed -= m_Wrapper.m_HackingActionsCallbackInterface.OnBackToMenu;
                @BackToMenu.canceled -= m_Wrapper.m_HackingActionsCallbackInterface.OnBackToMenu;
            }
            m_Wrapper.m_HackingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ChangeBit.started += instance.OnChangeBit;
                @ChangeBit.performed += instance.OnChangeBit;
                @ChangeBit.canceled += instance.OnChangeBit;
                @Activate.started += instance.OnActivate;
                @Activate.performed += instance.OnActivate;
                @Activate.canceled += instance.OnActivate;
                @PreviousBit.started += instance.OnPreviousBit;
                @PreviousBit.performed += instance.OnPreviousBit;
                @PreviousBit.canceled += instance.OnPreviousBit;
                @NextBit.started += instance.OnNextBit;
                @NextBit.performed += instance.OnNextBit;
                @NextBit.canceled += instance.OnNextBit;
                @BackToMenu.started += instance.OnBackToMenu;
                @BackToMenu.performed += instance.OnBackToMenu;
                @BackToMenu.canceled += instance.OnBackToMenu;
            }
        }
    }
    public HackingActions @Hacking => new HackingActions(this);
    public interface IMovementActions
    {
        void OnHorizontal(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface IHackingActions
    {
        void OnChangeBit(InputAction.CallbackContext context);
        void OnActivate(InputAction.CallbackContext context);
        void OnPreviousBit(InputAction.CallbackContext context);
        void OnNextBit(InputAction.CallbackContext context);
        void OnBackToMenu(InputAction.CallbackContext context);
    }
}
