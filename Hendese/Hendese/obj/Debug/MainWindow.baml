   M S B A M L   `   `   `  ����B  >Hendese, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:HendeseHendese  5   	       Hendese.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Hendese     5   	   /tb http://www.hardcodet.net/taskbar     5   	   -   5          � Title$  
MainWindow��5	   	    � ShowInTaskbar .   � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	    � AllowsTransparency . $y�Visible=�5   	    � Topmost . $	��420��5   	   $	��700��6   
������� � 	Resources 5         gridFadeInStoryBoard �� 
TargetName g� 	BeginTime	 �� TargetProperty
 W� From	 W� To g� AutoReverse g� Duration  gridFadeOutStoryBoard��%T  &	        &	 �     �� 5   
   .��5      W� $ MainGrid��6   $ 0:0:0.1X�6?   $	 Opacity�5      $

 0.00X�65   $
 1.00X�6A    .  $ 0:0:0.3D�6_   5      �� 5   
   .��5      W� $ MainGrid��6   $ 0:0:0.5X�6?   $	 Opacity�5      $

 1.00X�65   $
 0.00X�6A    .  $ 0:0:0.3D�6_   5      5      .��5       � .+�5"   
   ��  �� Padding$ 10, 0, 10, 10q�6   
���   �
�������$��1,1,1,1q�5%      ._�5&      �  � RowDefinitions 5(      �� 5)      $��30 �6$   �� 5*      .+�5-      � $��0��6   ���#3F3F46$��-10, 0, -10, 0q�69   .+�5.      �� -   5        $��0��5.   3   $��0q�6@   $��75��5/      $��30��6$   $
��Left=�50      $	��Top=�64     SquareButtonStyle#���  6L   
������ $��0q�51   2   .��52      �� $��0q�6%    �� Orientation$ 
Horizontal=�60   .+�53      �� $��Icons/back.png��6$   $��25��6<   �� 54      $�� Back��6(   $��Center=�65   ���#aaa55   (   56      57      �� 59      -   5        $��-��59      $��0��6C   $��30��5:      $��30��6   $��
0,0,30.4,0q�6(   $��Right=�5;      $	��Top=�6-   #���  6E   $��0q�5<      
������ 
������ ���#aaa5=      �� 5?      -   5         ��Name$ btnClose��5@   (   $��x��5?      $��0��6@   $��30��5@      $��30��6   $��Right=�5A      $	��Top=�6-   #���  6E   $��0q�5B      
���  ��
������ 
�������5D      5E      � 5H      -   5         �Name$ MainGrid��5H      $��1��6)   $��0,10,0,0q�66   5L       J� Effect 5M      3  -System.Windows.Media.Effects.DropShadowEffect  5N         Color$ 
BlueViolet��6#      Opacity$	 0.5Q�66   5P      5Q      5R      