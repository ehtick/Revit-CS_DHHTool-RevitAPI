   M S B A M L   `   `   `  ����X  T02_02_DrawDetailBeam_Detail2D, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:_02_02_DrawDetailBeam_Detail2D.MVVM.View(_02_02_DrawDetailBeam_Detail2D.MVVM.View  5      B    <_02_02_DrawDetailBeam_Detail2D.MVVM.View.vMainDrawDetailBeam   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Hlocal6clr-namespace:_02_02_DrawDetailBeam_Detail2D.MVVM.View     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5          �Name$  DetailBeam2D��5       � Title$ Vẽ mặt cắt dầm��5	      $
��1400��5
      $	��700��5       � 
ResizeMode$ NoResize=�5       � WindowStartupLocation$ CenterScreen=�5       � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $N Hpack://application:,,,/00_00_DHHShareLibrary;component/Themes/Style.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      �� 5      $	��200 �6   �� 5      $��52 �6   .+�5   
   ��  �� TabStripPlacement$
 Left=�6    5      	 �� 
TargetType
 �� Property �� Value �� RelativeSource �� AncestorType��%�   '��       �� 5      "	 ��6   .�5      �� 
 � �� 5       �� *Height+ �� *FindAncestor+" ��5#      5$      .@�5%      �� ��5&      �� 5'      $
��Left=�6!    �� Orientation$ 
Horizontal=�5(   !   .+�5)      �� $��Normal$�5*      $��DemiBold"�5+      $��
Vẽ nhanh��5,      5-      5/      �� 50      ��51      �� 52      $
��Left=�6!   $ 
Horizontal=�53   !   .+�54      �� $��Normal$�55      $��DemiBold"�56      $��Vẽ chi tiết��57      58      5:      5;      �� 5<   
   -   5         ��Name$ 
viewCanvas��5<      
�������$��1��5>      $��5q�6   5?      ]� 5@   
   $��2��6   ��. $��Right=�66   .+�5A      �� $	��140��5B      $��40��6   $��3q�5C      $��Chọn dầm��5F      $��Arial)�6$      ButtonStyle#���   5G      ��5D      �� *
BtnChonDam+��5E      ��  �� ElementName$ DetailBeam2D��5G      �� 5H      $	��140��5I      $��40��6   $��3q�5J      $��
Vẽ thép��5M      $��Arial)�6#   #���   5N      ��5K      �� *BtnOK+��5L      �� $ DetailBeam2D��5N      �� 5O      $	��140��5P      $��40��6   $��3q�5Q      $��
Hủy bỏ��5T      $��Arial)�6"   #���   5U      ��5R      �� *	BtnCancel+��5S      �� $ DetailBeam2D��5U      5V      5W      5X      