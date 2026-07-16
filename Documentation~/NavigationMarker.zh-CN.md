# NavigationMarker

`NavigationMarker` 用 `NavMesh.CalculatePath` 计算玩家到目标点的路径，并用带箭头贴图的 `LineRenderer` 显示导航线。

## 资源

- 脚本：`NavigationMarker.cs`
- 预制体：`Resources/NavigationMarker.prefab`
- 材质：`NavigationMarker.mat`
- 贴图：`Arrow.png`

## 使用方式

1. 确认场景已经烘焙 NavMesh。
2. 将 `Resources/NavigationMarker.prefab` 放入场景，或通过 `Resources.Load` 实例化。
3. 绑定 `player` 为起点，`endPoint` 为目标点，`line` 通常使用预制体自带的 `LineRenderer`。
4. 初始化并启用导航：

```csharp
navigationMarker.Initialization();
navigationMarker.EnableNavigation();
```

5. 不需要显示时调用：

```csharp
navigationMarker.DisableNavigation();
```

## 字段说明

- `player`：路径起点。
- `endPoint`：路径终点。
- `line`：用于绘制路径的 `LineRenderer`。
- `textureWorldWidth`：贴图在世界空间中的重复宽度，值越小箭头越密。

## 注意事项

- 当前实现会在 `Update` 中每帧重新计算路径，适合目标或玩家持续移动的导航提示。
- `Initialization` 必须在显示导航前调用，否则不会计算路径。
- `player`、`endPoint`、`line` 任一为空时不会绘制路径。
