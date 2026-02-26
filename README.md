# AntDesign.WPF

An enterprise-grade WPF control library following [Ant Design](https://ant.design) 5.x specifications. Provides 30+ beautifully themed controls, a complete design token system, light/dark theme switching, and accessibility support.

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
    ├── quick-start.md               # 快速开始
    ├── controls.md                  # 控件使用文档
    └── theming.md                   # 主题定制文档
```

## Requirements

- .NET 8.0+
- Windows (WPF)

## License

MIT
