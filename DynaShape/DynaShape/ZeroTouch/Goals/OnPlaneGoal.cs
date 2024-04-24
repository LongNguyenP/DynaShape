using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// OnPlaneGoal
    /// </summary>
    public  class OnPlaneGoal
    {
        private OnPlaneGoal(){}

        /// <summary>
        /// Force a set of nodes to lie on the specified plane.
        /// This is different from other CoPlanar goal, where the target plane is computed based on the current node positions rather than being defined and fixed in advance.
        /// </summary>
        /// <param name="startPositions"></param>
        /// <param name="targetPlaneOrigin"></param>
        /// <param name="targetPlaneNormal"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.OnPlaneGoal OnPlaneGoal_Create(
            List<Point> startPositions,
            [DefaultArgument("Point.Origin()")] Point targetPlaneOrigin,
            [DefaultArgument("Vector.ZAxis()")] Vector targetPlaneNormal,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.OnPlaneGoal(
                startPositions.ToTriples(),
                targetPlaneOrigin.ToTriple(),
                targetPlaneNormal.ToTriple(),
                weight);
        }

        /// <summary>
        /// Force a set of nodes to lie on the specified plane.
        /// This is different from other CoPlanar goal, where the target plane is computed based on the current node positions rather than being defined and fixed in advance.
        /// </summary>
        /// <param name="startPositions"></param>
        /// <param name="targetPlane"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.OnPlaneGoal OnPlaneGoal_Create(
            List<Point> startPositions,
            [DefaultArgument("Plane.ByOriginNormal(Point.Origin(), Vector.ZAxis())")] Plane targetPlane,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.OnPlaneGoal(startPositions.ToTriples(), targetPlane.Origin.ToTriple(),
                targetPlane.Normal.ToTriple(), weight);
        }


        /// <summary>
        /// Adjust the goal's parameters while the solver is running.
        /// </summary>
        /// <param name="goal"></param>
        /// <param name="targetPlaneOrigin"></param>
        /// <param name="targetPlaneNormal"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.OnPlaneGoal OnPlaneGoal_Change(
           DynaShape.Goals.OnPlaneGoal goal,
            [DefaultArgument("null")] Point targetPlaneOrigin,
            [DefaultArgument("null")] Vector targetPlaneNormal,
            [DefaultArgument("-1.0")] float weight)
        {
            if (targetPlaneOrigin != null) goal.TargetPlaneOrigin = targetPlaneOrigin.ToTriple();
            if (targetPlaneNormal != null) goal.TargetPlaneNormal = targetPlaneNormal.ToTriple();
            if (weight >= 0.0) goal.Weight = weight;
            return goal;
        }


        /// <summary>
        /// Adjust the goal's parameters while the solver is running.
        /// </summary>
        /// <param name="goal"></param>
        /// <param name="targetPlane"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.OnPlaneGoal OnPlaneGoal_Change(
            DynaShape.Goals.OnPlaneGoal goal,
            [DefaultArgument("null")] Plane targetPlane,
            [DefaultArgument("-1.0")] float weight)
        {
            if (targetPlane != null)
            {
                goal.TargetPlaneOrigin = targetPlane.Origin.ToTriple();
                goal.TargetPlaneNormal = targetPlane.Normal.ToTriple();
            }
            if (weight >= 0.0) goal.Weight = weight;
            return goal;
        }
    }
}
