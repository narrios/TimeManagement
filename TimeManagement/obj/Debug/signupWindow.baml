   M S B A M L   `   `   `  ����I  ETimeManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:TimeManagementTimeManagement  5   	   !    TimeManagement.signupWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:TimeManagement     5   	   _ [MaterialDesignThemes.Wpf, Version=4.4.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  To Do��5	   	   $	��500��6   $	��350��6$    � 
ResizeMode$ CanMinimize=�60    � Icon$ /Icons/To-Do_icon.png��6I      windowStyle#���   6f   .��5
      �  � ColumnDefinitions 5   
   �� 5       � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      �� 5      $��25 �6   �� 5      �� 5      $��25 �6   �� 5      �� 5      $��25 �6   �� 5      �� 5      $��80 �6   �� 5      $��50 �6   .+�5   
   �� $��1��6   .+�5      �� -   5         ��Name$ 	errorName��5      $��
0,-12,25,0q�6'   $��0,0,0,2q�6;   $��10(�6M   $y�Hidden=�6[   
���  ��$��Right=�6�   .��6�   Format nume/prenume incorect6�   �� 5      -   5         ��Name$
 name��5      $��1��6$     textBoxStyle#���  61   6d   5      �� 5    
   $��3��6   .+�5!      �� -   5        $ 
errorEmail��5!      $��
0,-12,25,0q�6(   $��0,0,0,2q�6<   $��10(�6N   $y�Hidden=�6\   
���  ��$��Right=�6�   .��6�   Format email incorect6�   �� 5"      -   5        $ email��5"      $��3��6%   #���  62   6e   5#      �� 5%   
   $��0��6   $��Nume/Prenume��6!     textBlockStyle#���  65   �� 5&   
   $��2��6   $��Email��6!   #���  6.   �� 5'   
   $��4��6   $��Parola��6!   #���  6/   �� 5(   
   $��6��6   $��Repetă parola��6!   #���  66   �� 5)   
   -   5         ��Name$
 link��5)   !   $��9��6   $��Center=�6/   $	��Top=�6L   $��
ExtraLight"�6d   $��14(�6|   #���  6�   �� 5*      -   5        .�5*   .   Ești înregistrat? Logare6H   5+      �� 5-   
   $��5��6   .+�5.      �� -   5        $ errorPassword��5.      $��
0,-12,25,0q�6+   $��0,0,0,2q�6?   $��10(�6Q   $y�Hidden=�6_   
���  ��$��Right=�6�   .��6�   Parolă prea scurtă6�   G� 5/      -   5         G�Name$ password��5/        passwordBoxStyle#���  6,   6c   �� 50      -	   5        $ passwordSymbols��50      $��25 -30 25 0q�6/   $y�Hidden=�6D   #���  6X   6�   �� 51      -
   5        $��25��51      $��25��6!   $��
0 -30 30 0q�6,   $��Right=�6@     buttonStyle#���  6\   .��52      '  !MaterialDesignThemes.Wpf.PackIcon  	   Kind$		 Eye=�6*   $��15��65   $��15��6@     packIconStyle#���  6L   53      54      �� 55   
   $��7��6   .+�56      �� -   5        $ errorConfirmPassword��56      $��
0,-12,25,0q�62   $��0,0,0,2q�6F   $��10(�6X   $y�Hidden=�6f   
���  ��$��Right=�6�   .��6�   Parolele nu coincid6�   G� 57      -   5        $ confirmpassword��57      $��7��63   #���  6@   6w   �� 58      -   5        $ confirmPasswordSymbols��58      $��25 -30 25 0q�66   $y�Hidden=�6K   #���  6_   6�   �� 59      -   5        $��25��59      $��25��6!   $��
0 -30 30 0q�6,   $��Right=�6@   #���  6\   .��5:        $		 Eye=�6*   $��15��65   $��15��6@   #���  6L   5;      5<      �� 5>   
   -   5        
 ��Name$
 button��5>      $��8��6!   $��Înregistrare��6.   #��[ 6E   6v   5?      5@      