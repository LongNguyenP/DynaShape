using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// OnPlaneGoal
    /// </summary>
    public  class OnPlaneGoal
    {
        private OnPlaneGoal(){}

        /// <summary>
        /// Creates an OnPlaneGoal to force a set of nodes to lie on the specified plane.
        /// This differs from the CoPlanar goal, where the target plane is computed based on the current node positions rather than being defined and fixed in advance.
        /// </summary>
        /// <param name="startPositions"></param>
        /// <param name="targetPlaneOrigin">The origin of the plane to use.</param>
        /// <param name="targetPlaneNormal">The normal of the plane to use.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="OnPlaneGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.OnPlaneGoal Create(
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
        /// Creates an OnPlaneGoal to force a set of nodes to lie on the specified plane.
        /// This differs from the CoPlanar goal, where the target plane is computed based on the current node positions rather than being defined and fixed in advance.
        /// </summary>
        /// <param name="startPositions">The start positions of the nodes.</param>
        /// <param name="targetPlane">The target plane to use for the nodes.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="OnPlaneGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.OnPlaneGoal Create(
            List<Point> startPositions,
            [DefaultArgument("Plane.ByOriginNormal(Point.Origin(), Vector.ZAxis())")] Plane targetPlane,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.OnPlaneGoal(startPositions.ToTriples(), targetPlane.Origin.ToTriple(),
                targetPlane.Normal.ToTriple(), weight);
        }


        /// <summary>
        /// Modifies the OnPlaneGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="onPlaneGoal">The OnPlaneGoal to modify.</param>
        /// <param name="targetPlaneOrigin">An optional new origin of the target plane to use for the OnPlaneGoal.</param>
        /// <param name="targetPlaneNormal">An optional new normal of the target plane to use for the OnPlaneGoal.</param>
        /// <param name="weight">An optional new weight for the OnPlaneGoal.</param>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.OnPlaneGoal Change(
           DynaShape.Goals.OnPlaneGoal onPlaneGoal,
            [DefaultArgument("null")] Point targetPlaneOrigin,
            [DefaultArgument("null")] Vector targetPlaneNormal,
            [DefaultArgument("-1.0")] float weight)
        {
            if (targetPlaneOrigin != null) onPlaneGoal.TargetPlaneOrigin = targetPlaneOrigin.ToTriple();
            if (targetPlaneNormal != null) onPlaneGoal.TargetPlaneNormal = targetPlaneNormal.ToTriple();
            if (weight >= 0.0) onPlaneGoal.Weight = weight;
            return onPlaneGoal;
        }


        /// <summary>
        /// Modifies the OnPlaneGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="onPlaneGoal">The OnPlaneGoal to modify.</param>
        /// <param name="targetPlane">An optional new target plane to use for the OnPlaneGoal..</param>
        /// <param name="weight">An optional new weight for the OnPlaneGoal.</param>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.OnPlaneGoal Change(
            DynaShape.Goals.OnPlaneGoal onPlaneGoal,
            [DefaultArgument("null")] Plane targetPlane,
            [DefaultArgument("-1.0")] float weight)
        {
            if (targetPlane != null)
            {
                onPlaneGoal.TargetPlaneOrigin = targetPlane.Origin.ToTriple();
                onPlaneGoal.TargetPlaneNormal = targetPlane.Normal.ToTriple();
            }
            if (weight >= 0.0) onPlaneGoal.Weight = weight;
            return onPlaneGoal;
        }
    }
}
