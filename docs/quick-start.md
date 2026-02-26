# AntDesign.WPF 快速开始

## 安装

### NuGet 安装
```
dotnet add package AntDesign.WPF --version 1.0.0-preview.1
```

或通过 Visual Studio 的 NuGet 包管理器搜索 `AntDesign.WPF`。

---

## 配置

### 1. 添加 XML 命名空间

在 `App.xaml` 中添加资源字典引用：

```xml
<Application x:Class="YourApp.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:antd="https://antdesign.wpf/">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <!-- 主题引擎 (生成所有颜色/画笔/圆角资源) -->
                <antd:AntDesignTheme />
                <!-- 标准 WPF 控件样式覆盖 (Button, TextBox, CheckBox 等) -->
                <ResourceDictionary Source="pack://application:,,,/AntDesign.WPF;component/Themes/AntDesign.Defaults.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

### 2. 在 XAML 页面中使用

```xml
<Window xmlns:antd="https://antdesign.wpf/">
    <!-- 所有 antd: 前缀的控件即可使用 -->
    <StackPanel>
        <antd:Card>
            <antd:Alert Type="Success" Message="AntDesign.WPF 已就绪!" />
        </antd:Card>
    </StackPanel>
</Window>
```

### 3. 应用窗口样式 (可选)

```xml
<Window Style="{StaticResource AntDesignWindow}">
```

---

## 主题配置

### 初始配置

`AntDesignTheme` 支持以下属性：

```xml
<antd:AntDesignTheme BaseTheme="Light"
                     PrimaryColor="Blue"
                     BorderRadius="6" />
```

| 属性 | 类型 | 默认值 | 说明 |
|---|---|---|---|
| `BaseTheme` | `BaseTheme` | `Light` | 基础主题 (`Light` / `Dark`) |
| `PrimaryColor` | `PresetColor` | `Blue` | 主色调 (12种预设色) |
| `BorderRadius` | `double` | `6` | 全局圆角半径 |

### 可选的预设色

`Blue`, `Purple`, `Cyan`, `Green`, `Magenta`, `Red`, `Orange`, `Gold`, `Lime`, `Volcano`, `Geekblue`

### 运行时切换主题

```csharp
using AntDesign.WPF;
using AntDesign.WPF.Colors;

// 切换到暗色主题
ThemeHelper.SetBaseTheme(BaseTheme.Dark);

// 切换到亮色主题
ThemeHelper.SetBaseTheme(BaseTheme.Light);

// 更换主色调
ThemeHelper.SetPrimaryColor(PresetColor.Purple);

// 更换语义色
ThemeHelper.SetSuccessColor(PresetColor.Green);
ThemeHelper.SetWarningColor(PresetColor.Gold);
ThemeHelper.SetErrorColor(PresetColor.Red);

// 更换圆角半径
ThemeHelper.SetBorderRadius(8);
```

---

## 下一步

- [控件使用文档](controls.md) - 查看所有 30+ 控件的详细用法
- [主题定制文档](theming.md) - 深入了解设计令牌系统
- 运行 Demo 应用查看所有控件效果
