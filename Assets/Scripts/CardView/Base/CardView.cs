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
        [SerializeField] public CardViewParameters cardConfigsParameters;
        public CardViewHandFsm FSM { get; set; }
        IPlayerHandView Hand { get; set; }
        public IMouseInput Input { get; set; }
        public bool IsDragging => false;

        public bool IsHovering => false;

        public bool IsDisabled => false;

        public bool IsPlayer => true;

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

        public MeshRenderer Renderer { get; set; }

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
            Renderer = GetComponentInChildren<MeshRenderer>();

            Scale = new ScaleCardViewMotion(this);
            Movement = new MovementCardViewMotion(this);
            Rotation = new RotationCardViewMotion(this);


            FSM = new CardViewHandFsm(MainCamera, cardConfigsParameters, this);
        }

        private void Update()
        {
            FSM?.Update();
            Scale?.Update();
            Movement?.Update();
            Rotation?.Update();
        }

        #endregion

        public void Enable() => FSM.Enable();

        public void Disable() => FSM.Disable();

        public void Draw() => FSM.Draw();

        public void Discard() => FSM.Discard();

        public void Hover() => FSM.Hover();

        public void Select()
        {
            if (!IsPlayer) return;

            Hand.SelectCard(this);
            FSM.Select();
        }

        public void UnSelect() => FSM.Unselect();

        public void MoveTo(Vector3 position, float speed, float delay = 0) => Movement.Execute(position, speed, delay);

        public void MoveToWithZ(Vector3 position, float speed, float delay = 0) => Movement.Execute(position, speed, delay, true);

        public void RotateTo(Vector3 rotation, float speed, float delay = 0) => Rotation.Execute(rotation, speed);

        public void ScaleTo(Vector3 scale, float speed, float delay = 0) => Scale.Execute(scale, speed, delay);
    }
}