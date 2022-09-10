using UnityEngine;

namespace IkTentacle
{
    public class IkChainLineRenderedView : MonoBehaviour
    {
        [SerializeField] private IkChainFollowSolver _solver;
        [SerializeField] private LineRenderer _line;

        private void Awake()
        {
            _solver ??= GetComponent<IkChainFollowSolver>();
            _line ??= GetComponent<LineRenderer>();

            _line.positionCount = _solver.Length;
            _solver.OnSolveStepEvent += OnSolverStep;
        }

        private void OnSolverStep(Vector3[] points)
        {
            _line.SetPositions(points);
        }
    }
}
