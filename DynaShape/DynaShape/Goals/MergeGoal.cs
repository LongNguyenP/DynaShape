﻿using System.Collections.Generic;
using Autodesk.DesignScript.Runtime;


namespace DynaShape.Goals
{
    [IsVisibleInDynamoLibrary(false)]
    public class MergeGoal : Goal
    {
        public MergeGoal()
        {
        }


        public MergeGoal(List<Triple> nodeStartingPositions, float weight = 1000f)
        {
            Initialize(nodeStartingPositions, weight);
        }


        public void Initialize(List<Triple> nodeStartingPositions, float weight)
        {
            Weight = weight;
            StartingPositions = nodeStartingPositions.ToArray();
            Moves = new Triple[StartingPositions.Length];
            Weights = new float[StartingPositions.Length];
        }


        internal override void Compute(List<Node> allNodes)
        {
            Triple center = Triple.Zero;
            for (int i = 0; i < NodeCount; i++) center += allNodes[NodeIndices[i]].Position;
            center /= NodeCount;

            for (int i = 0; i < NodeCount; i++)
            {
                Moves[i] = center - allNodes[NodeIndices[i]].Position;
                Weights[i] = Weight;
            }
        }
    }
}
