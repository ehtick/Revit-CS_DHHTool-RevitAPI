   M S B A M L   `   `   `  ����\  XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ComboBoxNormalBorderBrush  �� Color ! ComboBoxNormalBackgroundBrush # ComboBoxDisabledForegroundBrush # ComboBoxDisabledBackgroundBrush  ComboBoxDisabledBorderBrush   ComboBoxToggleButtonTemplate �� 
TargetType � ColumnDefinitions �� CornerRadius �� Triggers P� Property P� Value �� 
TargetName �� Property	 �� Value  ComboBoxStyle
 �� 
TargetType �� 	ClickMode d� 	IsChecked �� Path �� RelativeSource �� Mode �� SelectionBoxItem �� SelectionBoxItemTemplate J� IsHitTestVisible �� 
IsReadOnly �� 
IsReadOnly � AllowsTransparency �� IsDropDownOpen � ActualWidth �� MaxDropDownHeight J� SnapsToDevicePixels-    'System.Windows.Input.KeyboardNavigation    DirectionalNavigation�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      6W   %  &	        &	 #     &	 A     &	 _     &	 }     &	 �     7  7   7  7  7  7  7  &	     7  7   7  �� 5      $  	LightGray��68   �� 5      $
  #fff��6<   �� 5      $
  #888��6>   �� 5      $
  #eee��6>   �� 5      $
  #888��6:   �� 5	      " d�6;   .R�5
   
   �  5      �� 5      �� 5      $��20 �6#   .+�5      �� $��Border��5      $��2��5      8��  8�� $��1,1,1,1q�5      $ 0��5      �� 5      $��ButtonBorder��5      $��1��5      $��1,1,1,1q�5      8�� ���#444$��0,0,0,0q�5      $ 
0, 0, 0, 0��5      F� 5      $��Arrow��5      $��1��5       $��Center=�5!      $��Center=�5"      =����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	���#4445$      5%       5&   
   P� 5'       �@J�IsMouseOver . . �5(      �� $ ButtonBorder��6    � ��
	 �����6O   5)      P� 5*       �@d�	IsChecked$
 TrueY�68   . �5+      �� $ ButtonBorder��6    � ��
	 �����6O   �� 5,      $ Arrow��6    � ��	 �	#FF8D979E6B   5-      P� 5.       � {� .  . �5/      �� $ Border��6   8	   � ��6I   �� 50      $ ButtonBorder��6   8	   � ��6O   �� 51      $ ButtonBorder��6   8	   � ��6Q   �� 52      8	   � ��6;   �� 53      $ Arrow��6    � ��	 �#9996B   54      56      �� 58      "
 ��6"   .�59   
   ��  �@J�SnapsToDevicePixels	 . 60   �� 5:   
    �@�OverridesDefaultStyle	 . 62   �� 5;   
    � ��$
	 Auto=�6G   �� 5<   
    � ��$
	 Auto=�6E   �� 5=   
    � ��	 . 6:   �� 5>   
    � ��
	 �   �63   �� 5?   
    �@�FocusVisualStyle	 6-   Z� �� 5@   
    � ��$	 4q�6#   �� 5A   
    � ��$	 Center=�65   �� 5B   
    � ��$	 Center=�67   �� 5C   
    �@��
IsEditable	 .  6'   �� 5D   
    � ��	 5E      �� 5F      " ��6"   .R�5G      � .+�5H      d� $��ToggleButton��5I      $��2��5J      $ Press=�5K      |�.  8��   5M      �� $ IsDropDownOpen� �� *TemplatedParent+$ TwoWay=�5N      �� 5O      $��ContentSite��5P      $��5,3,23,3q�5Q      #��z��5R      #��z��5S      #��z 5T      #��z 5U      #��z��5V       .  5W      �� 5X      $��PART_EditableTextBox��5Y      $��3,3,23,3q�5Z      $
��Left=�5[      $��Center=�5\      
������ |�. # z 5_      $y�Hidden=�5`      .��61   ��5a      �� 5b   "   " ��62   .R�5c   &   �� $��PART_ContentHost��6-   |�.  6E   5d   #   .��5e   0   5f      � 5h      $��Popup��5i       . |�.  #��z 5l      $��Bottom=�5m      $��Slide=�5n      .��5o      � $��DropDown��5p   !   #��z 5q   !   #��z 5r   !    . .+�5t   "   �� $��DropDownBorder��5u   %   $��0,1,0,0q�5v   %   
�������8�� $��1,1,1,1q�5y   %   $ 0��5z   %   �� 5{   "   $��4q�6/    . .��5|   &   �� $ 	Contained=�65   5}   #   5~      5      5�       5�      P� 5�       �@��HasItems .  . �5�      �� $ DropDownBorder��6%    � ��$	 95��6g   5�      P� 5�       � {� .  . �5�      �� 8	   � ��6G   5�      P� 5�       �@��
IsGrouping . . �5�      ��  � ��	 .  6N   5�      P� 5�       �@��
IsEditable . . �5�      ��  �@  	IsTabStop	 .  6M   �� 5�      $ PART_EditableTextBox��6%    � y�$	 Visible=�6g   �� 5�      $ ContentSite��6%    � y�$	 Hidden=�6^   5�      5�      5�      5�      5�      