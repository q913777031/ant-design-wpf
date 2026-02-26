# AntDesign WPF 控件库 - 24小时开发工作计划

## 项目概述
开发一个WPF的Ant Design风格控件库，命名为 **AntDesign.WPF**，参考MaterialDesignInXamlToolkit的架构和使用方式，实现Ant Design 5.x的设计规范。

---

## 项目架构 (参考MaterialDesignToolkit)

```
AntDesign.WPF/
├── src/
│   ├── AntDesign.WPF.Colors/           # 颜色包 (对应MaterialDesignColors)
│   │   ├── AntDesign.WPF.Colors.csproj
│   │   ├── PresetColors.cs             # 12预设色板枚举
│   │   ├── ColorPalette.cs             # 10级色阶生成算法
│   │   └── Themes/
│   │       ├── Blue.xaml ~ Magenta.xaml # 12个色板XAML
│   │       └── Neutral.xaml            # 中性灰色板
│   │
│   └── AntDesign.WPF/                  # 主题+控件包 (对应MaterialDesignThemes.Wpf)
│       ├── AntDesign.WPF.csproj
│       ├── Themes/
│       │   ├── AntDesign.Defaults.xaml # 一键合并入口
│       │   ├── Generic.xaml            # 自定义控件默认样式
│       │   ├── AntDesignTheme.Button.xaml
│       │   ├── AntDesignTheme.TextBox.xaml
│       │   ├── AntDesignTheme.CheckBox.xaml
│       │   ├── ... (每个控件一个XAML)
│       │   └── Shadows.xaml
│       ├── Controls/                   # 自定义控件
│       │   ├── Card.cs
│       │   ├── Tag.cs
│       │   ├── Badge.cs
│       │   ├── Alert.cs
│       │   ├── Message.cs
│       │   ├── Avatar.cs
│       │   ├── Drawer.cs
│       │   ├── Modal.cs
│       │   ├── Spin.cs
│       │   ├── Progress.cs
│       │   ├── Steps.cs
│       │   ├── Tabs.cs (AntTabs)
│       │   ├── Breadcrumb.cs
│       │   ├── Pagination.cs
│       │   ├── Empty.cs
│       │   ├── Result.cs
│       │   ├── Divider.cs
│       │   ├── Rate.cs
│       │   ├── Switch.cs (AntSwitch)
│       │   ├── Segmented.cs
│       │   └── Timeline.cs
│       ├── Assists/                    # 附加属性 (对应Behaviors)
│       │   ├── TextFieldAssist.cs
│       │   ├── HintAssist.cs
│       │   ├── ElevationAssist.cs
│       │   ├── IconAssist.cs
│       │   └── SizeAssist.cs
│       ├── Converters/
│       ├── Tokens/
│       │   ├── DesignTokens.cs         # 设计令牌系统
│       │   ├── SeedToken.cs            # 种子令牌
│       │   └── AliasToken.cs           # 别名令牌
│       ├── AntDesignTheme.cs           # 主题MarkupExtension (对应BundledTheme)
│       ├── ThemeHelper.cs              # 运行时主题切换 (对应PaletteHelper)
│       └── Theme.cs / ITheme.cs        # 主题模型
│
├── demo/
│   └── AntDesign.WPF.Demo/            # 展示应用 (对应MaterialDesignDemo)
│       ├── App.xaml
│       ├── MainWindow.xaml             # 侧边栏导航
│       └── Pages/                      # 控件演示页
│           ├── ButtonPage.xaml
│           ├── InputPage.xaml
│           ├── ...
│           └── ThemePage.xaml
│
├── tests/
│   └── AntDesign.WPF.Tests/
│
├── Directory.Build.props
├── AntDesign.WPF.sln
└── README.md (用户要求时再创建)
```

---

## 阶段计划

### 第一阶段 [0-3小时] 基础架构搭建
**目标:** 创建解决方案、项目结构、主题系统基础

