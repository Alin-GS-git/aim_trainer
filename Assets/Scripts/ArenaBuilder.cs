using UnityEngine;

[ExecuteInEditMode]
public class ArenaBuilder : MonoBehaviour
{
    [Header("Arena Size (match TargetBounds BoxCollider)")]
    public float width  = 20f;
    public float depth  = 20f;
    public float height = 8f;

    [Header("Colors")]
    public Color surfaceColor = new Color(0.45f, 0.58f, 0.68f); // muted blue-grey
    public Color gridColor    = new Color(0.18f, 0.22f, 0.27f); // dark charcoal lines
    public Color lightColor   = new Color(0.9f, 0.95f, 1f);

    [Header("Grid")]
    public float gridSpacing   = 2f;
    public float gridLineWidth = 0.06f;

    [Header("Lighting")]
    public float ambientBrightness   = 0.4f;
    public float pointLightIntensity = 2.5f;

    GameObject arenaRoot;

    void Start()
    {
        // Find existing _Arena built in Edit Mode — don't rebuild on Play
        Transform existing = transform.Find("_Arena");
        if (existing != null)
        {
            arenaRoot = existing.gameObject;
            return;
        }

        Build();
    }

    [ContextMenu("Rebuild Arena")]
    public void Build()
    {
        if (arenaRoot != null)
            DestroyImmediate(arenaRoot);

        arenaRoot = new GameObject("_Arena");
        arenaRoot.transform.SetParent(transform);

        float hw = width  / 2f;
        float hd = depth  / 2f;
        float t  = 0.2f;

        // ── Panels ────────────────────────────────────────────────────────────
        MakePanel("Floor",      new Vector3(0,           -t/2f,        0),   new Vector3(width, t,      depth ));
        MakePanel("Ceiling",    new Vector3(0,            height+t/2f, 0),   new Vector3(width, t,      depth ));
        MakePanel("Wall_Front", new Vector3(0,            height/2f,   hd+t/2f), new Vector3(width, height, t));
        MakePanel("Wall_Back",  new Vector3(0,            height/2f,  -hd-t/2f), new Vector3(width, height, t));
        MakePanel("Wall_Left",  new Vector3(-hw-t/2f,     height/2f,   0),   new Vector3(t,     height, depth));
        MakePanel("Wall_Right", new Vector3( hw+t/2f,     height/2f,   0),   new Vector3(t,     height, depth));

        // ── Grid on ALL surfaces ──────────────────────────────────────────────
        float o = 0.013f; // surface offset so grid floats above panel

        BuildGrid("Grid_Floor",   new Vector3(0, o,          0),        GridPlane.XZ, width,  depth );
        BuildGrid("Grid_Ceiling", new Vector3(0, height - o, 0),        GridPlane.XZ, width,  depth );
        BuildGrid("Grid_Front",   new Vector3(0, height/2f,  hd - o),   GridPlane.XY, width,  height);
        BuildGrid("Grid_Back",    new Vector3(0, height/2f, -hd + o),   GridPlane.XY, width,  height);
        BuildGrid("Grid_Left",    new Vector3(-hw + o, height/2f, 0),   GridPlane.ZY, depth,  height);
        BuildGrid("Grid_Right",   new Vector3( hw - o, height/2f, 0),   GridPlane.ZY, depth,  height);

        SetupLighting();
        BuildPointLights(hw, hd);
    }

