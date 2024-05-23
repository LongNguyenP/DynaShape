using Autodesk.DesignScript.Runtime;
using DynamoServices;
using DynaShape.Goals;

namespace DynaShape;

[IsVisibleInDynamoLibrary(false)]
internal class TracingUtils
{
    private const string TraceID = "{0459D869-0C72-447F-96D8-08A7FB92214B}-REVIT";
    private static int id = 0;
    private static Dictionary<int, Goal> traceableObjectDictionary = new Dictionary<int, Goal>();

    public static T GetObjectFromTrace<T>() where T : Goal, new()
    {
        T item;
        // See if there is data for this object is in trace.
        var traceId = TracingUtils.GetObjectIdFromTrace();

        int id;
        if (traceId == -1)
        {
            // If there's no id stored in trace for this object,
            // then grab the next unused trace id.
            id = TracingUtils.GetNextUnusedID();

            // Create an item
            item = new T();

            // Remember to store the updated object in the trace object manager,
            // so it's available to use the next time around.
            TracingUtils.RegisterTraceableObjectForId(id, item);
        }
        else
        {
            // If there's and id stored in trace, then retrieve the object stored
            // with that id from the trace object manager.
            item = (T)TracingUtils.GetTracedObjectById(traceId)
                ?? new T();
        }

        return item;
    }

    private static int GetNextUnusedID()
    {
        var next = id;
        id++;
        return next;
    }

    private static int GetObjectIdFromTrace()
    {
        var id = TraceUtils.GetTraceData(TraceID);
        return string.IsNullOrEmpty(id) ? -1 : int.Parse(id);
    }

    private static Goal GetTracedObjectById(int id)
    {
        Goal ret;
        traceableObjectDictionary.TryGetValue(id, out ret);
        return ret;
    }

    private static void RegisterTraceableObjectForId(int id, Goal objectToTrace)
    {
        if (traceableObjectDictionary.ContainsKey(id))
        {
            traceableObjectDictionary[id] = objectToTrace;
        }
        else
        {
            traceableObjectDictionary.Add(id, objectToTrace);

            TraceUtils.SetTraceData(TraceID, id.ToString());
        }
    }

    private static void Clear()
    {
        traceableObjectDictionary.Clear();
        id = 0;
    }
}