﻿using System.Collections.Generic;
using Autodesk.DesignScript.Runtime;


namespace DynaShape.Goals
{
    [IsVisibleInDynamoLibrary(false)]
    public class FloorGoal : Goal
    {
        public float FloorHeight;


        public FloorGoal()
        {
        }


        public FloorGoal(List<Triple> nodeStartingPositions, float floorHeight = 0f, float weight = 1000f)
        {
            Initialize(nodeStartingPositions, floorHeight, weight);
        }


        public void Initialize(List<Triple> nodeStartingPositions, float floorHeight, float weight)
        {
            Weight = weight;
            FloorHeight = floorHeight;
            StartingPositions = nodeStartingPositions.ToArray();
            Moves = new Triple[nodeStartingPositions.Count];
            Weights = new float[nodeStartingPositions.Count];
        }


        internal override void Compute(List<Node> allNodes)
        {
            // TODO: The math is quite unstable
            for (int i = 0; i < NodeCount; i++)
            {
                Moves[i] = new Triple(0f, 0f, allNodes[i].Position.Z > FloorHeight ? 0f : FloorHeight - allNodes[i].Position.Z);
                Weights[i] = allNodes[i].Position.Z > FloorHeight ? 0f : Weight;
            }
        }
    }
}
