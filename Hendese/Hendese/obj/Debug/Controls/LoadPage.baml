   M S B A M L   `   `   `  ����B  >Hendese, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Hendese.ControlsHendese.Controls  5          Hendese.Controls.LoadPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:Hendese.Controls     5        N� Title$  LoadPage��5	       � 	Resources 5
         SimpleSingleLoad  SimpleDistributedLoad  ConsolSingleLoad  ConsolDistributedLoad  FixedSingleLoad  FixedDistributedLoad��%�  &	        &	 7     &	 s     &	 �     &	 �     &	     �� 5   
   $#��../Icons/SimpleSingleLoad.png��6)   �� 5   
   $(��"../Icons/SimpleDistributedLoad.png��6.   �� 5   
   $#��../Icons/ConsolSingleLoad.png��6)   �� 5   
   $(��"../Icons/ConsolDistributedLoad.png��6.   �� 5   
   $"��../Icons/FixedSingleLoad.png��6(   �� 5   
   $'��!../Icons/FixedDistributedLoad.png��6-   5      .��5      � $��1��6   $��	0,6.8,0,0q�6    � ColumnDefinitions 5   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      .+�5   
   �� -   5         ��Name$ SimpleSingleLoad��5      $��0��6+   $��0��68   #��[  5      $��0,0,3,3q�5        SquareButtonStyle#���  6'   �� 5    
   -   5        $ SimpleDistributedLoad��5       $��0��60   $��1��6=   #��[ 5!      $��3,0,0,3q�5"      #���  6&   �� 5#   
   -   5        $ ConsolSingleLoad��5#      $��1��6+   $��0��68   #��[ 5$      $��0,3,3,3q�5%      #���  6&   �� 5&   
   -   5        $ ConsolDistributedLoad��5&      $��1��60   $��1��6=   #��[ 5'      $��3,3,0,3q�5(      #���  6&   �� 5)   
   -   5        $ FixedSingleLoad��5)      $��2��6*   $��0��67   #��[ 5*      $��0,3,3,0q�5+      #���  6&   �� 5,   
   -   5        $ FixedDistributedLoad��5,      $��2��6/   $��2��6<   #��[ 5-      $��3,3,0,0q�5.      #���  6&   5/      50      