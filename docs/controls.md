# AntDesign.WPF 控件使用文档

> 命名空间: `xmlns:antd="https://antdesign.wpf/"`

---

## 目录

- [通用 General](#通用-general)
  - [Button 按钮](#button-按钮)
- [数据录入 Data Entry](#数据录入-data-entry)
  - [AntSwitch 开关](#antswitch-开关)
  - [Rate 评分](#rate-评分)
  - [InputNumber 数字输入](#inputnumber-数字输入)
- [数据展示 Data Display](#数据展示-data-display)
  - [Card 卡片](#card-卡片)
  - [Tag 标签](#tag-标签)
  - [Badge 徽标数](#badge-徽标数)
  - [Avatar 头像](#avatar-头像)
  - [Divider 分割线](#divider-分割线)
  - [Empty 空状态](#empty-空状态)
  - [Timeline 时间轴](#timeline-时间轴)
  - [Steps 步骤条](#steps-步骤条)
- [反馈 Feedback](#反馈-feedback)
  - [Alert 警告提示](#alert-警告提示)
  - [Progress 进度条](#progress-进度条)
  - [Modal 对话框](#modal-对话框)
  - [Drawer 抽屉](#drawer-抽屉)
  - [Spin 加载中](#spin-加载中)
  - [Result 结果](#result-结果)
  - [Message 全局提示](#message-全局提示)
- [导航 Navigation](#导航-navigation)
  - [Pagination 分页](#pagination-分页)
  - [Breadcrumb 面包屑](#breadcrumb-面包屑)
  - [Segmented 分段控制器](#segmented-分段控制器)
- [其他 Other](#其他-other)
  - [TransitionPresenter 页面切换](#transitionpresenter-页面切换)

---

## 通用 General

### Button 按钮

AntDesign.WPF 为标准 WPF `Button` 提供了 9 种预设样式，包含按下缩放动画。

```xml
<!-- 主要按钮 -->
<Button Content="Primary" Style="{DynamicResource AntDesignButton.Primary}" />

<!-- 默认按钮 -->
<Button Content="Default" Style="{DynamicResource AntDesignButton}" />

<!-- 虚线按钮 -->
<Button Content="Dashed" Style="{DynamicResource AntDesignButton.Dashed}" />

<!-- 文字按钮 -->
<Button Content="Text" Style="{DynamicResource AntDesignButton.Text}" />

<!-- 链接按钮 -->
<Button Content="Link" Style="{DynamicResource AntDesignButton.Link}" />

<!-- 危险按钮 -->
<Button Content="Danger" Style="{DynamicResource AntDesignButton.Danger}" />

<!-- 危险主要按钮 -->
<Button Content="Danger" Style="{DynamicResource AntDesignButton.DangerPrimary}" />

<!-- 成功按钮 -->
<Button Content="Success" Style="{DynamicResource AntDesignButton.Success}" />

<!-- 幽灵按钮 -->
<Button Content="Ghost" Style="{DynamicResource AntDesignButton.Ghost}" />
```

---

## 数据录入 Data Entry

### AntSwitch 开关

```xml
<antd:AntSwitch IsChecked="True" />
<antd:AntSwitch IsChecked="{Binding IsEnabled, Mode=TwoWay}" />
```

| 属性 | 类型 | 默认值 | 说明 |
|---|---|---|---|
| `IsChecked` | `bool` | `false` | 开关状态 (支持双向绑定) |

### Rate 评分

```xml
<antd:Rate Value="3" />
<antd:Rate Value="{Binding Rating, Mode=TwoWay}" AllowHalf="True" />
<antd:Rate Count="10" AllowClear="True" />
```

| 属性 | 类型 | 默认值 | 说明 |
|---|---|---|---|
| `Value` | `double` | `0` | 当前评分值 |
| `Count` | `int` | `5` | 星星数量 |
| `AllowHalf` | `bool` | `false` | 允许半星 |
| `AllowClear` | `bool` | `true` | 允许再次点击清空 |
| `IsReadOnly` | `bool` | `false` | 只读模式 |

### InputNumber 数字输入

```xml
<antd:InputNumber Value="3" Minimum="0" Maximum="100" Step="1" />
```

| 属性 | 类型 | 默认值 | 说明 |
|---|---|---|---|
| `Value` | `double` | `0` | 当前值 |
| `Minimum` | `double` | `double.MinValue` | 最小值 |
| `Maximum` | `double` | `double.MaxValue` | 最大值 |
| `Step` | `double` | `1` | 步长 |

---

## 数据展示 Data Display

### Card 卡片

```xml
<antd:Card Title="Card Title">
    <TextBlock Text="Card content goes here." />
</antd:Card>

<!-- 带额外操作 -->
<antd:Card Title="Card" Extra="More">
    <TextBlock Text="Content" />
</antd:Card>

<!-- 无边框 -->
<antd:Card Title="Borderless" Bordered="False">
    <TextBlock Text="Content" />
</antd:Card>
```

| 属性 | 类型 | 默认值 | 说明 |
|---|---|---|---|
| `Title` | `string` | `null` | 标题文本 |
| `Extra` | `object` | `null` | 右上角额外内容 |
| `Bordered` | `bool` | `true` | 是否显示边框 |
| `Hoverable` | `bool` | `false` | 鼠标悬停时升起 |

### Tag 标签

```xml
<!-- 基本用法 -->
<antd:Tag Content="Tag 1" />

<!-- 预设颜色 -->
<antd:Tag Content="Success" PresetColor="Green" />
<antd:Tag Content="Processing" PresetColor="Blue" />
<antd:Tag Content="Error" PresetColor="Red" />
<antd:Tag Content="Warning" PresetColor="Gold" />

<!-- 可关闭 -->
<antd:Tag Content="Closable" Closable="True" Closed="Tag_Closed" />
```

| 属性 | 类型 | 默认值 | 说明 |
|---|---|---|---|
| `PresetColor` | `PresetColor?` | `null` | 预设颜色 (12种) |
| `Color` | `string` | `null` | 自定义颜色 |
| `Closable` | `bool` | `false` | 是否可关闭 |
| `Icon` | `object` | `null` | 自定义图标 |

**事件**: `Closed` - 关闭标签后触发

### Badge 徽标数

```xml
<antd:Badge Count="5">
    <Button Content="Button" />
</antd:Badge>

<!-- 小红点 -->
<antd:Badge Dot="True">
    <TextBlock Text="Notifications" />
</antd:Badge>

<!-- 状态徽标 -->
<antd:Badge Status="Processing" Text="Processing" />
```

| 属性 | 类型 | 默认值 | 说明 |
|---|---|---|---|
| `Count` | `int` | `0` | 数字 |
| `OverflowCount` | `int` | `99` | 封顶数字 |
| `Dot` | `bool` | `false` | 不展示数字，只展示小红点 |
| `ShowZero` | `bool` | `false` | 值为 0 时是否显示 |
| `Status` | `BadgeStatus` | `Default` | 状态点 |
| `Text` | `string` | `null` | 状态点文本 |

### Avatar 头像

```xml
<antd:Avatar Text="U" />
<antd:Avatar Text="AB" Shape="Square" />
```

| 属性 | 类型 | 默认值 | 说明 |
|---|---|---|---|
| `Text` | `string` | `null` | 文字内容 |
| `Shape` | `AvatarShape` | `Circle` | 形状 (`Circle` / `Square`) |

### Divider 分割线

```xml
<antd:Divider />
<antd:Divider Orientation="Vertical" />
<antd:Divider Content="分割文字" />
```

| 属性 | 类型 | 默认值 | 说明 |
|---|---|---|---|
| `Orientation` | `Orientation` | `Horizontal` | 水平/垂直 |
| `Content` | `object` | `null` | 分割线上的文字 |

### Empty 空状态

```xml
<antd:Empty />
<antd:Empty Description="暂无数据" />
```

| 属性 | 类型 | 默认值 | 说明 |
|---|---|---|---|
| `Description` | `string` | `"No Data"` | 描述文字 |
| `ImageSource` | `ImageSource` | `null` | 自定义图片 |

### Timeline 时间轴

```xml
<antd:Timeline>
    <antd:TimelineItem Content="创建项目 2026-01-01" />
    <antd:TimelineItem Content="通过审核 2026-01-02" Color="Green" />
    <antd:TimelineItem Content="部署发布 2026-01-03" Color="Red" />
</antd:Timeline>
```

### Steps 步骤条

```xml
<antd:Steps Current="1">
    <antd:StepItem Title="Step 1" Description="Description" />
    <antd:StepItem Title="Step 2" Description="Description" />
    <antd:StepItem Title="Step 3" Description="Description" />
</antd:Steps>
```

| 属性 | 类型 | 默认值 | 说明 |
|---|---|---|---|
| `Current` | `int` | `0` | 当前步骤 (0开始) |
| `Direction` | `Orientation` | `Horizontal` | 方向 |

---

## 反馈 Feedback

### Alert 警告提示

```xml
<antd:Alert Type="Success" Message="操作成功!" />
<antd:Alert Type="Info" Message="提示信息" Description="详细描述" />
<antd:Alert Type="Warning" Message="警告" Closable="True" />
<antd:Alert Type="Error" Message="错误" ShowIcon="True" />
```

| 属性 | 类型 | 默认值 | 说明 |
|---|---|---|---|
| `Type` | `AlertType` | `Info` | 类型 (`Success`/`Info`/`Warning`/`Error`) |
| `Message` | `string` | `null` | 提示标题 |
| `Description` | `string` | `null` | 辅助说明 |
| `Closable` | `bool` | `false` | 是否可关闭 |
| `ShowIcon` | `bool` | `true` | 是否显示图标 |
| `Banner` | `bool` | `false` | 横幅模式 |

**事件**: `Closed` - 关闭后触发 (含淡出动画)

### Progress 进度条

```xml
<antd:Progress Percent="72" />
<antd:Progress Percent="100" Status="Success" />
<antd:Progress Percent="30" Type="Circle" />
```

| 属性 | 类型 | 默认值 | 说明 |
|---|---|---|---|
| `Percent` | `double` | `0` | 百分比 (0-100) |
| `Status` | `ProgressStatus` | `Normal` | 状态 |
| `Type` | `ProgressType` | `Line` | 类型 (`Line`/`Circle`/`Dashboard`) |
| `ShowInfo` | `bool` | `true` | 是否显示百分比文字 |

### Modal 对话框

```xml
<!-- 声明式用法 -->
<antd:Modal IsOpen="{Binding IsModalVisible}"
            Title="对话框标题"
            DialogContent="{Binding DialogContent}"
            OkText="确定"
            CancelText="取消"
            OkClicked="Modal_OkClicked">
    <!-- 这里是页面内容 (对话框会覆盖在其上) -->
    <StackPanel>
        <Button Content="打开对话框"
                Command="{x:Static antd:Modal.OpenCommand}" />
    </StackPanel>
</antd:Modal>
```

| 属性 | 类型 | 默认值 | 说明 |
|---|---|---|---|
| `IsOpen` | `bool` | `false` | 是否显示 (双向绑定) |
| `Title` | `string` | `null` | 标题 |
| `DialogContent` | `object` | `null` | 对话框内容 |
| `DialogWidth` | `double` | `520` | 对话框宽度 |
| `MaskClosable` | `bool` | `true` | 点击遮罩关闭 |
| `Closable` | `bool` | `true` | 显示关闭按钮 |
| `OkText` | `string` | `"OK"` | 确认按钮文本 |
| `CancelText` | `string` | `"Cancel"` | 取消按钮文本 |

**事件**: `Opened`, `Closed`, `OkClicked`, `CancelClicked`

**命令**: `Modal.OpenCommand`, `Modal.CloseCommand`

**动画**: 打开时淡入+缩放 (0.85→1.0)，关闭时淡出+缩放 (1.0→0.85)

### Drawer 抽屉

```xml
<antd:Drawer IsOpen="{Binding IsDrawerOpen}"
             Title="抽屉标题"
             Placement="Right"
             DrawerWidth="400"
             DrawerContent="{Binding DrawerBody}">
    <!-- 页面内容 -->
</antd:Drawer>
```

| 属性 | 类型 | 默认值 | 说明 |
|---|---|---|---|
| `IsOpen` | `bool` | `false` | 是否显示 (双向绑定) |
| `Title` | `string` | `null` | 标题 |
| `Placement` | `DrawerPlacement` | `Right` | 方向 (`Left`/`Right`/`Top`/`Bottom`) |
| `DrawerWidth` | `double` | `378` | 左右方向宽度 |
| `DrawerHeight` | `double` | `378` | 上下方向高度 |
| `DrawerContent` | `object` | `null` | 抽屉内容 |
| `Closable` | `bool` | `true` | 显示关闭按钮 |
| `MaskClosable` | `bool` | `true` | 点击遮罩关闭 |

**动画**: 基于实际宽度/高度的滑入滑出动画，遮罩淡入淡出

### Spin 加载中

```xml
<!-- 包裹内容 -->
<antd:Spin IsSpinning="True" Tip="加载中...">
    <TextBlock Text="内容在这里" />
</antd:Spin>

<!-- 独立使用 -->
<antd:Spin IsSpinning="True" />
```

| 属性 | 类型 | 默认值 | 说明 |
|---|---|---|---|
| `IsSpinning` | `bool` | `true` | 是否旋转 |
| `Tip` | `string` | `null` | 提示文字 |
| `Size` | `SpinSize` | `Default` | 大小 (`Small`/`Default`/`Large`) |
| `Delay` | `int` | `0` | 延迟显示 (毫秒) |

**优化**: 仅在 `IsSpinning=true` 时运行动画，减少 CPU 占用

### Result 结果

```xml
<antd:Result Status="Success"
             Title="操作成功"
             SubTitle="提交的订单号为 2017182818828182" />
```

| 属性 | 类型 | 默认值 | 说明 |
|---|---|---|---|
| `Status` | `ResultStatus` | `Info` | 状态类型 |
| `Title` | `string` | `null` | 标题 |
| `SubTitle` | `string` | `null` | 副标题 |
| `Extra` | `object` | `null` | 额外操作区域 |

### Message 全局提示

```csharp
// 在 C# 代码中调用
MessageService.Info("This is an info message");
MessageService.Success("Operation successful!");
MessageService.Warning("Warning notification");
MessageService.Error("Something went wrong");
```

**注意**: 使用 `MessageService` 前，确保 Window 的视觉树中有 `MessageContainer` 控件。

---

## 导航 Navigation

### Pagination 分页

```xml
<antd:Pagination Current="1"
                 Total="100"
                 PageSize="10"
                 CurrentChanged="Pagination_Changed" />
```

| 属性 | 类型 | 默认值 | 说明 |
|---|---|---|---|
| `Current` | `int` | `1` | 当前页码 |
| `Total` | `int` | `0` | 总条目数 |
| `PageSize` | `int` | `10` | 每页条数 |
| `PageSizeOptions` | `int[]` | `null` | 每页条数选项 |

**事件**: `CurrentChanged`
**命令**: `PreviousCommand`, `NextCommand`, `GoToPageCommand`

### Breadcrumb 面包屑

```xml
<antd:Breadcrumb>
    <antd:BreadcrumbItem Content="Home" />
    <antd:BreadcrumbItem Content="Application" />
    <antd:BreadcrumbItem Content="Detail" />
</antd:Breadcrumb>
```

### Segmented 分段控制器

```xml
<antd:Segmented>
    <antd:SegmentedItem Content="Daily" />
    <antd:SegmentedItem Content="Weekly" />
    <antd:SegmentedItem Content="Monthly" />
</antd:Segmented>
```

---

## 其他 Other

### TransitionPresenter 页面切换

用于实现页面切换时的过渡动画效果：

```xml
<antd:TransitionPresenter x:Name="PageHost"
                          TransitionType="Fade"
                          Duration="0:0:0.2" />
```

代码中切换页面：
```csharp
PageHost.Content = new SomePage();
```

| 属性 | 类型 | 默认值 | 说明 |
|---|---|---|---|
| `TransitionType` | `TransitionType` | `Fade` | 动画类型 |
| `Duration` | `TimeSpan` | `250ms` | 动画时长 |

**动画类型**: `Fade` (淡入淡出), `SlideLeft` (左滑), `SlideRight` (右滑), `SlideUp` (上滑)

---

## 标准 WPF 控件样式

除自定义控件外，AntDesign.WPF 还为 18 种标准 WPF 控件提供了 Ant Design 风格样式：

| 控件 | 样式 Key | 说明 |
|---|---|---|
| Button | `AntDesignButton` + 8种变体 | 主要/默认/虚线/文字/链接/危险等 |
| TextBox | `AntDesignTextBox` | 圆角输入框 |
| CheckBox | `AntDesignCheckBox` | 动画勾选 |
| RadioButton | `AntDesignRadioButton` | 圆形单选 |
| ComboBox | `AntDesignComboBox` | 下拉选择 |
| Slider | `AntDesignSlider` | 滑动条 |
| ProgressBar | `AntDesignProgressBar` | 进度条 |
| DatePicker | `AntDesignDatePicker` | 日期选择 |
| ScrollViewer | 自动应用 | 美化滚动条 |
| ToolTip | 自动应用 | 圆角提示 |
| Menu | `AntDesignMenu` | 菜单 |
| ListView | `AntDesignListView` | 列表视图 |
| TabControl | `AntDesignTabControl` | 选项卡 |
| DataGrid | `AntDesignDataGrid` | 数据表格 |
| TreeView | `AntDesignTreeView` | 树形视图 |
| Expander | `AntDesignExpander` | 折叠面板 |
| GroupBox | `AntDesignGroupBox` | 分组框 |
| Window | `AntDesignWindow` | 窗口样式 |

这些样式通过 `AntDesign.Defaults.xaml` 自动加载。
