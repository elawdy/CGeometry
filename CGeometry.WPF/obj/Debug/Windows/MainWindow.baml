   M S B A M L   `   `   `  ����H  DCGeometry.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:CGeometry.WPF.WindowsCGeometry.WPF.Windows  5   	   &     CGeometry.WPF.Windows.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:CGeometry.WPF.Windows     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5
      ]� .+�5   
   +  %System.Windows.Controls.Ribbon.Ribbon  $	��Top=�6      QuickAccessToolBar 5      =  7System.Windows.Controls.Ribbon.RibbonQuickAccessToolBar  5      .@�5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� .��6"   samfnm6*   �� 5      .��6"   samfnm6*   �� 5      .��6"   samfnm6*   5      5         ApplicationMenu 5      :  4System.Windows.Controls.Ribbon.RibbonApplicationMenu  5      5  /System.Windows.Controls.Ribbon.RibbonMenuButton   SmallImageSource$ /icons/ribbon-icon.png��6(   $��40��6R   $��56��6^   $	��Top=�6i   $��	0,0,2,-15q�6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   z�5      V� 5      .~�5      �� �� 5      �� 5      T� 5       T� Y$	 -20Q�61    T� X$ 3Q�69   5       ��5"      �� 5#      .@�5%      >  8System.Windows.Controls.Ribbon.RibbonApplicationMenuItem  $
��File��60     5&      $
��Save��60      AuxiliaryPaneContent 5'      2  ,System.Windows.Controls.Ribbon.RibbonGallery  5(      $
��Auto=�6(   .@�5)      :  4System.Windows.Controls.Ribbon.RibbonGalleryCategory  .@�5*   "   6  0System.Windows.Controls.Ribbon.RibbonGalleryItem  $��	CGeometry��64   5,   #   5-      5.      50      .@�55      .	  (System.Windows.Controls.Ribbon.RibbonTab	  -   5        $��
tab_shapes��55      $��Shapes��6*   .@�57      0
  *System.Windows.Controls.Ribbon.RibbonGroup
  $��Point��6   .@�59      � 	 � ColumnDefinitions	 5:      �� 5;      $��* �6/   �� 5<      $��* �6/   �� 5=      $��* �6/   .+�5?      1  +System.Windows.Controls.Ribbon.RibbonButton  $��0��6'   
   Label$
 	Add Point��67   ��5@      �� 5A   "   5C        5D      $��1��6'   $
 Random Points��67   6O   5E      5F      
  5H      $
��Line��6   .@�5I      � 	 5J      �� 5K      $��* �6/   �� 5L      $��* �6/   �� 5M      $��* �6/   .+�5O        $��0��6'   $
 Add Line��67      LargeImageSource$ /icons/lineee.png��6H     5P      $��1��6'   $
 Random Lines��67   6N   5Q      5R      
  5T      $��	Rectangle��6   5U      5V      	  5X      $��Solve��6   .@�5Y      
  $��
Sweap Line��6   .@�5Z      � 	 5[      �� 5\      $��* �6/   �� 5]      $��* �6/   �� 5^      $��* �6/   .+�5`        $��0��6'   $
 Closest Pair��67     5a      $��1��6'   $
 Segment Intersect��67   6S   5b      5f        5g      $��RibbonApplicationMenuItem��6,   $	��100��6O   $	��100��6\   5h      	  5k      $
��Edit��6   .@�5l      
  $
��Edit��6   .@�5m      � 	 5n      �� 5o      $��* �6/   �� 5p      $��* �6/   �� 5q      $��* �6/   .+�5s        $��0��6'   $
 Closest Pair��67     5t      $��1��6'   $
 Segment Intersect��67   6S   5u      5y        5z      $��RibbonApplicationMenuItem��6,   $	��100��6O   $	��100��6\   5{      5|      �� 5}   
   5      5�      5�      