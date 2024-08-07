﻿using System;
using System.Collections.Generic;
using Autodesk.DesignScript.Runtime;


namespace DynaShape.Goals
{
    [IsVisibleInDynamoLibrary(false)]
    public class CoPlanarGoal : Goal
    {
        public CoPlanarGoal()
        {
        }


        public CoPlanarGoal(List<Triple> nodeStartingPositions, float weight = 1f)
        {
            Initialize(nodeStartingPositions, weight);
        }


        public void Initialize(List<Triple> nodeStartingPositions, float weight)
        {
            if (nodeStartingPositions.Count < 4) throw new Exception("CoPlanar Goal: Node count must be at least 4");
            Weight = weight;
            StartingPositions = nodeStartingPositions.ToArray();
            Moves = new Triple[StartingPositions.Length];
            Weights = new float[StartingPositions.Length];
        }


        internal override void Compute(List<Node> allNodes)
        {
            List<Triple> points = new List<Triple>(NodeCount);
            for (int i = 0; i < NodeCount; i++)
                points.Add(allNodes[NodeIndices[i]].Position);

            // Here we use our own plane fitting function (implemented in the Util class)
            // .. which runs much faster than calling the DynamoAPI method Plane.ByBestFitThroughPoints()

            Util.ComputeBestFitPlane(points, out Triple planeOrigin, out Triple planeNormal);

            for (int i = 0; i < NodeCount; i++)
            {
                Moves[i] = (planeOrigin - allNodes[NodeIndices[i]].Position).Dot(planeNormal) * planeNormal;
                Weights[i] = Weight;
            }
        }
    }
}
