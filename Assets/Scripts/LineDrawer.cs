using UnityEngine;

public class LineDrawer : MonoBehaviour
{

    public GameObject linePrefab;
    public LayerMask cantDrawLayer;
    int cantDrawLayerIndex;

    [Space(10)]
    public float linePointsMinDistance;
    public float lineWidth;
    public int pCount;
    public Gradient lineColor;

    Line currentLine;

    [Space(10)]
    public bool lineIsDrawn;

    [Space(10)]
    public BeeSpawn BeeSpawn;
    public GameController GameControl;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !lineIsDrawn)
            BeginDraw();

        if (currentLine != null && !lineIsDrawn)
            Draw();

        if (Input.GetMouseButtonUp(0) && !lineIsDrawn)
            EndDraw();
    }

    private void Start()
    {
        cantDrawLayerIndex = LayerMask.NameToLayer("Can't Draw");
    }

    private void BeginDraw()
    {
        currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();

        currentLine.SetLineColor(lineColor);
        currentLine.SetLineWidth(lineWidth);
        currentLine.SetPointMinDistance(linePointsMinDistance);
    }

    private void Draw()
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.CircleCast(MousePos, lineWidth / 3f, Vector2.zero, 1f, cantDrawLayer);

        if (!hit && GameControl.maxPoints >= currentLine.pointsCount)
            currentLine.AddPoint(MousePos);
        pCount = currentLine.pointsCount;

    }

    private void EndDraw()
    {
        if (currentLine != null)
        {
            if (currentLine.pointsCount < 2)
            {
                Destroy(currentLine.gameObject);
            }
            else
            {
                currentLine.gameObject.layer = cantDrawLayerIndex;
                currentLine.UsePhysics(true);
                currentLine = null;
                BeeSpawn.isEnabled = true;
                lineIsDrawn = true;
            }
        }
    }
}
