# AntDesign.WPF

An enterprise-grade WPF control library following [Ant Design](https://ant.design) 5.x specifications. Provides 30+ beautifully themed controls, a complete design token system, light/dark theme switching, and accessibility support.

一个遵循 [Ant Design](https://ant.design) 5.x 设计规范的企业级 WPF 控件库。提供 30+ 精美主题控件、完整的 Design Token 体系、明暗主题切换和无障碍支持。

---

**English** | [中文](#中文文档)

## Features

- **30+ Custom Controls** — Card, Tag, Badge, Alert, Modal, Drawer, Rate, Progress, Steps, Timeline, and more
- **18 WPF Theme Overrides** — Button, TextBox, CheckBox, ComboBox, DataGrid, TabControl, TreeView, etc.
- **Design Token System** — 100+ tokens for colors, typography, spacing, and corners
- **Light / Dark Theme** — One-click switching, all tokens update instantly
- **12 Preset Colors** — Blue, Purple, Cyan, Green, Magenta, Red, Orange, Gold, Lime, Volcano, Geekblue
- **Smooth Animations** — Modal zoom, Drawer slide, Button press, page transitions
- **Accessibility** — AutomationPeer for all controls, keyboard navigation, focus management
- **MVVM Ready** — Built-in `RelayCommand`, `ICommand` support on interactive controls
- **High Performance** — Frozen brushes, `ISupportInitialize`, conditional animations

## Quick Start

### 1. Install

```
dotnet add package AntDesign.WPF --version 1.0.0-preview.1
```

### 2. Configure App.xaml

```xml
<Application xmlns:antd="https://antdesign.wpf/">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <antd:AntDesignTheme />
                <ResourceDictionary Source="pack://application:,,,/AntDesign.WPF;component/Themes/AntDesign.Defaults.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

### 3. Use Controls

```xml
<Window xmlns:antd="https://antdesign.wpf/">
    <StackPanel Margin="16">
        <antd:Card Title="Welcome">
            <antd:Alert Type="Success" Message="AntDesign.WPF is ready!" />
        </antd:Card>

        <antd:Tag Content="Processing" PresetColor="Blue" />
        <antd:AntSwitch IsChecked="True" />
        <antd:Rate Value="4" />

        <Button Content="Primary" Style="{DynamicResource AntDesignButton.Primary}" />
    </StackPanel>
</Window>
```

## Theme Switching

```csharp
using AntDesign.WPF;
using AntDesign.WPF.Colors;

// Switch theme
ThemeHelper.SetBaseTheme(BaseTheme.Dark);

// Change primary color
ThemeHelper.SetPrimaryColor(PresetColor.Purple);

// Adjust border radius
ThemeHelper.SetBorderRadius(8);
```

## Controls

| Category | Controls |
|---|---|
| **General** | Button (9 styles) |
| **Data Entry** | AntSwitch, Rate, InputNumber |
| **Data Display** | Card, Tag, Badge, Avatar, Divider, Empty, Timeline, Steps |
| **Feedback** | Alert, Progress, Modal, Drawer, Spin, Result, Message, Notification |
| **Navigation** | Pagination, Breadcrumb, Segmented |
| **Layout** | TransitionPresenter |

Plus 18 Ant Design-themed standard WPF controls (TextBox, CheckBox, ComboBox, DataGrid, TabControl, TreeView, etc.)

## Project Structure

```
AntDesign.WPF/
├── src/
│   ├── AntDesign.WPF/              # Main control library
│   │   ├── Controls/               # 30+ custom controls
│   │   ├── Themes/                 # Generic.xaml + 18 WPF theme files
│   │   ├── Assists/                # Attached properties (Hint, Size, Icon...)
│   │   ├── Converters/             # 12 value converters
│   │   ├── Automation/             # 10 AutomationPeers
│   │   ├── Input/                  # RelayCommand
│   │   └── Tokens/                 # DesignTokens resource keys
│   └── AntDesign.WPF.Colors/       # Color palette library
├── demo/
│   └── AntDesign.WPF.Demo/         # Demo application (24 pages)
└── docs/                            # Documentation
```

## Requirements

- .NET 8.0+
- Windows (WPF)

## License

MIT

---

<a id="中文文档"></a>

## 中文文档

### 特性

- **30+ 自定义控件** — Card 卡片、Tag 标签、Badge 徽标、Alert 警告、Modal 对话框、Drawer 抽屉、Rate 评分、Progress 进度条、Steps 步骤条、Timeline 时间轴等
- **18 个 WPF 原生控件主题** — Button、TextBox、CheckBox、ComboBox、DataGrid、TabControl、TreeView 等
- **Design Token 体系** — 100+ 设计令牌，覆盖颜色、字体、间距、圆角
- **明暗主题切换** — 一键切换，所有令牌即时更新
- **12 种预设主色** — 拂晓蓝、酱紫、明青、极光绿、法式洋红、薄暮红、日暮橙、金盏花、青柠、火山、极客蓝
- **流畅动画** — Modal 缩放、Drawer 滑入、按钮按压、页面过渡
- **无障碍支持** — 所有控件提供 AutomationPeer，支持键盘导航与焦点管理
- **MVVM 友好** — 内置 `RelayCommand`，交互控件支持 `ICommand`
- **高性能** — 冻结画刷、`ISupportInitialize`、条件动画

### 快速开始

#### 1. 安装

```
dotnet add package AntDesign.WPF --version 1.0.0-preview.1
```

#### 2. 配置 App.xaml

```xml
<Application xmlns:antd="https://antdesign.wpf/">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <antd:AntDesignTheme />
                <ResourceDictionary Source="pack://application:,,,/AntDesign.WPF;component/Themes/AntDesign.Defaults.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

#### 3. 使用控件

```xml
<Window xmlns:antd="https://antdesign.wpf/">
    <StackPanel Margin="16">
        <antd:Card Title="欢迎">
            <antd:Alert Type="Success" Message="AntDesign.WPF 已就绪！" />
        </antd:Card>

        <antd:Tag Content="处理中" PresetColor="Blue" />
        <antd:AntSwitch IsChecked="True" />
        <antd:Rate Value="4" />

        <Button Content="主按钮" Style="{DynamicResource AntDesignButton.Primary}" />
    </StackPanel>
</Window>
```

### 主题切换

```csharp
using AntDesign.WPF;
using AntDesign.WPF.Colors;

// 切换主题
ThemeHelper.SetBaseTheme(BaseTheme.Dark);

// 更改主色
ThemeHelper.SetPrimaryColor(PresetColor.Purple);

// 调整圆角
ThemeHelper.SetBorderRadius(8);
```

### 控件一览

| 分类 | 控件 |
|---|---|
| **通用** | Button 按钮（9 种样式） |
| **数据录入** | AntSwitch 开关、Rate 评分、InputNumber 数字输入框 |
| **数据展示** | Card 卡片、Tag 标签、Badge 徽标、Avatar 头像、Divider 分割线、Empty 空状态、Timeline 时间轴、Steps 步骤条 |
| **反馈** | Alert 警告、Progress 进度条、Modal 对话框、Drawer 抽屉、Spin 加载、Result 结果、Message 全局提示、Notification 通知 |
| **导航** | Pagination 分页、Breadcrumb 面包屑、Segmented 分段控制器 |
| **布局** | TransitionPresenter 过渡容器 |

另有 18 个 Ant Design 风格的 WPF 原生控件主题（TextBox、CheckBox、ComboBox、DataGrid、TabControl、TreeView 等）

### 环境要求

- .NET 8.0+
- Windows (WPF)
