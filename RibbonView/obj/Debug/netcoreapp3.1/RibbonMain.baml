   M S B A M L   `   `   `  ����E  ARibbonView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:RibbonView
RibbonView  5   	       RibbonView.RibbonMain   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35h dDevExpress.Xpf.TypedStyles.v19.2, Version=19.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Core.v19.2, Version=19.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089L 9http://schemas.microsoft.com/winfx/2006/xaml/presentation        R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089<x,http://schemas.microsoft.com/winfx/2006/xaml      5   	   Bd2http://schemas.microsoft.com/expression/blend/2008      5   	   Lmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5   	   ,localclr-namespace:RibbonView      5   	   l	 hDevExpress.Xpf.Core.v19.2.Extensions, Version=19.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dx2http://schemas.devexpress.com/winfx/2008/xaml/core 	   5   	   Adxe5http://schemas.devexpress.com/winfx/2008/xaml/editors   5   	   >dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars   5	   	   c
 _DevExpress.Xpf.Ribbon.v19.2, Version=19.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aBdxr4http://schemas.devexpress.com/winfx/2008/xaml/ribbon   
 5
   	   j fDevExpress.Xpf.LayoutControl.v19.2, Version=19.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aHdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol   5   	   d `DevExpress.Xpf.Docking.v19.2, Version=19.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aBdxdo5http://schemas.devexpress.com/winfx/2008/xaml/docking   5   	     � Title$!  功能区控件Ribbon案例��5   	   $	��450��6   $	��800��6,   &   DevExpress.Xpf.Core.ThemedWindow   HeaderItems 5      +  %DevExpress.Xpf.Core.HeaderItemControl  5   
   $�� HeaderItemControl��6   &   DevExpress.Xpf.Core.SimpleButton  5   
   $�� SimpleButton��6   %  DevExpress.Xpf.Editors.TextEdit  5   
   %  DevExpress.Xpf.Editors.BaseEdit   NullText$ 文本编辑��6   $
�� 200��6)   .��5      ]� $
��True��6   .+�5   
   ) 
 #DevExpress.Xpf.Ribbon.RibbonControl  -   5          Name$ ribbon��5      $	��Top=�6.      RibbonStyle$ 
Office2019=�6C      ApplicationButtonText$ 系统��5         ToolbarShowMode$
 Hide=�68   !   ApplicationButtonLargeIcon 5      *  $DevExpress.Xpf.Core.DXImageExtension  *&$SvgImages/Outlook Inspired/Close.svg+   ApplicationMenu 5      + 
 %DevExpress.Xpf.Ribbon.ApplicationMenu  5      	   RightPaneWidth$		 280Q�6&   
   ShowRightPane$

 True��6;   #	  DevExpress.Xpf.Bars.PopupMenu 	  Items. 5      '
  !DevExpress.Xpf.Bars.BarButtonItem
  -   5         
 Name$ biClose��5   (   !  DevExpress.Xpf.Bars.BarItem   Content)  #DevExpress.Xpf.Core.ObjectConverter$ Close window 5    (      
LargeGlyph 5!   (     *&$SvgImages/Outlook Inspired/Close.svg+   Command 5"   (   �� *CloseWindowCommand+   	RightPane 5$      �� 5%      
�������$��1,0,0,0q�64   
�������._�5&      �  � RowDefinitions 5'   "   �� 5(   &   $
��Auto �64   �� 5)   &   $��* �64   .+�5+   "   �� $��0,0,0,1q�6)   
�������$��7,5,5,0q�6[   ._�5,   &   �� $
��Bold"�6,   .��6>   Recent Documents:6Q   5-   #   �� 5.   "   $��1��6*   $��0q�67   $��2,0,0,0q�6K   ��5/   &   �� 50   *   .R�51   .   ��  �� Orientation$ 
Horizontal=�69   $��0,2,0,0q�6R   .+�52   2   �� $��0,0,0,1q�69   
���   �._�53   6   �� ��6@   �� *Number+54   3   �� 55   2   $��7,0,0,0q�6V   ��6<   �� *
FileName+6V   56   /   57   +   5:   #   5;      5<         
BottomPane 5>      �� 5?      $ 
Horizontal=�6%   .+�5@      �� $��Options��6&   $	��100��68   $��0,0,10,0q�6D   �� 5B      $
��Exit��6&   $	��100��65   5D      5F         ToolbarItems 5H      +  %DevExpress.Xpf.Bars.BarButtonItemLink  5I      )  #DevExpress.Xpf.Bars.BarItemLinkBase   BarItemName$ newItem��6(      Items. 5K      5 
 /DevExpress.Xpf.Ribbon.RibbonDefaultPageCategory  2 
 ,DevExpress.Xpf.Ribbon.RibbonPageCategoryBase   Pages. 5L      & 
  DevExpress.Xpf.Ribbon.RibbonPage     Caption$
 Home 6!      Groups. 5M      + 
 %DevExpress.Xpf.Ribbon.RibbonPageGroup     Caption$
 File��6*      Items. 5N      
  -   5        $ newItem��5N   ,   $	 New 6=      Glyph 6K     *SvgImages/Actions/New.svg+5O      5P      5Q      5R      2 
 ,DevExpress.Xpf.Ribbon.RibbonStatusBarControl  5S   
   -   5          Name$ 	statusbar��5S   %   $��Bottom=�68      
RightItems 5T      
  5U      -   5        $ 	printItem��5U   $   $ Print 67    6G     *SvgImages/Print/Print.svg+5W      .  (DevExpress.Xpf.Docking.DockLayoutManager  5Y   
   -   5          Name$ 	layoutMgr��5Y   8       FloatingMode$  Desktop=�6!   !   AutoHideGroups! 5Z      *  $DevExpress.Xpf.Docking.AutoHideGroup  5[      "   DockType$" Right=�6&   (  "DevExpress.Xpf.Docking.LayoutGroup#   Items.# 5\      (  "DevExpress.Xpf.Docking.LayoutPanel  +  %DevExpress.Xpf.Docking.BaseLayoutItem$   Caption$$  PanelRight��6'   5]        5^      $
" Left=�6&   .# 5_        $$ 
 PanelLeft��6'   5`        5a      $" Bottom=�6&   .# 5b        $$  PanelBottom��6'   5c      %   
LayoutRoot.% 5g        -   5        &  Name$& 
rootLayout��5g      $$ 
LayoutRoot��63   '   Orientation$' Vertical=�6H   .# 5h      *  $DevExpress.Xpf.Docking.DocumentGroup  -   5        (  Name$( documentGroup��5h   &   )   SelectedTabIndex$) 1��6=   .# 5i      *  $DevExpress.Xpf.Docking.DocumentPanel  $$ 
Document 1��6)   (  "DevExpress.Xpf.Docking.ContentItem*   Content.* 5j      �� 5k        5l      $$ 
Document 2��6)   .* 5m      �� 5n        5o      $$ 
Document 3��6)   .* 5p      �� 5q      5r      5s      5t      5v      5w      