    // ── Panel ─────────────────────────────────────────────────────────────────
    void MakePanel(string name, Vector3 pos, Vector3 scale)
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = name;
        go.transform.SetParent(arenaRoot.transform);
        go.transform.localPosition = pos;
        go.transform.localScale    = scale;
        go.GetComponent<Renderer>().material = MakeMat(surfaceColor, 0.15f);
        go.isStatic = true;
    }

    // ── Grid ──────────────────────────────────────────────────────────────────
    enum GridPlane { XZ, XY, ZY }

    void BuildGrid(string label, Vector3 origin, GridPlane plane, float spanA, float spanB)
    {
        var root = new GameObject(label);
        root.transform.SetParent(arenaRoot.transform);
        var mat   = MakeMat(gridColor, 0f);
        float lw  = gridLineWidth;
        float th  = 0.008f;
        int   cA  = Mathf.RoundToInt(spanA / gridSpacing);
        int   cB  = Mathf.RoundToInt(spanB / gridSpacing);

        for (int i = 0; i <= cA; i++)
        {
            float a = -spanA/2f + i * gridSpacing;
            Vector3 p, s;
            switch (plane)
            {
                case GridPlane.XZ: p = origin + new Vector3(a, 0, 0);           s = new Vector3(lw,   th,   spanB); break;
                case GridPlane.XY: p = origin + new Vector3(a, 0, 0);           s = new Vector3(lw,   spanB, th  ); break;
                default:           p = origin + new Vector3(0,  0, a);           s = new Vector3(th,   spanB, lw  ); break;
            }
            SpawnLine(root.transform, label + "_A" + i, p, s, mat);
        }

        for (int i = 0; i <= cB; i++)
        {
            float b = -spanB/2f + i * gridSpacing;
            Vector3 p, s;
            switch (plane)
            {
                case GridPlane.XZ: p = origin + new Vector3(0, 0, b);           s = new Vector3(spanA, th,   lw  ); break;
                case GridPlane.XY: p = origin + new Vector3(0, b, 0);           s = new Vector3(spanA, lw,   th  ); break;
                default:           p = origin + new Vector3(0, b, 0);           s = new Vector3(th,    lw,   spanA); break;
            }
            SpawnLine(root.transform, label + "_B" + i, p, s, mat);
        }
    }

    void SpawnLine(Transform parent, string name, Vector3 pos, Vector3 scale, Material mat)
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = name;
        go.transform.SetParent(parent);
        go.transform.localPosition = pos;
        go.transform.localScale    = scale;
        go.GetComponent<Renderer>().material = mat;
        DestroyImmediate(go.GetComponent<Collider>());
        go.isStatic = true;
    }

    // ── Material ──────────────────────────────────────────────────────────────
    Material MakeMat(Color color, float smoothness)
    {
        Shader shader = Shader.Find("Universal Render Pipeline/Lit")
                     ?? Shader.Find("Standard")
                     ?? Shader.Find("Legacy Shaders/Diffuse");

        if (shader == null) return null;

        var mat = new Material(shader);
        mat.color = color;
        if (mat.HasProperty("_Smoothness")) mat.SetFloat("_Smoothness", smoothness);
        if (mat.HasProperty("_Glossiness")) mat.SetFloat("_Glossiness", smoothness);
        if (mat.HasProperty("_Cull"))       mat.SetFloat("_Cull", 0); // double-sided
        return mat;
    }

    // ── Lighting ──────────────────────────────────────────────────────────────
    void SetupLighting()
    {
        RenderSettings.ambientMode  = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = new Color(0.20f, 0.25f, 0.32f) * ambientBrightness;

        var dir = FindFirstObjectByType<Light>();
        if (dir != null && dir.type == LightType.Directional)
        {
            dir.intensity = 0.4f;
            dir.color     = Color.white;
        }
    }

    void BuildPointLights(float hw, float hd)
    {
        Vector3[] positions =
        {
            new Vector3(-hw * 0.5f, height - 0.4f, -hd * 0.5f),
            new Vector3( hw * 0.5f, height - 0.4f, -hd * 0.5f),
            new Vector3(-hw * 0.5f, height - 0.4f,  hd * 0.5f),
            new Vector3( hw * 0.5f, height - 0.4f,  hd * 0.5f),
            new Vector3( 0f,        height - 0.4f,  0f),
        };

        foreach (var pos in positions)
        {
            var go = new GameObject("ArenaLight");
            go.transform.SetParent(arenaRoot.transform);
            go.transform.localPosition = pos;

            var l      = go.AddComponent<Light>();
            l.type     = LightType.Point;
            l.color    = lightColor;
            l.intensity = pointLightIntensity;
            l.range    = Mathf.Max(width, depth) * 1.5f;
            l.shadows  = LightShadows.None;
        }
    }
}
