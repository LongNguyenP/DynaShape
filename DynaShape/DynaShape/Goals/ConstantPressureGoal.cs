﻿using System.Collections.Generic;
using Autodesk.DesignScript.Runtime;


namespace DynaShape.Goals
{
    [IsVisibleInDynamoLibrary(false)]
    public class ConstantPressureGoal : Goal
    {
        public float Pressure;
        private int id;
        private static Random rand = new Random();

        public ConstantPressureGoal()
        {
            id = rand.Next(9999);
        }


        public ConstantPressureGoal(Triple nodePosition1, Triple nodePosition2, Triple nodePosition3, float pressure, float weight = 1000f)
        {
            Initialize(nodePosition1, nodePosition2, nodePosition3, pressure, weight);
        }


        public void Initialize(Triple nodePosition1, Triple nodePosition2, Triple nodePosition3, float pressure, float weight)
        {
            Weight = weight;
            Pressure = pressure;
            StartingPositions = new[] { nodePosition1, nodePosition2, nodePosition3 };
            Moves = new Triple[3];
            Weights = new float[3];
        }


        internal override void Compute(List<Node> allNodes)
        {
            Triple n = (allNodes[NodeIndices[1]].Position - allNodes[NodeIndices[0]].Position)
                .Cross(allNodes[NodeIndices[2]].Position - allNodes[NodeIndices[0]].Position);

            Moves[0] = Moves[1] = Moves[2] = n * Pressure * 0.16666666666666666666666f;
            Weights[0] = Weights[1] = Weights[2] = Weight;
        }


        public override string ToString()
        {
            return "ConstantPressureGoal " + id;
        }
    }
}
