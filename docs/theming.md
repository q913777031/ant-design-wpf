# AntDesign.WPF 主题定制文档

## 概述

AntDesign.WPF 使用 **设计令牌 (Design Token)** 系统管理所有视觉属性。整个主题由一小组种子令牌 (Seed Token) 派生出 100+ 映射令牌 (Map Token)，实现一处修改、全局生效。

---

## 设计令牌体系

### 种子令牌 (Seed Tokens)

这些是您可以直接控制的基础设置：

| 种子令牌 | 说明 | 默认值 |
|---|---|---|
| `BaseTheme` | 基础主题 | `Light` |
| `PrimaryColor` | 主色调 | `Blue` (#1677FF) |
| `SuccessColor` | 成功色 | `Green` (#52C41A) |
| `WarningColor` | 警告色 | `Gold` (#FAAD14) |
| `ErrorColor` | 错误色 | `Red` (#FF4D4F) |
| `BorderRadius` | 圆角半径 | `6` |

### 映射令牌 (Map Tokens)

从种子令牌自动生成的 100+ 资源键：

#### 颜色画笔 (Brush)

```
AntDesign.Brush.Primary           — 主色
AntDesign.Brush.Primary.Hover     — 主色悬停
AntDesign.Brush.Primary.Active    — 主色按下
AntDesign.Brush.Primary.Bg        — 主色背景 (浅)
AntDesign.Brush.Primary.Bg.Hover  — 主色背景悬停
AntDesign.Brush.Primary.Border    — 主色边框
AntDesign.Brush.Primary.Border.Hover — 主色边框悬停
AntDesign.Brush.Primary.7         — 主色第7级
AntDesign.Brush.Primary.8         — 主色第8级
AntDesign.Brush.Primary.9         — 主色第9级

AntDesign.Brush.Success           — 成功色
AntDesign.Brush.Success.Hover
AntDesign.Brush.Success.Active
AntDesign.Brush.Success.Bg

AntDesign.Brush.Warning           — 警告色
AntDesign.Brush.Warning.Hover
AntDesign.Brush.Warning.Active
AntDesign.Brush.Warning.Bg

AntDesign.Brush.Error             — 错误色
AntDesign.Brush.Error.Hover
AntDesign.Brush.Error.Active
AntDesign.Brush.Error.Bg
```

#### 文字画笔

```
AntDesign.Brush.Text              — 主要文字 (rgba 88%)
AntDesign.Brush.Text.Secondary    — 次要文字 (rgba 65%)
AntDesign.Brush.Text.Tertiary     — 三级文字 (rgba 45%)
AntDesign.Brush.Text.Quaternary   — 四级文字 (rgba 25%)
AntDesign.Brush.Text.LightSolid   — 白色文字 (#FFFFFF)
```

#### 背景画笔

```
AntDesign.Brush.Bg.Container      — 容器背景
AntDesign.Brush.Bg.Layout         — 布局背景
AntDesign.Brush.Bg.Spotlight      — 侧边栏背景
AntDesign.Brush.Bg.Mask           — 遮罩背景 (rgba 45%)
```

#### 边框与填充

```
AntDesign.Brush.Border            — 默认边框
AntDesign.Brush.Border.Secondary  — 次要边框
AntDesign.Brush.Fill              — 默认填充
AntDesign.Brush.Fill.Secondary    — 次要填充
AntDesign.Brush.Fill.Tertiary     — 三级填充
AntDesign.Brush.Fill.Quaternary   — 四级填充
```

#### 字体大小

```
AntDesign.FontSize                — 14px (默认)
AntDesign.FontSize.SM             — 12px (小)
AntDesign.FontSize.LG             — 16px (大)
AntDesign.FontSize.XL             — 20px (标题)
AntDesign.FontSize.XXL            — 24px (大标题)
AntDesign.FontSize.Heading1       — 38px
AntDesign.FontSize.Heading2       — 30px
AntDesign.FontSize.Heading3       — 24px
AntDesign.FontSize.Heading4       — 20px
AntDesign.FontSize.Heading5       — 16px
```

#### 圆角

```
AntDesign.CornerRadius            — 默认圆角 (6)
AntDesign.CornerRadius.SM         — 小圆角 (4)
AntDesign.CornerRadius.LG         — 大圆角 (8)
AntDesign.CornerRadius.XS         — 极小圆角 (2)
```

#### 间距

```
AntDesign.Padding                 — 默认内距 (16)
AntDesign.Padding.SM              — 小内距 (12)
AntDesign.Padding.XS              — 极小内距 (8)
AntDesign.Margin                  — 默认外距 (16)
AntDesign.Margin.SM               — 小外距 (12)
AntDesign.Margin.XS               — 极小外距 (8)
```

---

## 在 XAML 中使用设计令牌

所有设计令牌通过 `DynamicResource` 引用，确保主题切换时自动更新：

```xml
<!-- 使用颜色令牌 -->
<Border Background="{DynamicResource AntDesign.Brush.Primary.Bg}"
        BorderBrush="{DynamicResource AntDesign.Brush.Border}"
        CornerRadius="{DynamicResource AntDesign.CornerRadius}">
    <TextBlock Text="Hello"
               FontSize="{DynamicResource AntDesign.FontSize.LG}"
               Foreground="{DynamicResource AntDesign.Brush.Text}" />
</Border>
```

---

## 运行时主题切换

### ThemeHelper API

```csharp
using AntDesign.WPF;
using AntDesign.WPF.Colors;

// ═══════════ 基础主题切换 ═══════════

// 获取当前主题
ITheme theme = ThemeHelper.GetTheme();

// 切换亮/暗模式
ThemeHelper.SetBaseTheme(BaseTheme.Dark);
ThemeHelper.SetBaseTheme(BaseTheme.Light);

// ═══════════ 颜色定制 ═══════════

// 主色调 (12种预设色)
ThemeHelper.SetPrimaryColor(PresetColor.Purple);
ThemeHelper.SetPrimaryColor(PresetColor.Cyan);

// 语义色
ThemeHelper.SetSuccessColor(PresetColor.Green);
ThemeHelper.SetWarningColor(PresetColor.Gold);
ThemeHelper.SetErrorColor(PresetColor.Red);

// ═══════════ 圆角 ═══════════

ThemeHelper.SetBorderRadius(0);   // 直角风格
ThemeHelper.SetBorderRadius(6);   // 默认
ThemeHelper.SetBorderRadius(16);  // 大圆角风格
```

### 通过 AntDesignTheme 初始化

```xml
<antd:AntDesignTheme BaseTheme="Dark"
                     PrimaryColor="Purple"
                     BorderRadius="8" />
```

---

## 亮色 vs 暗色主题对照

| 令牌 | 亮色模式 | 暗色模式 |
|---|---|---|
| `Brush.Bg.Container` | `#FFFFFF` | `#141414` |
| `Brush.Bg.Layout` | `#F5F5F5` | `#000000` |
| `Brush.Bg.Spotlight` | `#001529` | `#001529` |
| `Brush.Text` | `rgba(0,0,0,88%)` | `rgba(255,255,255,85%)` |
| `Brush.Text.Secondary` | `rgba(0,0,0,65%)` | `rgba(255,255,255,65%)` |
| `Brush.Border` | `#D9D9D9` | `#424242` |
| `Brush.Fill.Quaternary` | `rgba(0,0,0,2%)` | `rgba(255,255,255,2%)` |

---

## 自定义控件中使用令牌

当您创建自己的自定义控件时，可以引用 AntDesign 令牌保持一致风格：

```xml
<Style TargetType="{x:Type local:MyControl}">
    <Setter Property="Background" Value="{DynamicResource AntDesign.Brush.Bg.Container}" />
    <Setter Property="BorderBrush" Value="{DynamicResource AntDesign.Brush.Border}" />
    <Setter Property="Foreground" Value="{DynamicResource AntDesign.Brush.Text}" />
    <Setter Property="FontSize" Value="{DynamicResource AntDesign.FontSize}" />
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="{x:Type local:MyControl}">
                <Border Background="{TemplateBinding Background}"
                        BorderBrush="{TemplateBinding BorderBrush}"
                        BorderThickness="1"
                        CornerRadius="{DynamicResource AntDesign.CornerRadius}"
                        Padding="{DynamicResource AntDesign.Padding.SM}">
                    <ContentPresenter />
                </Border>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

---

## 性能说明

- **冻结画笔**: 所有生成的 `SolidColorBrush` 均已 `Freeze()`，支持跨线程访问且减少 GC 压力
- **ISupportInitialize**: `AntDesignTheme` 实现了 `ISupportInitialize`，在 XAML 加载期间仅执行一次初始化
- **DynamicResource**: 所有令牌通过 `DynamicResource` 引用，运行时切换主题零性能损失
