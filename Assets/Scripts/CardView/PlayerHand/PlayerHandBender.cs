using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MultiJam
{
    public class PlayerHandBender : MonoBehaviour
    {
        #region Fields & Properties

        [SerializeField]
        CardViewParameters parameters;

        [SerializeField]
        [Tooltip("Controls the curve that the hand uses.")]
        private Vector3 curveStart = new Vector3(-2f, -0.7f, 0), curveEnd = new Vector3(2f, -0.7f, 0);

        [SerializeField]
        [Tooltip("Controls the area which is considered 'in-hand' allowing cards to be selected/reordered. " +
            "If a card leaves this area it can be used upon releasing the mouse button." +
            "Recommend having the hand bounds go past the screen edges to prevent accidental use when reordering cards quickly.")]
        private Vector2 handOffset = new Vector2(0, -0.3f), handSize = new Vector2(9, 1.7f);

        [SerializeField]
        [Tooltip("Allow cards to tilt when not in hand, based on the velocity of mouse movement.")]
        private bool cardTilt = true;

        [SerializeField]
        [Tooltip("Overlay camera that is rendering the cards. Used for raycasting mouse position.")]
        Camera camOverlay = null;

        private Material lineMaterial;

        private Plane plane;    // world XY plane, used for mouse position raycasts

        private Vector2 force;
        private Vector2 heldCardTilt;
        private Vector2 prevMousePos;
        private Vector3 mouseWorldPos;
        private Vector2 mousePosDelta;
        private Vector2 screenRatio;

        private Rect handBounds;
        private bool mouseInsideHand;

        IPlayerHandView PlayerHand { get; set; }
        Vector2 MousePos
        {
            get
            {
                Vector2 _ = Mouse.current.position.value;
                return new Vector2
                    (
                    Mathf.Clamp(_.x, 0, Screen.width),
                    Mathf.Clamp(_.y, 0, Screen.height)
                    );
            }
        }

        #endregion

        #region UnityCallbacks

        //private void Awake()
        //{
        //    PlayerHand = GetComponent<IPlayerHandView>();
        //    PlayerHand.OnPileChanged += Bend;
        //}

        private void Start()
        {
            prevMousePos = Mouse.current.position.value;
            handBounds = new Rect(handOffset - handSize / 2, handSize);
            plane = new Plane(-Vector3.forward, transform.position);
            screenRatio = new Vector2(1600f / Screen.width, 900f / Screen.height);
        }

        private void OnRenderObject()
        {
            CreateLineMaterial();

            lineMaterial.SetPass(0);

            GL.PushMatrix();

            GL.MultMatrix(transform.localToWorldMatrix);

            GL.Begin(GL.LINES);
            GL.Color(Color.blue);

            DrawSphere(curveStart, 0.03f, Color.blue);
            DrawSphere(Vector3.zero, 0.03f, Color.blue);
            DrawSphere(curveEnd, 0.03f, Color.blue);

            Vector3 p1 = curveStart;
            for (int i = 0; i < 20; i++)
            {
                float t = (i + 1) / 20f;
                Vector3 p2 = GetCurvePoint(curveStart, Vector3.zero, curveEnd, t);
                GL.Vertex(p1);
                GL.Vertex(p2);
                p1 = p2;
            }

            GL.End();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.MultMatrix(transform.localToWorldMatrix);
            GL.Begin(GL.LINES);
            //GL.Color(mouseInsideHand ? Color.red : Color.blue);
            GL.Color(Color.blue);

            DrawWireCube(handOffset, handSize);

            GL.End();
            GL.PopMatrix();
        }

        //private void Update()
        //{
        //    Ray ray = camOverlay.ScreenPointToRay(MousePos);
        //    if (plane.Raycast(ray, out float enter))
        //        mouseWorldPos = ray.GetPoint(enter);

        //    Vector3 point = transform.InverseTransformPoint(mouseWorldPos);
        //    mouseInsideHand = handBounds.Contains(point);
        //}

        #endregion

        #region Operations

        public void Bend(ICardView[] cards)
        {
            if (cards == null)
                throw new ArgumentException("Can't bend a card list null");

            for (int i = 0; i < cards.Length; i++)
            {
                ICardView card = cards[i];

                if (!card.FSM.IsCurrent<CardViewIdle>()) continue;

                float t = (float)(i + 0.5f) / cards.Length;
                Vector3 cardPos = GetCurvePoint(curveStart + transform.position, transform.position, curveEnd + transform.position, 1 - t);

                Vector3 cardUp = GetCurveNormal(curveStart + transform.position, transform.position, curveEnd + transform.position, 1 - t);

                cardPos.z = -i * 0.1f;

                Vector3 cardRot = Quaternion.LookRotation(Vector3.forward, cardUp).eulerAngles;

                card.MoveToWithZ(cardPos, parameters.MovementSpeed);
                card.RotateTo(cardRot, parameters.RotationSpeed);
            }
        }

        /// <summary>
        ///     Obtains a point along a curve based on 3 points.
        ///     Equal to Lerp(Lerp(a, b, t), Lerp(b, c, t), t).
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector3 GetCurvePoint(Vector3 a, Vector3 b, Vector3 c, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return (oneMinusT * oneMinusT * a) + (2f * oneMinusT * t * b) + (t * t * c);
        }

        public static Vector3 GetCurveNormal(Vector3 a, Vector3 b, Vector3 c, float t)
        {
            Vector3 tangent = GetCurveTangent(a, b, c, t);
            return Vector3.Cross(tangent, Vector3.forward);
        }

        public static Vector3 GetCurveTangent(Vector3 a, Vector3 b, Vector3 c, float t)
        {
            return 2f * (1f - t) * (b - a) + 2f * t * (c - b);
        }

        private void CreateLineMaterial()
        {
            if (!lineMaterial)
            {
                Shader shader = Shader.Find("Hidden/Internal-Colored");
                lineMaterial = new Material(shader);
                lineMaterial.hideFlags = HideFlags.HideAndDontSave;

                lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);

                lineMaterial.SetInt("_ZWrite", 0);
            }
        }

        private void DrawSphere(Vector3 center, float radius, Color color)
        {
            GL.Color(color);
            float step = Mathf.PI * 0.1f;
            for (float theta = 0; theta < 2 * Mathf.PI; theta += step)
            {
                GL.Vertex(center + new Vector3(Mathf.Cos(theta) * radius, Mathf.Sin(theta) * radius, 0));
                GL.Vertex(center + new Vector3(Mathf.Cos(theta + step) * radius, Mathf.Sin(theta + step) * radius, 0));
            }
        }

        private void DrawWireCube(Vector3 center, Vector2 size)
        {
            Vector3 halfSize = new Vector3(size.x / 2, size.y / 2, 0);

            Vector3[] vertices = new Vector3[]
            {
                center + new Vector3(-halfSize.x, -halfSize.y, 0),
                center + new Vector3(halfSize.x, -halfSize.y, 0),
                center + new Vector3(halfSize.x, halfSize.y, 0),
                center + new Vector3(-halfSize.x, halfSize.y, 0),
            };

            for (int i = 0; i < vertices.Length; i++)
            {
                GL.Vertex(vertices[i]);
                GL.Vertex(vertices[(i + 1) % vertices.Length]);
            }
        }

        #endregion
    }
}