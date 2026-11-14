# Navigation Marker

基于 Unity NavMesh 路径绘制运行时导航线，并通过材质贴图滚动显示连续的方向箭头。

包内包含可直接使用的脚本、Prefab、材质和箭头贴图。

## 功能

- 使用 `NavMesh.CalculatePath` 计算玩家到目标点的路径
- 根据 NavMesh 路径拐点更新 LineRenderer
- 每帧追踪移动中的玩家和目标点
- 自动根据路径总长度调整箭头贴图重复次数
- 通过材质偏移实现箭头滚动动画
- 支持自定义箭头在世界空间中的显示间距

## 安装

### 通过 OpenUPM 安装

在 Unity 的 `Edit > Project Settings > Package Manager` 中添加：

```text
Name: package.openupm.com
URL: https://package.openupm.com
Scope: com.cowart.navigation-marker
```

然后通过 `Add package by name` 安装：

```text
com.cowart.navigation-marker
```

### 通过 Git URL 安装

```text
https://github.com/liuyan7669/unity-navigation-marker.git#3.1.0
```

## 使用前准备

1. 在场景中准备可用的 NavMesh。
2. 确保玩家和目标点位于可计算路径的区域。
3. 使用包内的 `NavigationMarker` Prefab，或将组件添加到带有 LineRenderer 的 GameObject。
4. 设置玩家 `player`、终点 `endPoint` 和 LineRenderer `line`。

## 使用方法

运行时代码使用以下命名空间：

```csharp
using Cowart.NavigationMarker;
```

### 启动导航

```csharp
using Cowart.NavigationMarker;
using UnityEngine;

public class NavigationExample : MonoBehaviour
{
    [SerializeField] private NavigationMarker marker;
    [SerializeField] private Transform player;
    [SerializeField] private Transform destination;

    private void Start()
    {
        marker.player = player;
        marker.endPoint = destination;
        marker.Initialization();
        marker.EnableNavigation();
    }
}
```

必须先调用 `Initialization()` 创建 NavMeshPath 缓存，再调用 `EnableNavigation()` 初始化 LineRenderer 状态。

### 停止导航

```csharp
marker.enabled = false;
marker.DisableNavigation();
```

`DisableNavigation()` 只清空当前 LineRenderer。如果组件仍处于启用状态且引用完整，`Update()` 会在下一帧重新计算并显示路径，因此需要同时禁用组件。

重新启用：

```csharp
marker.enabled = true;
marker.EnableNavigation();
```

### 调整箭头间距

```csharp
marker.textureWorldWidth = 2f;
```

该值表示贴图横向重复一次所对应的世界空间长度。数值越小，路径上的箭头越密集。

## Prefab 与资源

包内提供：

- `NavigationMarker.prefab`
- `NavigationMarker.mat`
- `Arrow.png`

可以从 Package Manager 的包目录中将 Prefab 拖入场景，也可以通过 Resources 加载：

```csharp
GameObject prefab = Resources.Load<GameObject>("NavigationMarker");
GameObject instance = Instantiate(prefab);
```

## 注意事项

- 组件会每帧重新计算完整 NavMesh 路径，目标数量较多时应评估性能。
- `line.material` 会访问当前 LineRenderer 的材质实例。
- `textureWorldWidth` 不应设置为 0，避免路径贴图缩放计算出现无效值。
- 玩家或目标点不在 NavMesh 上时，路径可能为空。

## 主要类型

| 类型 | 说明 |
| --- | --- |
| `NavigationMarker` | NavMesh 路径计算、LineRenderer 绘制与贴图滚动组件 |

## 环境要求

- Unity 2021.3 或更高版本
- Unity AI 模块（`com.unity.modules.ai`）
- 场景中已烘焙或运行时生成的 NavMesh

## 许可证

本包使用 MIT License，详情参见仓库中的许可证文件。

## Inspector 统一依赖

本包声明依赖 `com.cowart.inspector-groups` `3.1.0`，Inspector 样式统一由该基础包提供。安装前须配置 OpenUPM scoped registry（`https://package.openupm.com`，scope `com.cowart`）；UPM 会根据 dependencies 连带解析 Inspector Groups。使用未发布源码时，请同时使用本地 Inspector Groups 3.1.0。业务包之间不互相依赖，Samples 的可选组合不受此限制。
