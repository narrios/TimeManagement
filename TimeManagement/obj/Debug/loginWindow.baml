   M S B A M L   `   `   `  ����I  ETimeManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:TimeManagementTimeManagement  5   	        TimeManagement.loginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:TimeManagement     5   	   _ [MaterialDesignThemes.Wpf, Version=4.4.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  To Do��5	   	   $	��450��6   $	��350��6$    � 
ResizeMode$ CanMinimize=�60    � Icon$ /Icons/To-Do_icon.png��6I      windowStyle#���   6f   .��5
      �  � RowDefinitions 5   
   �� 5      $	��110 �6   �� 5      $��30 �6   �� 5      �� 5      $��30 �6   �� 5      �� 5      $��40 �6   �� 5      $��80 �6   �� 5      $��50 �6   .+�5   
   �� -   5        $��6��5      $��Logare��62     buttonStyle#���  6C   �� 5   
   $��1��6   $��Email��6!     textBlockStyle#���  6.   �� 5   
   $��3��6   $��Parola��6!   #���  6/   �� 5   
   $��5��6    �� Orientation$ 
Horizontal=�6"   $
��25 0q�6;   .+�5      �� -   5         ��Name$ memorizecheck��5      $��Center=�6.   �� 5      $��Memorizează contul��6   $��Center=�62   $
��10 0q�6M   #���  6[   5      �� 5   
   $��8��6   $��Center=�6!   $	��Top=�6>   $��
ExtraLight"�6V   $��14(�6n   #���  6|   �� 5       -   5        .�5    /   &$Nu ești înregistrat? Înregistrare6R   5!      �� 5#   
   $��2��6   .+�5$      �� -   5         ��Name$ error��5$      $��
0,-12,25,0q�6#   $��0,0,0,2q�67   $��10(�6I   $y�Hidden=�6W   
���  ��$��Right=�6�   .��6�   Login sau parola incorecte6�   �� 5%      -   5         ��Name$ email��5%        textBoxStyle#���  6%   6X   5&      G� 5(   
   -   5         G�Name$ password��5(      $��4��6(     passwordBoxStyle#���  65   6l   �� 5)   
   -   5        $ passwordSymbols��5)      $��4��6+   $y�Hidden=�68   #���  6L   6   �� 5*   
   -   5        $��4��5*      $��25��6   $��25��6*   $��0 0 30 0q�65   $��Right=�6G   #���  6c   .��5+      '  !MaterialDesignThemes.Wpf.PackIcon  	   Kind$		 Eye=�6&   $��15��61   $��15��6<     packIconStyle#���  6H   5,        5.   
   $
	 User=�6"   $��0��6.   $��90��6;   $��90��6F   $��Stretch=�6R   $��Stretch=�6n     packIconLoginStyle#���  6�   5/      50      