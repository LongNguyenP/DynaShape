﻿using System;
using System.Collections.Generic;
using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;


namespace DynaShape.Goals
{
    [IsVisibleInDynamoLibrary(false)]
    public class OnSurfaceGoal : Goal
    {
        public Surface TargetSurface;


        public OnSurfaceGoal()
        {
        }


        public OnSurfaceGoal(List<Triple> nodeStartingPositions, Surface surface, float weight = 1f)
        {
            Initialize(nodeStartingPositions, surface, weight);
        }


        public void Initialize(List<Triple> nodeStartingPositions, Surface surface, float weight)
        {
            TargetSurface = surface;

            Weight = weight;
            StartingPositions = nodeStartingPositions.ToArray();
            Moves = new Triple[StartingPositions.Length];
            Weights = new float[StartingPositions.Length];
        }


        internal override void Compute(List<Node> allNodes)
        {
            if (TargetSurface == null) throw new Exception("OnSurfaceGoal: The target surface has not been set");
            for (int i = 0; i < NodeCount; i++)
            {
                Triple nodePosition = allNodes[NodeIndices[i]].Position;
                Moves[i] = TargetSurface.ClosestPointTo(nodePosition.ToPoint()).ToTriple() - nodePosition;
                Weights[i] = Weight;
            }
        }
    }
}
