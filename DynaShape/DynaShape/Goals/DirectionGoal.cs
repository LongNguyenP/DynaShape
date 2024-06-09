using System.Collections.Generic;
using Autodesk.DesignScript.Runtime;


namespace DynaShape.Goals
{
    [IsVisibleInDynamoLibrary(false)]
    public class DirectionGoal : Goal
    {
        public Triple TargetDirection;


        public DirectionGoal()
        {
        }


        public DirectionGoal(Triple firstNodePosition, Triple secondNodePosition, Triple targetDirection, float weight = 1f)
        {
            Initialize(firstNodePosition, secondNodePosition, targetDirection, weight);
        }


        public DirectionGoal(Triple firstNodePosition, Triple secondNodePosition, float weight = 1f)
            : this(firstNodePosition, secondNodePosition, secondNodePosition - firstNodePosition, weight)
        {
        }


        private void Initialize(Triple firstNodePosition, Triple secondNodePosition, Triple targetDirection, float weight)
        {
            TargetDirection = targetDirection.Normalise();
            Weight = weight;
            StartingPositions = new[] { firstNodePosition, secondNodePosition };
            Moves = new Triple[2];
            Weights = new float[2];
        }


        internal override void Compute(List<Node> allNodes)
        {
            Triple v = allNodes[NodeIndices[1]].Position - allNodes[NodeIndices[0]].Position;
            Moves[0] = 0.5f * (v - v.Dot(TargetDirection) * TargetDirection);
            Moves[1] = -Moves[0];
            Weights[0] = Weights[1] = Weight;
        }
    }
}
