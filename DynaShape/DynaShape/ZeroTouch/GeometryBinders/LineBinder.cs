using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using DSCore;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.GeometryBinders;

/// <summary>
/// Wrapper class for LineBinder
/// </summary>
public class LineBinder
{
    private LineBinder(){}


    /// <summary>
    /// Create a LineBinder.
    /// </summary>
    /// <param name="line">The line to generate a preview for.</param>
    /// <param name="color">An optional color to preview the circle geometry.</param>
    /// <returns name="lineBinder"></returns>
    [NodeCategory("Create")]
    public static DynaShape.GeometryBinders.LineBinder ByLine(
        Line line,
        [DefaultArgument("null")] Color color)
    {
        var binder = TracingUtils.GetObjectFromTrace<DynaShape.GeometryBinders.LineBinder>();

        binder.Initialize(
            line.StartPoint.ToTriple(),
            line.EndPoint.ToTriple(),
            color?.ToColor4() ?? DynaShapeDisplay.DefaultLineColor);

        return binder;
    }


    /// <summary>
    /// Create a LineBinder.\\
    /// </summary>
    /// <param name="startPoint">The start point of the line.</param>
    /// <param name="endPoint">The end point of the line.</param>
    /// <param name="color">An optional color to preview the circle geometry.</param>
    /// <returns name="lineBinder"></returns>
    [NodeCategory("Create")]
    public static DynaShape.GeometryBinders.LineBinder ByStartPointEndPoint(
        Point startPoint,
        Point endPoint,
        [DefaultArgument("null")] Color color)
    {
        var binder = TracingUtils.GetObjectFromTrace<DynaShape.GeometryBinders.LineBinder>();

        binder.Initialize(
            startPoint.ToTriple(),
            endPoint.ToTriple(),
            color?.ToColor4() ?? DynaShapeDisplay.DefaultLineColor);

        return binder;
    }
}