using UnityEngine;

namespace MultiJam
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(IMouseInput))]
    public class CardView : MonoBehaviour, ICardView
    {
        #region Properites
        public string Name => gameObject.name;
        CardViewHandFsm FSM { get; set; }
        [SerializeField] public CardViewParameters cardConfigsParameters;
        IPlayerHandView Hand { get; set; }
        public IMouseInput Input { get; set; }
        public bool IsDragging => throw new System.NotImplementedException();

        public bool IsHovering => throw new System.NotImplementedException();

        public bool IsDisabled => throw new System.NotImplementedException();

        public bool IsPlayer => throw new System.NotImplementedException();

        #endregion

        #region Motion

        public BaseCardViewMotion Movement { get; private set; }
        public BaseCardViewMotion Rotation { get; private set; }
        public BaseCardViewMotion Scale { get; private set; }

        #endregion

        #region Components

        public Camera MainCamera => Camera.main;
        private Transform Transform { get; set; }

        public SpriteRenderer[] Renderers { get; set; }

        public SpriteRenderer Renderer { get; set; }

        public Collider Collider { get; set; }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          

        public Rigidbody Rigidbody { get; set; }

        public MonoBehaviour MonoBehaviour => this;

        #endregion

        #region Unity Callbacks
        
        private void Awake()
        {
            Transform = transform;
            Collider = GetComponent<Collider>();
            Rigidbody = GetComponent<Rigidbody>();
            Input = GetComponent<IMouseInput>();
            // Hand = transform.parent.GetComponentInChildren<IPlayerHandView>();
            Renderers = GetComponentsInChildren<SpriteRenderer>();
            Renderer = GetComponent<SpriteRenderer>();

            Movement = new MovementCardViewMotion(this);

            FSM = new CardViewHandFsm(MainCamera, cardConfigsParameters, this);
        }

        private void Update()
        {
            FSM?.Update();
            Movement?.Update();
        }

        #endregion

        public void Disable()
        {
            throw new System.NotImplementedException();
        }

        public void Discard()
        {
            throw new System.NotImplementedException();
        }

        public void Draw()
        {
            throw new System.NotImplementedException();
        }

        public void Enable()
        {
            throw new System.NotImplementedException();
        }

        public void Hover()
        {
            throw new System.NotImplementedException();
        }

        public void MoveTo(Vector3 position, float speed, float delay = 0)
        {
            throw new System.NotImplementedException();
        }

        public void MoveToWithZ(Vector3 position, float speed, float delay = 0)
        {
            throw new System.NotImplementedException();
        }

        public void RotateTo(Vector4 euler, float speed, float delay = 0)
        {
            throw new System.NotImplementedException();
        }

        public void ScaleTo(Vector3 scale, float speed, float delay = 0)
        {
            throw new System.NotImplementedException();
        }

        public void Select()
        {
            throw new System.NotImplementedException();
        }

        public void UnSelect()
        {
            throw new System.NotImplementedException();
        }
    }
}