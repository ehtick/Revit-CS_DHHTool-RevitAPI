   M S B A M L   `   `   `  ���� K  GDHHTools_Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:DHHToolsDHHTools  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 .  *TreeViewItem.TreeArrow.Static.Checked.Fill  �� Color 0 ,TreeViewItem.TreeArrow.Static.Checked.Stroke + 'TreeViewItem.TreeArrow.MouseOver.Stroke ) %TreeViewItem.TreeArrow.MouseOver.Fill 3 /TreeViewItem.TreeArrow.MouseOver.Checked.Stroke 1 -TreeViewItem.TreeArrow.MouseOver.Checked.Fill  	TreeArrow & "TreeViewItem.TreeArrow.Static.Fill ( $TreeViewItem.TreeArrow.Static.Stroke 	 TreeViewItemFocusVisual �� Property �� Value 
 ExpandCollapseToggleStyle �� 
TargetType �� 
TargetType �� Padding F�Name �� Angle �� CenterX	 �� CenterY
 �� Triggers P� Property P� Value �� 
TargetName ]� 
Conditions �� Property �� Value  TreeViewStyle �� RelativeSource �� AncestorType � ColumnDefinitions � RowDefinitions d�Name �� 	ClickMode d� 	IsChecked ��Name J� SnapsToDevicePixels ��Name ��Name    DHHTools.CustomTreeView �� Triggers�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml      localclr-namespace:DHHTools  %�  &	        &	      &	 *     &	 ?     &	 V     &	 k     &	 �     &	 �     &	 �     &		 �     &	
 �     7  7  7  7   7  7  7  7  7  &	 �    7 	 7 
 �� $  	#FF595959���� $  	#FF262626���� $  	#FF1BBBFA���� $  Transparent���� $  	#FF262626���� $  	#FF595959��B� $��M0,0 L0,6 L6,0 zC��� $  Transparent���� $  	#FF989898���� .���  � �� �� .R��� �� " d�.���  � |� .  ��  � ��$ 16����  � ��$ 16����  � �� �� " d�.R��� $��16��$��16��$ 5,5,5,5q�
������ ._�F� $ 
ExpandPath��8��  8�� 8�� z��� $	 135Q�$ 3Q�$	 3Q�
 P�  �@d�	IsChecked$
 TrueY�. ��� $ 
ExpandPath�� � z� �� $	 180Q�$ 3Q�$	 3Q��� $ 
ExpandPath��8   � ���� $ 
ExpandPath��8   � ��P�  �@J�IsMouseOver . . ��� $ 
ExpandPath��8   � ���� $ 
ExpandPath��8   � ��]�  ��  �@J�IsMouseOver . ��  �@d�	IsChecked$
 TrueY�./��� $ 
ExpandPath��8   � ���� $ 
ExpandPath��8   � ���� " Q�.���  � ��
 ���� ��  � ��
 �������  � �� �� *HorizontalContentAlignment+ �� " ����  � �� �� *VerticalContentAlignment+ �� " ����  � ��$ 1,0,0,0q���  �@Q�
IsExpanded . ��  � ��# � ����  �@�FocusVisualStyle8   ��  � �� �� " Q�.R��  �� $
��Auto �$��19���� $
��Auto ��� $��* � �� $
��Auto ��� .+�d� $ Expander��$ Press=�8��  �� *
IsExpanded+ �� *TemplatedParent+�� $ Bd��$��1��# z��#��z��#��z��#��z�� . ._��� $ PART_Header��#��z��$��Header��# z �� $ 	ItemsHost��$��1��$��1��$��2��
 P�  �@Q�
IsExpanded .  . ��� $ 	ItemsHost�� � y�$ 	Collapsed=�P�  �@��HasItems .  . ��� $ Expander�� � y�$ Hidden=�P�  �@  
IsSelected . . ��� $ Bd��# � �� � ���� # � �� � ��]�  ��  �@  
IsSelected . ��  �@Q�IsSelectionActive .  ./��� $ Bd��# � /� � ���� # � .� � ��P�  � {� .  . ��� # � �� � �� P�  �@%�IsVirtualizing . . ���  � �� �� .R�$� 