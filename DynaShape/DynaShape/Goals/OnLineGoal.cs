﻿using System.Collections.Generic;
using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;


namespace DynaShape.Goals
{
    [IsVisibleInDynamoLibrary(false)]
    public class OnLineGoal : Goal
    {
        public Triple TargetLineOrigin;
        public Triple TargetLineDirection;


        public OnLineGoal()
        {
        }


        public OnLineGoal(List<Triple> nodeStartingPositions, Triple lineOrigin, Triple lineDirection, float weight = 1f)
        {
            Initialize(nodeStartingPositions, lineOrigin, lineDirection, weight);
        }


        public OnLineGoal(List<Triple> nodeStartingPositions, Line line, float weight = 1f)
            : this(
                  nodeStartingPositions,
                  line.StartPoint.ToTriple(),
                  (line.EndPoint.ToTriple() - line.StartPoint.ToTriple()).Normalise(),
                  weight)
        {
        }


        public void Initialize(List<Triple> nodeStartingPositions, Triple lineOrigin, Triple lineDirection, float weight)
        {
            TargetLineOrigin = lineOrigin;
            TargetLineDirection = lineDirection;
            Weight = weight;
            StartingPositions = nodeStartingPositions.ToArray();
            Moves = new Triple[StartingPositions.Length];
            Weights = new float[StartingPositions.Length];
        }


        internal override void Compute(List<Node> allNodes)
        {
            for (int i = 0; i < NodeCount; i++)
            {
                Triple v = allNodes[NodeIndices[i]].Position - TargetLineOrigin;
                Moves[i] = v.Dot(TargetLineDirection) * TargetLineDirection - v;
                Weights[i] = Weight;
            }
        }
    }
}
