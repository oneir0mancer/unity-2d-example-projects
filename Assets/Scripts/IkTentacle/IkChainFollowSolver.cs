using UnityEngine;

namespace IkTentacle
{
    public class IkChainFollowSolver : MonoBehaviour
    {
        public event System.Action<Vector3[]> OnSolveStepEvent;
        
        public int Length => _length;
        
        [SerializeField] private Transform _targetDirection;
        [SerializeField] private bool _useDirectionlessChain;
        [Header("Settings")]
        [SerializeField, Min(1)] private int _length;
        [SerializeField, Min(0)] private float _segmentLength;
        [SerializeField, Min(0)] private float _smoothTime;
        [SerializeField] private bool _useSmoothTimeCurve;
        [SerializeField] private AnimationCurve _smoothTimeCurve = AnimationCurve.Constant(0, 1 , 1);
        [Header("Misc")]
        [SerializeField] private bool _onUpdate;
        
        private Vector3[] _segmentPositions;
        private Vector3[] _segmentVelocities;

        private void Awake()
        {
            _segmentPositions = new Vector3[_length];
            _segmentVelocities = new Vector3[_length];
        }

        private void Start()
        {
            SetStraight(_targetDirection.right);
        }

        private void Update()
        {
            if (!_onUpdate) return;
            Solve(_targetDirection.right);
        }

        public void Solve(Vector3 targetDirection)
        {
            _segmentPositions[0] = _targetDirection.position;

            for (int i = 1; i < _segmentPositions.Length; i++)
            {
                Vector3 targetPos = !_useDirectionlessChain ? 
                    _segmentPositions[i - 1] + targetDirection * _segmentLength : 
                    _segmentPositions[i - 1] + (_segmentPositions[i] - _segmentPositions[i - 1]).normalized * _segmentLength;
                
                float time = _useSmoothTimeCurve ? _smoothTimeCurve.Evaluate((float) i / Length) : _smoothTime;
                _segmentPositions[i] = Vector3.SmoothDamp(_segmentPositions[i], targetPos, ref _segmentVelocities[i],
                    time);
            }
            
            OnSolveStepEvent?.Invoke(_segmentPositions);
        }
        
        //TODO call this in inspector to setup lineRenderer
        private void SetStraight(Vector3 targetDirection)
        {
            _segmentPositions[0] = _targetDirection.position;
            Vector3 offset = targetDirection * _segmentLength;

            for (int i = 1; i < _segmentPositions.Length; i++)
            {
                _segmentPositions[i] = i * offset;
            }
            
            OnSolveStepEvent?.Invoke(_segmentPositions);
        }
    }
}
