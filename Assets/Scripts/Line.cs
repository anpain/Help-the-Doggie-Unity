using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public Rigidbody2D rb;

    [HideInInspector] public List<Vector2> points = new List<Vector2>();
    [HideInInspector] public int pointsCount = 0;

    private float pointMinDistance = 0.1f;
    private float circleColiderRaduis;

    public Vector2 GetLastPoint()
    {
        return (Vector2)lineRenderer.GetPosition(pointsCount - 1);
    }

    public void UsePhysics(bool usePhysic)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void SetLineColor(Gradient LineColor)
    {
        lineRenderer.colorGradient = LineColor;
    }

    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        edgeCollider.edgeRadius = width / 2;
        circleColiderRaduis = width / 2;
    }

    public void AddPoint(Vector2 newPoint)
    {
        if (pointsCount >= 1 && Vector2.Distance(newPoint, GetLastPoint()) < pointMinDistance)
            return;

        if (pointsCount >= 1 && Vector2.Distance(newPoint, GetLastPoint()) >= pointMinDistance * 5)
        {
            return;
        }
        else
        {
            points.Add(newPoint);
            pointsCount++;

            CircleCollider2D circleColider = this.gameObject.AddComponent<CircleCollider2D>();
            circleColider.offset = newPoint;
            circleColider.radius = circleColiderRaduis;

            lineRenderer.positionCount = pointsCount;
            lineRenderer.SetPosition(pointsCount - 1, newPoint);

            if (pointsCount > 1)
                edgeCollider.points = points.ToArray();

        }

    }

    public void SetPointMinDistance(float distance)
    {
        pointMinDistance = distance;
    }

}
