   M S B A M L   `   `   `  ����B  >Hendese, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Hendese.ControlsHendese.Controls  5      "    Hendese.Controls.SectionPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:Hendese.Controls     5        N� Title$  SectionPage��5	       � 	Resources 5
         	RedButton �� 
TargetType ��Name A�Name �� Triggers �� Value �� Binding �� Path	 �� 
TargetName
 �� Property �� Value K� ZIndex��%  &	        �� 5   
   " ��6,   .R�5      � .+�5      �� $ btnQuickReport��6   -   5        $��?��5   1   
���������5      �� 5      " ��6*   .R�5      � .+�5      A� $ ellipse��6&   $��40��5   !   $��40��6,   
�������$��1��5   0   $
��Left=�5   !   $	��Top=�6<   �� 5      $��40��6(   $��20(�64   $��	15,0,30,0q�5   !   $
��Left=�5   !   $��Center=�6<   #��z��5   !   #��z��5   !   5       5      �� 5   "   $
 True��6U    6.   �� $ ResultSuccess�.a�5   &   �� $	 ellipse��6=   
 � ��
 � � �6R   �� 5    &   
 �@K�ZIndex$ 1��6E   5!   #   �� 5"   "   $ False��6U    6.   �� $ ResultSuccess�.a�5#   &   �� $	 ellipse��6=   
 � ��
 �  ��6R   5$   #   5&      5(      �� 5*      $ 	btnReport��6   -   5        $��Report��5+      $ 0��6*   
��� � �
�������$	��120��5-      $��30��6%   $��20,5,0,0q�61   $
��Left=�5.      $	��Top=�64   5/       50      �� 51      $
 True��6E    6   �� $ ResultSuccess�.a�52      �� $	 	btnReport��63   
 � y�$ Visible=�6J   53      �� 54      $ False��6E    6   �� $ ResultSuccess�.a�55      �� $	 	btnReport��63   
 � y�$ Hidden=�6J   56      58      59      .��5:      � .+�5;   
   �� $
��Auto=�6   $
��Auto=�5<      .��5=      �� .+�5>      9� -   5         9�Name$ expInternalForces��5>      $��Internal Forces��66   $��
10,10,10,0q�5@      $	��Top=�63   $
��auto��6K   .��5A      � -   5         �Name$ 
ForceInput��5A      $��0��6/   $��0��6?   5B      9� 5D      -   5        $ 
expSection��5D      $��SECTION��6/   $��	10,0,10,0q�6@   .��5F      � -   5        $ SectionGrid��5F      $��
0,0,-1.2,0q�60   $	��Top=�6D   $
��auto��6\    � RowDefinitions 5G      �� 5H      $��40 �6,   �� 5I      �� 5J      �� 5K      $��60 �6,    � ColumnDefinitions 5M      �� 5N      �� 5O      .+�5R      �� -   5         ��Name$ comboBox��5R   #   $��0��65   $��2��6B   $	��Top=�5T   '    �� SelectedValue 5S   '   �� *SelectedSection+5U   '   � 5W      -   5        $ SectionInput��5W      $��0��65   $��1��6E   � 5X      -	   5        $ DesignInput��5X      $��0��64   $��2��6D   � 5Z      $��1��6   $��1��6,    5[      �� 5\   "   $��40 �60   �� 5]   "   .+�5`      �� $��0��6%   #��[  5a   %   $��0,0,0,0q�5b   %   $
��Left=�66   $	��Top=�6Q   � 5e      -
   5        $ SectionOutput��5e   #   $��2��6:   5f      5g      5h      5j      5k      5m      5n      