- [x] 1.1 创建解决方案和项目结构
  - `AntDesign.WPF.sln`
  - `src/AntDesign.WPF.Colors/` (.NET 8.0 WPF类库)
  - `src/AntDesign.WPF/` (.NET 8.0 WPF类库)
  - `demo/AntDesign.WPF.Demo/` (.NET 8.0 WPF应用)
  - `Directory.Build.props` 集中管理版本号

- [x] 1.2 实现颜色系统 (AntDesign.WPF.Colors)
  - `PresetColor.cs` - 12个预设颜色枚举 (Blue/Green/Red/Gold/Yellow/Orange/Volcano/Lime/Cyan/GeekBlue/Purple/Magenta)
  - `ColorPalette.cs` - 存储所有10级色阶的硬编码值
  - `NeutralColors.cs` - 13级中性灰
  - 每个颜色的XAML ResourceDictionary

- [x] 1.3 实现设计令牌系统 (AntDesign.WPF)
  - `SeedToken.cs` - 种子令牌 (colorPrimary, fontSize, borderRadius等)
  - `DesignTokens.cs` - 完整令牌体系
  - `Theme.cs / ITheme.cs` - 主题模型 (Light/Dark)

### 第二阶段 [3-6小时] 主题引擎与基础样式
**目标:** 主题标记扩展、基础资源字典、基础控件样式

- [x] 2.1 AntDesignTheme MarkupExtension
  - 类似BundledTheme的用法
  - 配置: BaseTheme(Light/Dark), PrimaryColor, SuccessColor, WarningColor, ErrorColor
  - 动态生成所有Brush资源

- [x] 2.2 基础资源字典
  - `AntDesign.Defaults.xaml` - 一键导入所有样式
  - Typography资源 (字体、大小、行高)
  - 间距/圆角/阴影资源
  - 基础Brush资源绑定

- [x] 2.3 基础WPF控件样式 - 第一批
  - **Button** (5种变体: Primary/Default/Dashed/Text/Link + Ghost + Danger)
  - **TextBox** (Input: outlined/filled/borderless + 尺寸)
  - **PasswordBox** (密码输入)
  - **CheckBox**
  - **RadioButton**

### 第三阶段 [6-10小时] 核心控件开发
**目标:** 高优先级控件实现

- [ ] 3.1 更多WPF控件样式
  - **ComboBox** (Select样式)
  - **Slider**
  - **ProgressBar** (Line + Circle)
  - **DatePicker**
  - **ListBox** / **ListView** (List样式)
  - **TabControl** (Tabs样式)
  - **DataGrid** (Table样式)
  - **ScrollViewer** / **ScrollBar**
  - **Menu** / **ContextMenu**
  - **ToolTip**
  - **Expander** (Collapse样式)
  - **GroupBox**
  - **TreeView** (Tree样式)

- [ ] 3.2 自定义控件 - 第一批
  - **Card** - 卡片容器 (带elevation/hoverable)
  - **Tag** - 标签 (多色/可关闭)
  - **Badge** - 徽标 (数字/红点/状态)
  - **Alert** - 警告提示 (4种类型/可关闭)
  - **Divider** - 分割线 (水平/垂直/带文字)
  - **Avatar** - 头像 (图片/图标/文字)
  - **Empty** - 空状态
  - **Spin** - 加载中

### 第四阶段 [10-14小时] 高级控件开发
**目标:** 复杂控件和反馈类控件

- [ ] 4.1 自定义控件 - 第二批
  - **Switch** (AntSwitch) - 开关
  - **Rate** - 评分
  - **InputNumber** - 数字输入框
  - **Progress** (自定义控件版, Line/Circle/Dashboard)
  - **Steps** - 步骤条
  - **Breadcrumb** - 面包屑
  - **Pagination** - 分页
  - **Timeline** - 时间轴
  - **Segmented** - 分段控制器

