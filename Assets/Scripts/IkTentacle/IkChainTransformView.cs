using System.Collections.Generic;
using UnityEngine;

namespace IkTentacle
{
    public class IkChainTransformView : MonoBehaviour
    {
        [SerializeField] private IkChainFollowSolver _solver;
        [SerializeField] private List<Transform> _segments;
        //TODO [SerializeField] private bool _ignoreHeadSegment;
        
        private void Awake()
        {
            _solver ??= GetComponent<IkChainFollowSolver>();
            _solver.OnSolveStepEvent += OnSolverStep;
        }

        private void OnSolverStep(Vector3[] points)
        {
            for (int i = 0; i < _segments.Count; i++)
            {
                if (_segments[i] == null) continue;
                _segments[i].position = points[i];

                if (i > 0)
                {
                    _segments[i].right = points[i] - points[i - 1];
                }
            }
        }

        private void OnValidate()
        {
            if (_solver == null) return;
            if (_segments.Count < _solver.Length)
            {
                for (int i = _segments.Count; i < _solver.Length; i++)
                    _segments.Add(null);
            }
            
            if (_segments.Count > _solver.Length)
            {
                _segments.RemoveRange(_solver.Length, _segments.Count - _solver.Length);
            }
        }
    }
}
