﻿using System;
using System.Collections.Generic;
using Autodesk.DesignScript.Runtime;


namespace DynaShape.Goals
{
    [IsVisibleInDynamoLibrary(false)]
    public class CoCircularGoal : Goal
    {
        public CoCircularGoal()
        {
        }


        public CoCircularGoal(List<Triple> nodeStartingPositions, float weight = 1f)
        {
            Initialize(nodeStartingPositions, weight);
        }


        public void Initialize(List<Triple> nodeStartingPositions, float weight = 1f)
        {
            if (nodeStartingPositions.Count < 4) throw new Exception("CoCircular Goal: Node count must be at least 4");
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

            // Here we use our own circle fitting function (implemented in the Util class)
            // .. which runs much faster than calling the DynamoAPI method Circle.ByBestFitThroughPoints()

            if (Util.ComputeBestFitCircle(points, out Triple c, out Triple n, out float r))
                for (int i = 0; i < NodeCount; i++)
                {
                    Triple d = allNodes[NodeIndices[i]].Position - c;
                    Moves[i] = (d - d.Dot(n) * n).Normalise() * r - d;
                    Weights[i] = Weight;
                    if (float.IsNaN(Moves[i].X))
                        throw new Exception("Damn!!!");
                }
            else
            {
                Moves.FillArray(Triple.Zero);
                Weights.FillArray(Weight);
            }
        }
    }
}
