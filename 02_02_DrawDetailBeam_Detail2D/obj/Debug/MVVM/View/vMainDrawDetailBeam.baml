   M S B A M L   `   `   `  ����X  T02_02_DrawDetailBeam_Detail2D, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:_02_02_DrawDetailBeam_Detail2D.MVVM.View(_02_02_DrawDetailBeam_Detail2D.MVVM.View  5      B    <_02_02_DrawDetailBeam_Detail2D.MVVM.View.vMainDrawDetailBeam   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Hlocal6clr-namespace:_02_02_DrawDetailBeam_Detail2D.MVVM.View     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5          �Name$  DetailBeam2D��5       � Title$ Vẽ mặt cắt dầm��5	      $
��1400��5
      $	��700��5       � 
ResizeMode$ NoResize=�5       � WindowStartupLocation$ CenterScreen=�5       � 	Resources 5       �� Source�� 5   
   $4 ./DHHLibrary;component/WPF/Resource/Styles.xaml?�6   %    .��5      �  � RowDefinitions 5   
   �� 5      �� 5      $	��200 �6   �� 5      $��52 �6   .+�5   
   ��  �� TabStripPlacement$
 Left=�6    5       �� 
TargetType	 �� Property
 �� Value��%W   '��       �� 5      " ��6   .�5      �� 	 � ��$
 Stretch=�6:   5      5      .@�5      �� ��5       �� 5!      $
��Left=�5"   !    �� Orientation$ 
Horizontal=�5#   !   .+�5$      �� $��Normal$�5%      $��DemiBold"�5&      $��
Vẽ nhanh��5'      5(      5*      �� 5+      ��5,      �� 5-      $
��Left=�6!   $ 
Horizontal=�5.   !   .+�5/      �� $��Normal$�50      $��DemiBold"�51      $��Vẽ chi tiết��52      53      55      56      �� 57   
   -   5         ��Name$ 
viewCanvas��57      
�������$��1��59      $��5q�6   5:      ]� 5;   
   $��2��6   ��. $��Right=�66   .+�5<      �� $	��140��5=      $��40��6   $��3q�5>      $��Chọn dầm��5A      $��Arial)�6$      ButtonStyle#���   5B      ��5?      �� *
BtnChonDam+��5@      ��  �� ElementName$ DetailBeam2D��5B      �� 5C      $	��140��5D      $��40��6   $��3q�5E      $��
Vẽ thép��5H      $��Arial)�6#   #���   5I      ��5F      �� *BtnOK+��5G      �� $ DetailBeam2D��5I      �� 5J      $	��140��5K      $��40��6   $��3q�5L      $��
Hủy bỏ��5O      $��Arial)�6"   #���   5P      ��5M      �� *	BtnCancel+��5N      �� $ DetailBeam2D��5P      5Q      5R      5S      