﻿using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// Anchor goal to simulate pinning down an object at points.
    /// </summary>
    public class AngleGoal
    {
        private AngleGoal(){}


        /// <summary>
        /// Creates an Angle Goal that attempts to keep the angle formed by three nodes at a target value.
        /// </summary>
        /// <param name="startPositionA">The first point that defines the angle</param>
        /// <param name="startPositionB">The second point that defines the angle</param>
        /// <param name="startPositionC">The third point that defines the angle</param>
        /// <param name="targetAngle">The angle to attempt to maintain</param>
        /// <param name="weight">The goal's weight/impact on the solver</param>
        [NodeCategory("Create")]
        public static DynaShape.Goals.AngleGoal ByPointsTargetAngle(
            Point startPositionA,
            Point startPositionB,
            Point startPositionC,
            [DefaultArgument("0.0")] float targetAngle,
            [DefaultArgument("1.0")] float weight)
        {
            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.AngleGoal>();
            goal.Initialize(startPositionA.ToTriple(),
                startPositionB.ToTriple(),
                startPositionC.ToTriple(),
                targetAngle.ToRadian(),
                weight);
            return goal;
        }


        /// <summary>
        /// Creates an Angle Goal that attempts to keep the angle formed by three nodes
        /// </summary>
        /// <param name="a">The first node to maintain.</param>
        /// <param name="b">The second node to maintain.</param>
        /// <param name="c">The third node to maintain.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        [NodeCategory("Create")]
        public static DynaShape.Goals.AngleGoal ByPoints(
            Point a,
            Point b,
            Point c,
            [DefaultArgument("1.0")] float weight)
        {
            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.AngleGoal>();
            goal.Initialize(a.ToTriple(),
                b.ToTriple(),
                c.ToTriple(),
                weight);
            return goal;
        }
    }
}
