using UnityEngine;
using UnityEngine.AI;

namespace Cowart.NavigationMarker
{
/// <summary>
/// 使用 Unity NavMesh 路径拐点，从玩家位置到目标点绘制带动画效果的导航线。
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class NavigationMarker : MonoBehaviour
{
    /// <summary>
    /// 计算导航路径时使用的起点。
    /// </summary>
    public Transform player;

    /// <summary>
    /// 计算导航路径时使用的终点。
    /// </summary>
    public Transform endPoint;

    /// <summary>
    /// 用于显示导航路径的线渲染器。
    /// </summary>
    public LineRenderer line;

    /// <summary>
    /// 线条贴图横向重复一次对应的世界空间宽度。
    /// </summary>
    public float textureWorldWidth = 2f;

    private NavMeshPath path;
    private float textureOffsetX;

    /// <summary>
    /// 创建可复用的 NavMesh 路径缓存，启用导航前需要先调用。
    /// </summary>
    public void Initialization()
    {
        path = new NavMeshPath();
    }

    /// <summary>
    /// 准备用于显示路径的线渲染器，并重置贴图滚动偏移。
    /// </summary>
    public void EnableNavigation()
    {
        if (path == null || endPoint == null)
        {
            return;
        }

        line.alignment = LineAlignment.TransformZ;
        line.transform.eulerAngles = new Vector3(90, 0, 0);
        textureOffsetX = 0f;
        SetLineOffset(0f);
    }

    /// <summary>
    /// 清空当前导航路径显示。
    /// </summary>
    public void DisableNavigation()
    {
        if (line != null)
        {
            line.positionCount = 0;
        }
    }

    /// <summary>
    /// 在必要引用齐全时，每帧重新计算并绘制导航路径。
    /// </summary>
    private void Update()
    {
        if (path == null || line == null || player == null || endPoint == null)
        {
            return;
        }

        NavMesh.CalculatePath(player.position, endPoint.position, NavMesh.AllAreas, path);
        Vector3[] corners = path.corners;
        line.positionCount = corners.Length;
        line.SetPositions(corners);
        textureOffsetX -= Time.deltaTime;
        SetLineOffset(textureOffsetX);
        UpdateLineScale();
    }

    /// <summary>
    /// 根据路径总长度缩放线条材质贴图，保持箭头间距稳定。
    /// </summary>
    private void UpdateLineScale()
    {
        if (line.positionCount < 2)
        {
            return;
        }

        float totalLength = 0f;
        for (int i = 1; i < line.positionCount; i++)
        {
            totalLength += Vector3.Distance(line.GetPosition(i - 1), line.GetPosition(i));
        }

        float x = totalLength / textureWorldWidth;
        line.material.mainTextureScale = new Vector2(x, 1f);
    }

    /// <summary>
    /// 设置线条贴图偏移，用于实现路径滚动动画。
    /// </summary>
    private void SetLineOffset(float x, float y = 0f)
    {
        line.material.mainTextureOffset = new Vector2(x, y);
    }
}
}
