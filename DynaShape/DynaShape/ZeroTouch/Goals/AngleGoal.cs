using Autodesk.DesignScript.Runtime;
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
        /// <param name="a">The first node to maintain.</param>
        /// <param name="b">The second node to maintain.</param>
        /// <param name="c">The third node to maintain.</param>
        /// <param name="targetAngle">The angle to attempt to maintain.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="AngleGoal">A newly defined AngleGoal.</returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.AngleGoal Create(
            Point a,
            Point b,
            Point c,
            [DefaultArgument("0.0")] float targetAngle,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.AngleGoal(
                a.ToTriple(),
                b.ToTriple(),
                c.ToTriple(),
                targetAngle.ToRadian(),
                weight);
        }


        /// <summary>
        /// Creates an Angle Goal that attempts to keep the angle formed by three nodes
        /// </summary>
        /// <param name="a">The first node to maintain.</param>
        /// <param name="b">The second node to maintain.</param>
        /// <param name="c">The third node to maintain.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="AngleGoal">A newly defined AngleGoal</returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.AngleGoal Create(
            Point a,
            Point b,
            Point c,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.AngleGoal(
                a.ToTriple(),
                b.ToTriple(),
                c.ToTriple(),
                weight);
        }

        /// <summary>
        /// Modifies the AngleGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="angleGoal">An AngleGoal to modify with the given parameters.</param>
        /// <param name="targetAngle">The new target angle for the AngleGoal.</param>
        /// <param name="weight">An optional new weight for the AngleGoal.</param>
        /// <returns name="angleGoal">The modified AngleGoal.</returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.AngleGoal Change(
            DynaShape.Goals.AngleGoal angleGoal,
            float targetAngle,
            [DefaultArgument("-1.0")] float weight)
        {
            angleGoal.TargetAngle = targetAngle.ToRadian();
            if (weight >= 0.0) angleGoal.Weight = weight;
            return angleGoal;
        }
    }
}
