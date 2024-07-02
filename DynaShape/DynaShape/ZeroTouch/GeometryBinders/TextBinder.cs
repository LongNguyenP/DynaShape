using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using DSCore;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.GeometryBinders;

/// <summary>
/// Wrapper class for TextBinder
/// </summary>
public class TextBinder
{
    private TextBinder(){}
    /// <summary>
    /// Create a TextBinder.
    /// </summary>
    /// <param name="position">The location of the text.</param>
    /// <param name="text">The string value of the text.</param>
    /// <param name="color">An optional color to preview the circle geometry.</param>
    /// <returns name="textBinder"></returns>
    [NodeCategory("Create")]
    public static DynaShape.GeometryBinders.TextBinder ByPointText(
        Point position,
        string text,
        [DefaultArgument("null")] Color color)
    {
        var binder = TracingUtils.GetObjectFromTrace<DynaShape.GeometryBinders.TextBinder>();

        binder.Initialize(position.ToTriple(), text, color?.ToColor4() ?? DynaShapeDisplay.DefaultLineColor);

        return binder;
    }
}