- [ ] 4.2 反馈类控件
  - **Modal** - 模态框 (类似DialogHost)
  - **Drawer** - 抽屉 (类似DrawerHost)
  - **Message** - 全局提示 (类似Snackbar)
  - **Notification** - 通知提醒
  - **Result** - 结果页
  - **Popconfirm** - 气泡确认

### 第五阶段 [14-18小时] 附加属性与完善
**目标:** Assist系统、转换器、完善所有控件

- [ ] 5.1 Assist附加属性系统
  - **HintAssist** - 浮动标签/占位符
  - **TextFieldAssist** - 清除按钮/前后缀
  - **ElevationAssist** - 阴影层级
  - **SizeAssist** - 大中小尺寸
  - **IconAssist** - 图标支持

- [ ] 5.2 值转换器
  - BoolToVisibility, ColorToBrush, EnumToVisibility等
  - 边距/圆角转换器

- [ ] 5.3 运行时主题切换
  - **ThemeHelper** - 类似PaletteHelper
  - 支持动态切换Light/Dark
  - 支持动态修改PrimaryColor
  - DynamicResource绑定确保实时更新

### 第六阶段 [18-22小时] Demo展示应用
**目标:** 构建完整的控件展示应用

- [ ] 6.1 Demo应用框架
  - 侧边栏导航 (类似Ant Design官网)
  - 主题切换器 (Light/Dark/PrimaryColor)
  - 响应式布局

- [ ] 6.2 控件展示页面
  - 每个控件独立页面
  - 多种变体展示
  - 代码示例文本
  - 按Ant Design文档分类:
    - 通用: Button, Icon, Typography
    - 布局: Divider, Space
    - 导航: Breadcrumb, Menu, Pagination, Steps, Tabs
    - 数据录入: Checkbox, DatePicker, Input, InputNumber, Radio, Rate, Select, Slider, Switch
    - 数据展示: Avatar, Badge, Card, Collapse, Empty, List, Table, Tag, Timeline, Tooltip, Tree
    - 反馈: Alert, Drawer, Message, Modal, Notification, Progress, Result, Spin

### 第七阶段 [22-24小时] 测试与发布准备
**目标:** 质量保证和发布配置

- [ ] 7.1 基础测试
  - 主题切换测试
  - 控件渲染测试
  - Light/Dark模式全面测试

- [ ] 7.2 NuGet发布配置
  - 项目属性配置 (PackageId, Description, Tags等)
  - 确保所有XAML资源正确嵌入
  - 版本号: 1.0.0-preview.1

- [ ] 7.3 最终检查
  - 编译无错误/警告
  - Demo应用完整运行
  - 所有控件可正常使用

---

## 使用方式 (最终用户体验)

```xml
<!-- App.xaml -->
<Application xmlns:antd="clr-namespace:AntDesign.WPF;assembly=AntDesign.WPF">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <!-- 步骤1: 配置主题 -->
                <antd:AntDesignTheme BaseTheme="Light"
                                     PrimaryColor="Blue"
                                     BorderRadius="6" />

                <!-- 步骤2: 加载所有控件样式 -->
                <ResourceDictionary Source="pack://application:,,,/AntDesign.WPF;component/Themes/AntDesign.Defaults.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

```xml
<!-- 使用控件 -->
<Button Style="{StaticResource AntDesignPrimaryButton}" Content="Primary" />
<Button Style="{StaticResource AntDesignDashedButton}" Content="Dashed" />
<antd:Card Title="Card Title" Hoverable="True">
    <TextBlock Text="Card content" />
</antd:Card>
<antd:Tag Color="Blue">Tag</antd:Tag>
<antd:Badge Count="5">
    <antd:Avatar Text="U" />
</antd:Badge>
```

---

## 优先级说明

24小时内重点确保:
1. ✅ 完整的主题系统 (颜色、令牌、Light/Dark)
2. ✅ 所有WPF标准控件的Ant Design样式
3. ✅ 15-20个核心自定义控件
4. ✅ 可运行的Demo展示应用
5. ✅ 运行时主题切换功能

如时间不足，优先保证1-3项完成度。
